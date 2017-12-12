/******************************************************************************
 * Gear: An open-world sandbox game for creative people.                      *
 * http://github.com/cathode/gear/                                            *
 * Copyright © 2009-2017 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT        *
 * license. See the included LICENSE file for details.                        *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ProtoBuf;

namespace Gear.Net
{
    /// <summary>
    /// Represents a communication channel that utilizes TCP/IP to maintain an active connection with the remote endpoint.
    /// </summary>
    public class ConnectedChannel : Channel
    {
        #region Fields
        private readonly object reconnectSync = new object();
        private IPTarget connectionTarget;
        private Timer reconnectTimer;
        private Socket socket;
        #endregion
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectedChannel"/> class.
        /// </summary>
        /// <param name="remoteEP"></param>
        public ConnectedChannel(IPEndPoint remoteEP)
        {
            Contract.Requires(remoteEP != null);

            this.connectionTarget = IPTarget.FromIPEndPoint(remoteEP);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectedChannel"/> class.
        /// </summary>
        /// <param name="target"></param>
        public ConnectedChannel(IPTarget target)
        {
            Contract.Requires<ArgumentNullException>(target != null);

            this.connectionTarget = target;
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
            {
                this.State = ChannelState.Connected;
            }

            this.connectionTarget = IPTarget.FromIPEndPoint(this.socket.RemoteEndPoint as IPEndPoint);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectedChannel"/> class.
        /// </summary>
        /// <param name="hostname"></param>
        /// <param name="port"></param>
        public ConnectedChannel(string hostname, ushort port)
        {
            Contract.Requires<ArgumentNullException>(hostname != null);

            this.connectionTarget = new IPTarget(hostname, port);
        }

        #endregion
        #region Events

        /// <summary>
        /// Raised when the Channel establishes a connection to the remote endpoint,
        /// or when a new inbound connection is accepted by a <see cref="ConnectionListener"/>
        /// </summary>
        public event EventHandler Connected;

        /// <summary>
        /// Raised when the <see cref="Channel"/> loses it's established connection to the remote endpoint.
        /// </summary>
        public event EventHandler<ChannelDisconnectedEventArgs> Disconnected;

        #endregion
        #region Properties
        /// <summary>
        /// Gets or sets the unique id of the client or server that the channel is connected to.
        /// </summary>
        public Guid RemoteEndPointId { get; protected set; }

        /// <summary>
        /// Gets the <see cref="IPEndPoint"/> representing the locally bound address of the channel.
        /// </summary>
        public override IPEndPoint LocalEndPoint
        {
            get { return this.socket?.LocalEndPoint as IPEndPoint; }
        }

        /// <summary>
        /// Gets the <see cref="IPEndPoint"/> representing the remote address of the peer this channel is connected to.
        /// </summary>
        public override IPEndPoint RemoteEndPoint
        {
            get { return this.socket?.RemoteEndPoint as IPEndPoint; }
        }

        #endregion
        #region Methods

        /// <summary>
        /// Creates and returns a new <see cref="ConnectedChannel"/> that is connected to the specified remote endpoint.
        /// </summary>
        /// <param name="remoteEP">An <see cref="IPEndPoint"/> that describes what to connect to.</param>
        /// <returns>A new <see cref="ConnectedChannel"/> instance, pre-connected to the specified remote endpoint.</returns>
        public static ConnectedChannel ConnectTo(IPEndPoint remoteEP)
        {
            Contract.Requires<ArgumentNullException>(remoteEP != null);
            Contract.Ensures(Contract.Result<ConnectedChannel>() != null);

            var channel = new ConnectedChannel(remoteEP);
            channel.Connect();

            return channel;
        }

        /// <summary>
        /// Creates and returns a new <see cref="ConnectedChannel"/> that is connected to the specified remote <see cref="IPTarget"/>.
        /// </summary>
        /// <param name="target">An <see cref="IPTarget"/> that describes what to connect to.</param>
        /// <returns>A new <see cref="ConnectedChannel"/> instance, pre-connected to the specified remote <see cref="IPTarget"/>.</returns>
        public static ConnectedChannel ConnectTo(IPTarget target)
        {
            Contract.Requires<ArgumentNullException>(target != null);
            Contract.Ensures(Contract.Result<ConnectedChannel>() != null);

            var channel = new ConnectedChannel(target);
            channel.Connect();

            return channel;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">Thrown if the channel is already connected.</exception>
        public bool Connect()
        {
            if (this.State == ChannelState.Connected)
            {
                throw new InvalidOperationException();
            }

            var ep = this.connectionTarget.GetNextReachableEndPoint();

            if (this.socket != null)
            {
                this.socket.Dispose();
            }

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
                this.OnDisconnected();
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

            var args = new ChannelDisconnectedEventArgs();

            // Raise the event.
            this.Disconnected?.Invoke(this, args);

            if ((args.ReconnectCount == -1 || args.ReconnectCount > 0) && args.ReconnectInterval.TotalMilliseconds > 0)
            {
                lock (this.reconnectSync)
                {
                    if (this.reconnectTimer == null)
                    {
                        this.reconnectTimer = new Timer(this.ReconnectCallback, args, args.ReconnectInterval, TimeSpan.FromMilliseconds(-1));
                    }
                    else
                    {
                        this.reconnectTimer.Change(args.ReconnectInterval, TimeSpan.FromMilliseconds(-1));
                    }
                }
            }
        }

        protected override int SendMessages(Queue<IMessage> messages)
        {
            if (messages == null || messages.Count == 0)
            {
                return 0;
            }

            int sent = 0;

            using (var ms = new MemoryStream())
            {
                while (messages.Count > 0)
                {
                    var msg = messages.Dequeue();

                    if (msg == null)
                    {
                        throw new NotImplementedException("Null message in send buffer");
                    }

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
            if (this.socket == null)
            {
                throw new InvalidOperationException();
            }

            var state = new RxState();

            // Look at only the length prefix first
            this.socket.BeginReceive(state.Buffer, 0, 4, SocketFlags.Peek, this.RecvCallback, state);
        }

        private void ReconnectCallback(object state)
        {
            var args = state as ChannelDisconnectedEventArgs;

            if (args == null)
            {
                return;
            }

            // TODO: Evaluate need for locking
            lock (this.reconnectTimer)
            {
                // attempt reconnect
                if (args.ReconnectCount > 0)
                {
                    if (!this.Connect())
                    {
                        args.ReconnectCount--;
                        this.reconnectTimer.Change(args.ReconnectInterval, TimeSpan.FromMilliseconds(-1));
                    }
                }
                else if (args.ReconnectCount == -1)
                {
                    if (!this.Connect())
                    {
                        this.reconnectTimer.Change(args.ReconnectInterval, TimeSpan.FromMilliseconds(-1));
                    }
                }
            }
        }

        private void RecvCallback(IAsyncResult result)
        {
            Contract.Requires<InvalidOperationException>(this.socket != null);

            try
            {
                var state = (RxState)result.AsyncState;
                var rxCount = this.socket.EndReceive(result);
                IMessage msg = null;

                // Did we at least get the length prefix?
                if (rxCount == 4)
                {
                    // Check if we have the entire message
                    var msgSize = 0;
                    if (!Serializer.TryReadLengthPrefix(state.Buffer, 0, 4, PrefixStyle.Fixed32BigEndian, out msgSize))
                    {
                        throw new NotImplementedException();
                    }

                    var bsize = msgSize + 4;

                    // Can we use the existing buffer?
                    if (state.Buffer.Length < bsize)
                    {
                        // Resize rx state buffer
                        state.ResizeBuffers(bsize);
                    }

                    // Read into buffer until entire message is available
                    do
                    {
                        int readRemain = bsize - state.ReceivedBytes;

                        // Blocking receive from socket
                        state.ReceivedBytes += this.socket.Receive(state.Buffer, state.ReceivedBytes, readRemain, SocketFlags.None);
                    }
                    while (state.ReceivedBytes < bsize);

                    if (state.ReceivedBytes == bsize)
                    {
                        // Entire message available
                        msg = Serializer.DeserializeWithLengthPrefix<IMessage>(state.BufferStream, PrefixStyle.Fixed32BigEndian);

                        // Cleanup
                        state.ReceivedBytes = 0;
                        state.BufferStream.Position = 0;
                        state.TotalBytes = -1;

                        // Read the next prefix.
                        this.socket.BeginReceive(state.Buffer, 0, 4, SocketFlags.Peek, this.RecvCallback, state);

                        if (msg != null)
                        {
                            // After next read operation is dispatched, we need to handle the received message
                            this.QueueRxMessageThreadSafe(msg);
                        }
                    }
                }
                else
                {
                    // Length prefix incomplete.
                    // Pause for a millisecond to give the NIC a chance to fill up the buffer if data is enroute
                    Thread.Sleep(1);
                }
            }
            catch (IOException ioe)
            {
            }
            catch (SocketException se)
            {
                if (!this.socket.Connected)
                {
                    // Channel is dead
                    this.OnDisconnected();
                }
            }
        }

        [ContractInvariantMethod]
        private void ContractInvariants()
        {
            Contract.Invariant(this.connectionTarget != null);
        }

        #endregion

        protected class RxState
        {
            public byte[] Buffer;
            public MemoryStream BufferStream;
            public int ReceivedBytes = 0;
            public int TotalBytes = -1;
            public const int BufferSegment = 8192; // 8KiB buffer size

            public RxState()
            {
                this.Buffer = new byte[RxState.BufferSegment];
                this.BufferStream = new MemoryStream(this.Buffer);
            }

            internal void ResizeBuffers(int bsize)
            {
                Array.Resize<byte>(ref this.Buffer, (int)Math.Ceiling((double)bsize / RxState.BufferSegment) * RxState.BufferSegment);
                this.BufferStream.Dispose();
                this.BufferStream = new MemoryStream(this.Buffer);
            }
        }
    }
}
