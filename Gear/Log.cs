/******************************************************************************
 * Gear: An open-world sandbox game for creative people.                      *
 * http://github.com/cathode/gear/                                            *
 * Copyright © 2009-2014 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT        *
 * license. See the included LICENSE file for details.                        *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Gear
{

    public delegate void LogMessageHandler(LogMessage message);

    /// <summary>
    /// Provides method of recording runtime-generated events.
    /// </summary>
    public static class Log
    {
        #region Fields
        /// <summary>
        /// Holds the default format string used when outputting log messages.
        /// </summary>
        public const string DefaultFormat = "{0:s} | {1} | {2} | {3}";

        /// <summary>
        /// Holds the default value used for a log message "Source" field.
        /// </summary>
        public const string DefaultSource = "General";

        private static readonly Queue<LogMessage> buffer = new Queue<LogMessage>();
        private static readonly List<LogOutput> outputs = new List<LogOutput>();
        private static readonly List<LogMessageHandler> handlers = new List<LogMessageHandler>();
        private static int threshold = 0;
        #endregion
        #region Methods
        /// <summary>
        /// Writes a message to the log.
        /// </summary>
        /// <param name="message">The message to log.</param>
        public static void Write(string message)
        {
            Log.Write(message, string.Empty, LogMessageGroup.Info);
        }

        /// <summary>
        /// Writes a message to the log.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="source">The source of the message.</param>
        public static void Write(string message, string source)
        {
            Log.Write(message, source, LogMessageGroup.Info);
        }

        /// <summary>
        /// Writes a message to the log.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="source">The source of the message.</param>
        /// <param name="level">The group/category of the message.</param>
        public static void Write(string message, string source, LogMessageGroup level)
        {
            lock (Log.buffer)
            {
                Log.buffer.Enqueue(new LogMessage(message, source, level));
            }

            if (Log.buffer.Count >= Log.threshold)
                Log.Flush();
        }

        /// <summary>
        /// Binds a stream to the log output. Multiple streams can be bound simultaneously.
        /// </summary>
        /// <param name="stream">The stream to bind.</param>
        public static void BindOutput(Stream stream)
        {
            Log.BindOutput(stream, Log.DefaultFormat, LogMessageGroup.All);
        }

        /// <summary>
        /// Binds a stream to the log output. Multiple streams can be bound simultaneously.
        /// </summary>
        /// <param name="stream">The stream to bind.</param>
        /// <param name="format">A custom format string to use for formatting log messages to the stream being bound.</param>
        /// <param name="filter">Indicates which log messages will be output to the stream being bound.</param>
        public static void BindOutput(Stream stream, string format, LogMessageGroup filter)
        {
            lock (Log.outputs)
            {
                if (!Log.outputs.Any(op => op.Stream == stream))
                    Log.outputs.Add(new LogOutput(stream, format, filter));
            }
        }

        public static void BindOutput(LogMessageHandler callback)
        {
            lock (Log.handlers)
            {
                if (!Log.handlers.Contains(callback))
                    Log.handlers.Add(callback);
            }
        }

        /// <summary>
        /// Flushes any queued messages to bound output streams.
        /// </summary>
        public static void Flush()
        {
            lock (Log.buffer)
            {
                while (Log.buffer.Count > 0)
                {
                    var data = Log.buffer.Dequeue();

#if !DEBUG
                    if (data.Level == LogMessageGroup.Debug)
                   
                        continue;
#endif

                    foreach (var handler in Log.handlers)
                        handler(data);

                    foreach (var output in Log.outputs)
                    {
                        var bytes = Encoding.UTF8.GetBytes(string.Format(output.Format, data.Timestamp, data.Level, data.Source, data.Message) + Environment.NewLine);
                        output.Stream.Write(bytes, 0, bytes.Length);
                        output.Stream.Flush();
                    }
                }
            }
        }
        #endregion
    }
}
