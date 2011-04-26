using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Gear
{
    public sealed class EventLogOutput
    {
        internal EventLogOutput(Stream stream, string format, LogMessageGroup filter)
        {
            this.Stream = stream;
            this.Format = format;
            this.Filter = filter;
        }
        internal readonly Stream Stream;
        internal readonly string Format;
        internal readonly LogMessageGroup Filter;
    }
}
