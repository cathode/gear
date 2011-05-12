/******************************************************************************
 * Gear: A Steampunk Action-RPG - http://trac.gearedstudios.com/gear/         *
 * Copyright © 2009-2011 Will 'cathode' Shelley. All Rights Reserved.         *
 *****************************************************************************/
using System.IO;

namespace Gear
{
    /// <summary>
    /// Represents a log output binding.
    /// </summary>
    public sealed class LogOutput
    {
        #region Constructors
       
        public LogOutput(Stream stream, string format, LogMessageGroup filter)
        {
            this.Stream = stream;
            this.Format = format;
            this.Filter = filter;
        }
        #endregion
        #region Properties
        public Stream Stream
        {
            get;
            set;
        }
        public string Format
        {
            get;
            set;
        }
        public LogMessageGroup Filter
        {
            get;
            set;
        }
        #endregion
    }
}
