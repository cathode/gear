/******************************************************************************
 * Gear: A Steampunk Action-RPG - http://trac.gearedstudios.com/gear/         *
 * Copyright © 2009-2011 Will 'cathode' Shelley. All Rights Reserved.         *
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
            this.listener = new ConnectionListener();

            this.InitGShell();
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

        /// <summary>
        /// Initializes the <see cref="GShell"/> used by the <see cref="ServerEngine"/>.
        /// </summary>
        private void InitGShell()
        {
            //this.Shell.Register(new GShellCommand("sv_maxplayers", this.
        }
        #endregion
    }
}
