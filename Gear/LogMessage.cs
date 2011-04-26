/******************************************************************************
 * Gear: A Steampunk Action-RPG - http://trac.gearedstudios.com/gear/         *
 * Copyright © 2009-2011 Will 'cathode' Shelley. All Rights Reserved.         *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gear
{
    /// <summary>
    /// Represents a message written to the message log.
    /// </summary>
    public sealed class LogMessage
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="LogMessage"/> class.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="source"></param>
        /// <param name="level"></param>
        public LogMessage(string message, string source, LogMessageGroup level)
        {
            this.Timestamp = DateTime.Now;
            this.Source = source;
            this.Message = message;
            this.Level = level;
        }
        #endregion
        #region Fields
        internal DateTime Timestamp;
        internal string Source;
        internal string Message;
        internal LogMessageGroup Level;
        #endregion
        #region Properties

        #endregion
    }
}
