/******************************************************************************
 * Gear: A Steampunk Action-RPG - http://trac.gearedstudios.com/gear/         *
 * Copyright © 2009-2010 Will 'cathode' Shelley. All Rights Reserved.         *
 * This software is released under the terms and conditions of the Microsoft  *
 * Reference Source License (MS-RSL). See the 'license.txt' file for details. *
 *****************************************************************************/
using System;

namespace Gear.Net
{
    public sealed class ConnectionEventArgs : EventArgs
    {
        #region Fields
        private readonly Connection connection;
        #endregion
        #region Constructors
        public ConnectionEventArgs(Connection connection)
        {
            this.connection = connection;
        }
        #endregion
        #region Properties
        public Connection Connection
        {
            get
            {
                return this.connection;
            }
        }
        #endregion
    }
}
