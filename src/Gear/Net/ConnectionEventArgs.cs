/******************************************************************************
 * Gear: A Steampunk Action-RPG - http://trac.gearedstudios.com/gear/         *
 * Copyright © 2009-2010 Will 'cathode' Shelley. All Rights Reserved.         *
 * This software is released under the terms and conditions of the Microsoft  *
 * Reference Source License (MS-RSL). See the 'license.txt' file for details. *
 *****************************************************************************/
using System;

namespace Gear.Net
{
    /// <summary>
    /// Represents event data for an event involving a <see cref="Connection"/>.
    /// </summary>
    public sealed class ConnectionEventArgs : EventArgs
    {
        #region Fields
        /// <summary>
        /// Backing field for the <see cref="ConnectionEventArgs.Connection"/> property.
        /// </summary>
        private readonly Connection connection;

        /// <summary>
        /// Backing field for the <see cref="ConnectionEventArgs.Handled"/> property.
        /// </summary>
        private bool handled;
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new current of the <see cref="ConnectionEventArgs"/> class.
        /// </summary>
        /// <param name="connection">The <see cref="Connection"/>.</param>
        public ConnectionEventArgs(Connection connection)
        {
            this.connection = connection;
        }
        #endregion
        #region Properties

        /// <summary>
        /// Gets the <see cref="Connection"/>.
        /// </summary>
        public Connection Connection
        {
            get
            {
                return this.connection;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the connection was handled by a subscriber of the event.
        /// </summary>
        public bool Handled
        {
            get
            {
                return this.handled;
            }
            set
            {
                this.handled = value;
            }
        }
        #endregion
    }
}
