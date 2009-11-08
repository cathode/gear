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
    public sealed class GameConsoleParseException : Exception
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="GameConsoleParseException"/> class.
        /// </summary>
        public GameConsoleParseException()
        {

        }
        /// <summary>
        /// Initializes a new instance of the <see cref="GameConsoleParseException"/> class.
        /// </summary>
        /// <param name="message"></param>
        public GameConsoleParseException(string message)
            : base(message)
        {

        }
        /// <summary>
        /// Initializes a new instance of the <see cref="GameConsoleParseException"/> class.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="inner"></param>
        public GameConsoleParseException(string message, Exception inner)
            : base(message, inner)
        {

        }
        #endregion
    }
}
