/* Copyright © 2009-2010 Will Shelley. All Rights Reserved.
   See the included license.txt file for details. */
using System;
using System.Collections.Generic;

using System.Text;
using System.Runtime.Serialization;

namespace Gear
{
    /// <summary>
    /// Represents an exception that occurs when a user supplies an incorrect 
    /// </summary>
    public sealed class GameShellParseException : Exception
    {
        #region Constructors
        /// <summary>
        /// Initializes a new current of the <see cref="GameShellParseException"/> class.
        /// </summary>
        public GameShellParseException()
        {

        }
        /// <summary>
        /// Initializes a new current of the <see cref="GameShellParseException"/> class.
        /// </summary>
        /// <param name="message"></param>
        public GameShellParseException(string message)
            : base(message)
        {

        }
        /// <summary>
        /// Initializes a new current of the <see cref="GameShellParseException"/> class.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="inner"></param>
        public GameShellParseException(string message, Exception inner)
            : base(message, inner)
        {

        }
        #endregion
    }
}
