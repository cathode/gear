/******************************************************************************
 * Gear: A Steampunk Action-RPG - http://trac.gearedstudios.com/gear/         *
 * Copyright © 2009-2011 Will 'cathode' Shelley. All Rights Reserved.         *
 * This software is released under the terms and conditions of the Microsoft  *
 * Reference Source License (MS-RSL). See the 'license.txt' file for details. *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace Gear.Net
{
    /// <summary>
    /// Represents a network connection between two endpoints which provides queue and event-based notifications.
    /// </summary>
    public abstract class Connection
    {
        #region Fields
        /// <summary>
        /// Holds the default port number used by Gear network communication.
        /// </summary>
        public const ushort DefaultPort = 10421;

        /// <summary>
        /// Holds a specical FourCC that is prefixed to each message prior to sending.
        /// </summary>
        public const int MessagePrefix = 'G' << 24 | 'M' << 16 | 'S' << 8 | 'G';

        /// <summary>
        /// Holds the size (in bytes) of an encoded message header.
        /// </summary>
        public const int MessageHeaderSize = 11;
        /// <summary>
        /// Backing field for the <see cref="Connection.Socket"/> property.
        /// </summary>
        private Socket socket;

        private Queue<Message> sendQueue;
        private Queue<Message> receiveQueue;

        private bool isActive;
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new current of the <see cref="Connection"/> class.
        /// </summary>
        protected Connection()
        {
            this.sendQueue = new Queue<Message>();
            this.receiveQueue = new Queue<Message>();
        }
        #endregion
        #region Events
        /// <summary>
        /// Raised when the value of the <see cref="Connection.State"/> property changes, indicating a change in the underlying network socket.
        /// </summary>
        public event EventHandler StateChanged;

        /// <summary>
        /// Raised when a <see cref="Message"/> is received from the remote endpoint, after it has been enqueued to the message receive queue.
        /// </summary>
        public event EventHandler<MessageEventArgs> MessageReceived;
        #endregion
        #region Properties
        /// <summary>
        /// Gets the number of messages currently waiting in the message receive queue.
        /// </summary>
        public int ReceivedMessageCount
        {
            get
            {
                return this.receiveQueue.Count;
            }
        }

        /// <summary>
        /// Gets or sets the underlying <see cref="Socket"/>.
        /// </summary>
        protected Socket Socket
        {
            get
            {
                return this.socket;
            }
            set
            {
                this.socket = value;
            }
        }
        #endregion
        #region Methods
        /// <summary>
        /// Processes all messages queued for sending and scans any received data to ensure that all fully received messages are parsed and queued in the receive queue.
        /// </summary>
        public void Flush()
        {
            while (this.sendQueue.Count > 0)
            {
                var message = this.sendQueue.Dequeue();

                int size = 11; // Message header is 11 bytes.
                foreach (var field in message.Fields)
                    size += 6 + field.Size; // Field header is 5 bytes (per field)

                DataBuffer buffer = new DataBuffer(size, DataBufferMode.NetworkByteOrder);
                buffer.WriteInt32(Connection.MessagePrefix);
                buffer.WriteInt16((short)message.Id);
                buffer.WriteByte((byte)message.Fields.Length);
                buffer.WriteInt32(size - 11); // payload size, all fields and field headers.

                foreach (var field in message.Fields)
                {
                    buffer.WriteInt16((short)field.Id);
                    buffer.WriteInt16(field.Tag);
                    buffer.WriteInt16(field.Size);
                    buffer.Position += field.CopyTo(buffer.Contents, buffer.Position);
                }
                this.socket.BeginSend(buffer.Contents, 0, buffer.Contents.Length, SocketFlags.None, this.SendCallback, message);
            }
        }

        /// <summary>
        /// Dequeues the next <see cref="Message"/> from the receive queue (if any) and returns it.
        /// </summary>
        /// <returns>The first message in the receive queue, or null if the receive queue is empty.</returns>
        public Message Receive()
        {
            if (this.ReceivedMessageCount > 0)
                return this.receiveQueue.Dequeue();

            return null;
        }

        /// <summary>
        /// Queues the specified <see cref="Message"/> for sending.
        /// </summary>
        /// <param name="message">The <see cref="Message"/> to be sent.</param>
        public void Send(Message message)
        {
            this.sendQueue.Enqueue(message);
            this.Flush();
        }

        public bool Start()
        {
            if (!this.socket.Connected)
                return false;

            MessageReceiveState state = new MessageReceiveState();
            state.Buffer = new byte[Connection.MessageHeaderSize];
            this.socket.BeginReceive(state.Buffer, 0, state.Buffer.Length, SocketFlags.None, this.ReceiveAsyncCallbck, state);

            return true;
        }

        /// <summary>
        /// Callback for async Socket.Send calls.
        /// </summary>
        /// <param name="result"></param>
        protected virtual void SendCallback(IAsyncResult result)
        {
            this.socket.EndSend(result);


        }

        protected virtual void ReceiveAsyncCallbck(IAsyncResult result)
        {
            var state = result.AsyncState as MessageReceiveState;
            state.ReceivedBytes += this.socket.EndReceive(result);
            if (!state.HeaderDone)
            {
                if (state.ReceivedBytes < Connection.MessageHeaderSize)
                    this.socket.BeginReceive(state.Buffer, state.ReceivedBytes, Connection.MessageHeaderSize - state.ReceivedBytes, SocketFlags.None, this.ReceiveAsyncCallbck, state);

                DataBuffer buffer = new DataBuffer(state.Buffer, DataBufferMode.NetworkByteOrder);

                var prefix = buffer.ReadInt32();
                if (prefix != Connection.MessagePrefix)
                    throw new NotImplementedException();

                MessageId id = (MessageId)buffer.ReadInt16();
                state.Message = MessageFactory.Current.Create(id);
                state.FieldCount = buffer.ReadByte();
                state.Payload = buffer.ReadInt32();
                state.HeaderDone = true;
                state.Buffer = new byte[state.Payload];
            }

            if (socket.Available < state.Payload)

                this.socket.BeginReceive(state.Buffer, state.ReceivedBytes - 11, socket.Available, SocketFlags.None, this.ReceiveAsyncCallbck, state);

            else
            {
                state.ReceivedBytes += this.socket.Receive(state.Buffer, state.Buffer.Length, SocketFlags.None);

                DataBuffer buffer = new DataBuffer(state.Buffer, DataBufferMode.NetworkByteOrder);
                for (int i = 0; i < state.FieldCount; i++)
                {
                    var fieldId = (FieldKind)buffer.ReadInt16();
                    short tag = buffer.ReadInt16();
                    short length = buffer.ReadInt16();
                    var field = state.Message.GetField(fieldId, tag);
                    buffer.Position += field.CopyFrom(state.Buffer, buffer.Position, length);
                }
            }
            return;
        }
        #endregion
    }
}
