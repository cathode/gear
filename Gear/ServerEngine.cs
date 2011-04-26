/******************************************************************************
 * Gear: A Steampunk Action-RPG - http://trac.gearedstudios.com/gear/         *
 * Copyright © 2009-2011 Will 'cathode' Shelley. All Rights Reserved.         *
 * This software is released under the terms and conditions of the Microsoft  *
 * Reference Source License (MS-RSL). See the 'license.txt' file for details. *
 *****************************************************************************/
using Gear.Net;

namespace Gear
{
    /// <summary>
    /// Represents a <see cref="EngineBase"/> implementation used by a Gear server.
    /// </summary>
    public class ServerEngine : EngineBase
    {
        #region Fields
        private readonly ConnectionListener listener;
        #endregion
        #region Constructors
        public ServerEngine()
        {
            this.listener = new ConnectionListener(this);
        }
        #endregion
        #region Properties
        public ConnectionListener Listener
        {
            get
            {
                return this.listener;
            }
        }
        #endregion
        #region Methods
        protected override void OnStarting(System.EventArgs e)
        {
            base.OnStarting(e);

            this.listener.Start();
        }
        #endregion
    }
}
