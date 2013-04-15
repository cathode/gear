/******************************************************************************
 * Gear: A game of block-based sandbox fun. http://github.com/cathode/gear/   *
 * Copyright © 2009-2013 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the Microsoft  *
 * Reference Source License (MS-RSL). See the 'license.txt' file for details. *
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
