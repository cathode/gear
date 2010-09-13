/******************************************************************************
 * Gear: A Steampunk Action-RPG - http://trac.gearedstudios.com/gear/         *
 * Copyright © 2009-2010 Will 'cathode' Shelley. All Rights Reserved.         *
 * This software is released under the terms and conditions of the Microsoft  *
 * Reference License (MS-RL). See the 'license.txt' file for details.         *
 *****************************************************************************/
using System;

namespace Gear.Net
{
    /// <summary>
    /// Represents event data for an event involving a <see cref="Message"/>.
    /// </summary>
    public sealed class MessageEventArgs : EventArgs
    {
        #region Fields
        /// <summary>
        /// Backing field for the <see cref="MessageEventArgs.Message"/> property.
        /// </summary>
        private readonly Message message;
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="MessageEventArgs"/> class.
        /// </summary>
        /// <param name="message">The <see cref="Message"/> that was related to the event which was raised.</param>
        public MessageEventArgs(Message message)
        {
            if (message == null)
                throw new ArgumentNullException("message");

            this.message = message;
        }
        #endregion
        #region Properties
        /// <summary>
        /// Gets the <see cref="Message"/> that was related to the event which was raised.
        /// </summary>
        public Message Message
        {
            get
            {
                return this.message;
            }
        }
        #endregion
    }
}
