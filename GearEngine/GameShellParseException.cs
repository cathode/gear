// Gear - Copyright © 2009 Will Shelley. All Rights Reserved.
// Released under the Microsoft Reference License - see license.txt for details.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace GearEngine
{
    /// <summary>
    /// Represents an exception that occurs when a user supplies an incorrect 
    /// </summary>
    public sealed class GameShellParseException : Exception
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="GameShellParseException"/> class.
        /// </summary>
        public GameShellParseException()
        {

        }
        /// <summary>
        /// Initializes a new instance of the <see cref="GameShellParseException"/> class.
        /// </summary>
        /// <param name="message"></param>
        public GameShellParseException(string message)
            : base(message)
        {

        }
        /// <summary>
        /// Initializes a new instance of the <see cref="GameShellParseException"/> class.
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
