/******************************************************************************
 * Rust: A Managed Game Engine - http://trac.gearedstudios.com/rust/          *
 * Copyright © 2009-2011 Will 'cathode' Shelley. All Rights Reserved.         *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Rust
{
    /// <summary>
    /// Provides method of recording runtime-generated events.
    /// </summary>
    public sealed class EventLog
    {
        #region Fields
        public const string DefaultFormat = "[{0}] {1}: {3} ({2})";
        public const string DefaultSource = "General";
        private readonly Queue<EventData> buffer;
        private readonly int threshold;
        private readonly List<EventLogOutput> outputs = new List<EventLogOutput>();
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="EventLog"/> class.
        /// </summary>
        public EventLog()
            : this(1)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventLog"/> class.
        /// </summary>
        /// <param name="bufferSize"></param>
        public EventLog(int threshold)
        {
            this.buffer = new Queue<EventData>(threshold);
            this.threshold = threshold;
        }
        #endregion
        #region Methods
        public void Record(string message)
        {
            this.Record(message, string.Empty, EventLevel.Info);
        }
        public void Record(string message, string source)
        {
            this.Record(message, source, EventLevel.Info);
        }
        public void Record(string message, string source, EventLevel level)
        {
            lock (this.buffer)
            {
                this.buffer.Enqueue(new EventData(message, source, level));
            }

            if (this.buffer.Count >= this.threshold)
                this.Flush();
        }
        public void BindOutput(Stream stream)
        {
            this.BindOutput(stream, EventLog.DefaultFormat, EventLevel.All);
        }
        public void BindOutput(Stream stream, string format, EventLevel filter)
        {
            lock (this.outputs)
            {
                if (!this.outputs.Any(op => op.Stream == stream))
                    this.outputs.Add(new EventLogOutput(stream, format, filter));
            }
        }
        public void Flush()
        {
            lock (this.buffer)
            {
                while (this.buffer.Count > 0)
                {
                    var data = this.buffer.Dequeue();

                    foreach (var output in this.outputs)
                    {
                        var bytes = Encoding.UTF8.GetBytes(string.Format(output.Format, data.Timestamp, data.Level, data.Source, data.Message) + Environment.NewLine);
                        output.Stream.Write(bytes, 0, bytes.Length);
                        output.Stream.Flush();
                    }
                }
            }
        }
        #endregion
        #region Types
        public enum EventLevel
        {
            None = 0x00,
            Debug = 0x01,
            Info = 0x02,
            Warning = 0x04,
            Error = 0x08,

            All = 0xFF,
        }
        internal sealed class EventData
        {
            internal EventData(string message, string source, EventLevel level)
            {
                this.Timestamp = DateTime.Now;
                this.Source = source;
                this.Message = message;
                this.Level = level;
            }
            internal DateTime Timestamp;
            internal string Source;
            internal string Message;
            internal EventLevel Level;
        }
        internal sealed class EventLogOutput
        {
            internal EventLogOutput(Stream stream, string format, EventLevel filter)
            {
                this.Stream = stream;
                this.Format = format;
                this.Filter = filter;
            }
            internal readonly Stream Stream;
            internal readonly string Format;
            internal readonly EventLevel Filter;
        }
        #endregion

    }

}
