/******************************************************************************
 * Gear: A Steampunk Action-RPG - http://trac.gearedstudios.com/gear/         *
 * Copyright © 2009-2010 Will 'cathode' Shelley. All Rights Reserved.         *
 * This software is released under the terms and conditions of the Microsoft  *
 * Reference Source License (MS-RSL). See the 'license.txt' file for details. *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using Gear;
using Gear.Net;
using Gear.Net.Messaging;

namespace Gear.Client
{
    public class ClientEngine : EngineBase
    {
        #region Fields
        private ClientConnection connection;
        #endregion
        #region Constructors
        public ClientEngine()
        {

        }
        #endregion
        #region Properties
        public ClientConnection Connection
        {
            get
            {
                return this.connection;
            }
            set
            {
                this.connection = value;
            }
        }
        #endregion
        #region Methods

        #endregion
    }
}
