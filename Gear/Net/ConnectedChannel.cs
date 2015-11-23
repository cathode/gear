/******************************************************************************
 * Gear: An open-world sandbox game for creative people.                      *
 * http://github.com/cathode/gear/                                            *
 * Copyright © 2009-2016 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT        *
 * license. See the included LICENSE file for details.                        *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Diagnostics.Contracts;
using ProtoBuf;
using System.Threading;

namespace Gear.Net
{
    /// <summary>
    /// Represents a communication channel that 
    /// </summary>
    public class ConnectedChannel : Channel
    {
        private Socket socket;

        private IPEndPoint cachedRemoteEP;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectedChannel"/> class.
        /// </summary>
        /// <param name="remoteEP"></param>
        public ConnectedChannel(IPEndPoint remoteEP)
        {
            Contract.Requires(remoteEP != null);

            this.cachedRemoteEP = remoteEP;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectedChannel"/> class.
        /// </summary>
        /// <param name="socket"></param>
        public ConnectedChannel(Socket socket)
        {
            Contract.Requires(socket != null);

            this.socket = socket;
            if (this.socket.Connected)
                this.State = ChannelState.Connected;

            this.cachedRemoteEP = this.socket.RemoteEndPoint as IPEndPoint;
        }

        /// <summary>
        /// Raised when the Channel establishes a connection to the remote endpoint,
        /// or when a new inbound connection is accepted by a <see cref="ConnectionListener"/>
        /// </summary>
        public event EventHandler Connected;

        public event EventHandler Disconnected;

        #region Properties
        public Guid RemoteEndPointId { get; set; }

        public EndPointKind RemoteEndPointKind { get; set; }

        public override IPEndPoint LocalEndPoint
        {
            get { return this.socket.LocalEndPoint as IPEndPoint; }
        }

        public override IPEndPoint RemoteEndPoint
        {
            get { return this.socket.RemoteEndPoint as IPEndPoint; }
        }
        #endregion

        public static ConnectedChannel ConnectTo(IPEndPoint remoteEP)
        {
            Contract.Requires(remoteEP != null);
            Contract.Ensures(Contract.Result<ConnectedChannel>() != null);

            var channel = new ConnectedChannel(remoteEP);
            channel.Connect();

            return channel;
        }

        public bool Connect(IPEndPoint ep = null)
        {
            if (this.State == ChannelState.Connected)
                throw new InvalidOperationException();

            if (ep == null)
                ep = this.cachedRemoteEP;
            else
                this.cachedRemoteEP = ep;

            if (this.socket != null)
                this.socket.Dispose();

            this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                this.socket.Connect(ep);
                this.OnConnected();
                this.Setup();
                return true;

            }
            catch (Exception ex)
            {
                // TODO: Handle failed connection attempt
                return false;
            }
        }

        /// <summary>
        /// Raises the <see cref="Connected"/> event.
        /// </summary>
        protected virtual void OnConnected()
        {
            this.State = ChannelState.Connected;

            if (this.Connected != null)
            {
                this.Connected(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Raises the <see cref="Disconnected"/> event.
        /// </summary>
        protected virtual void OnDisconnected()
        {
            this.State = ChannelState.Disconnected;

            if (this.Disconnected != null)
            {
                this.Disconnected(this, EventArgs.Empty);
            }
        }

        protected override int SendMessages(Queue<IMessage> messages)
        {
            //Contract.Requires(messages != null);
            //Contract.Requires(this.State == ChannelState.Connected);

            int sent = 0;

            using (var ms = new MemoryStream())
            {
                while (messages.Count > 0)
                {
                    var msg = messages.Dequeue();

                    if (msg == null)
                        throw new NotImplementedException("Null message in send buffer");

                    Serializer.SerializeWithLengthPrefix(ms, msg, PrefixStyle.Fixed32BigEndian);
                }

                var bytes = ms.GetBuffer();
                try
                {
                    sent = this.socket.Send(bytes, 0, (int)ms.Position, SocketFlags.None);
                }
                catch (SocketException ex)
                {
                    if (!this.socket.Connected)
                    {
                        // Channel is dead
                        this.OnDisconnected();
                    }
                }
            }
            return sent;
        }

        protected override void BeginBackgroundReceive()
        {
            var state = new RxState();

            // Look at only the length prefix first
            this.socket.BeginReceive(state.Buffer, 0, 4, SocketFlags.Peek, this.RecvCallback, state);
        }

        private void RecvCallback(IAsyncResult result)
        {
            try
            {
                var state = (RxState)result.AsyncState;

                var rxCount = this.socket.EndReceive(result);

                //Console.WriteLine("Read {0} bytes for message prefix", rxCount);
                IMessage msg = null;

                // Did we at least get the length prefix?
                if (rxCount == 4)
                {

                    // Check if we have the entire message
                    var msgSize = 0;
                    if (!Serializer.TryReadLengthPrefix(state.Buffer, 0, 4, PrefixStyle.Fixed32BigEndian, out msgSize))
                        throw new NotImplementedException();

                    var bsize = msgSize + 4;

                    // Can we use the existing buffer?
                    if (state.Buffer.Length < bsize)
                    {
                        // Resize rx state buffer
                        state.ResizeBuffers(bsize);
                    }

                    // Blocking receive from socket
                    this.socket.Receive(state.Buffer, bsize, SocketFlags.None);

                    state.BufferStream.Position = 0;

                    // Entire message available
                    msg = Serializer.DeserializeWithLengthPrefix<IMessage>(state.BufferStream, PrefixStyle.Fixed32BigEndian);
                }
                else
                {
                    // Length prefix incomplete.
                    // Pause for a millisecond to give the NIC a chance to fill up the buffer if data is enroute
                    Thread.Sleep(1);
                }

                // Read the next prefix.
                this.socket.BeginReceive(state.Buffer, 0, 4, SocketFlags.Peek, this.RecvCallback, state);

                if (msg != null)
                {
                    // After next read operation is dispatched, we need to handle the received message
                    this.QueueRxMessageThreadSafe(msg);
                }
            }
            catch (IOException ioe)
            {

            }
            catch (SocketException se)
            {
                if (!this.socket.Connected)
                    this.OnDisconnected();
            }
        }

        protected class RxState
        {
            public byte[] Buffer;
            public MemoryStream BufferStream;
            public int Offset;
            public const int BufferSegment = 8192; // 8KiB buffer size

            public RxState()
            {
                this.Buffer = new byte[RxState.BufferSegment];
                this.BufferStream = new MemoryStream(this.Buffer);
            }

            internal void ResizeBuffers(int bsize)
            {
                Array.Resize<byte>(ref this.Buffer, bsize);
                this.BufferStream.Dispose();
                this.BufferStream = new MemoryStream(this.Buffer);
            }
        }
    }
}
