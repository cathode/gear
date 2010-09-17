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
    /// Represents event data for an event involving 
    /// </summary>
    public sealed class ConnectionStateEventArgs : EventArgs
    {
        #region Fields
        private ConnectionState previous;
        private ConnectionState current;
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new current of the <see cref="ConnectionStateEventArgs"/> class.
        /// </summary>
        /// <param name="state"></param>
        public ConnectionStateEventArgs(ConnectionState state)
        {
            this.current = state;
        }
        public ConnectionStateEventArgs(ConnectionState previous, ConnectionState current)
        {
            this.previous = previous;
            this.current = current;
        }
        #endregion
        #region Properties
        public ConnectionState Previous
        {
            get
            {
                return this.previous;
            }
            set
            {
                this.previous = value;
            }
        }
        public ConnectionState Current
        {
            get
            {
                return this.current;
            }
            set
            {
                this.current = value;
            }
        }
        #endregion
    }
}
