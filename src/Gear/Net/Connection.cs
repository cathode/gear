/******************************************************************************
 * Gear: A Steampunk Action-RPG - http://trac.gearedstudios.com/gear/         *
 * Copyright © 2009-2010 Will 'cathode' Shelley. All Rights Reserved.         *
 * This software is released under the terms and conditions of the Microsoft  *
 * Reference License (MS-RL). See the 'license.txt' file for details.         *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace Gear.Net
{
    /// <summary>
    /// Represents a network connection between two endpoints which provides queue and event-based notifications.
    /// </summary>
    public abstract class Connection
    {
        #region Fields
        /// <summary>
        /// Holds the default port number used by Gear network communication.
        /// </summary>
        public const ushort DefaultPort = 10421;

        /// <summary>
        /// Backing field for the <see cref="Connection.State"/> property.
        /// </summary>
        private ConnectionState state;
        #endregion
        #region Constructors
        protected Connection()
        {
        }
        #endregion
        #region Properties
        /// <summary>
        /// Gets or sets the <see cref="ConnectionState"/> of the current <see cref="Connection"/>.
        /// </summary>
        public ConnectionState State
        {
            get
            {
                return this.state;
            }
            protected set
            {
                this.state = value;
                this.OnStateChanged(this, EventArgs.Empty);
            }
        }
        #endregion
        #region Events
        /// <summary>
        /// Raised when the value of the <see cref="Connection.State"/> property changes, indicating a change in the underlying network socket.
        /// </summary>
        public event EventHandler StateChanged;
        #endregion
        #region Methods
        /// <summary>
        /// Raises the <see cref="Connection.StateChanged"/> event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnStateChanged(object sender, EventArgs e)
        {
            if (this.StateChanged != null)
                this.StateChanged(sender, e);
        }
        #endregion
    }
}
