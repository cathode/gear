using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rust
{
    public sealed class LogMessage
    {
        internal LogMessage(string message, string source, LogMessageGroup level)
        {
            this.Timestamp = DateTime.Now;
            this.Source = source;
            this.Message = message;
            this.Level = level;
        }
        internal DateTime Timestamp;
        internal string Source;
        internal string Message;
        internal LogMessageGroup Level;
    }
}
