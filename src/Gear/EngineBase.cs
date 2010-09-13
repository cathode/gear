/******************************************************************************
 * Gear: A Steampunk Action-RPG - http://trac.gearedstudios.com/gear/         *
 * Copyright © 2009-2010 Will 'cathode' Shelley. All Rights Reserved.         *
 * This software is released under the terms and conditions of the Microsoft  *
 * Reference License (MS-RL). See the 'license.txt' file for details.         *
 *****************************************************************************/
using System;

namespace Gear
{
    /// <summary>
    /// Supervises and directs the operation of all subsystems of the Gear engine.
    /// </summary>
    public abstract class EngineBase
    {
        #region Fields
        private bool active = false;
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="EngineBase"/> class.
        /// </summary>
        protected EngineBase()
        {

        }
        #endregion
        #region Events
        /// <summary>
        /// Raised before the engine performs loading tasks.
        /// </summary>
        public event EventHandler PreLoad;

        /// <summary>
        /// Raised after the engine performs loading tasks.
        /// </summary>
        public event EventHandler PostLoad;

        /// <summary>
        /// Raised when a player connects to the game.
        /// </summary>
        public event EventHandler<PlayerEventArgs> PlayerConnected;

        /// <summary>
        /// Raised when a player disconnects from the game.
        /// </summary>
        public event EventHandler<PlayerEventArgs> PlayerDisconnected;
        #endregion
        #region Properties
        /// <summary>
        /// Gets a value indicating whether the engine is actively processing the input queue.
        /// </summary>
        public bool Active
        {
            get
            {
                return this.active;
            }
        }
        #endregion
        #region Methods
        /// <summary>
        /// Raises the <see cref="EngineBase.PreLoad"/> event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnPreLoad(object sender, EventArgs e)
        {
            if (this.PreLoad != null)
                this.PreLoad(sender, e);
        }

        /// <summary>
        /// Raises the <see cref="EngineBase.PostLoad"/> event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnPostLoad(object sender, EventArgs e)
        {
            if (this.PostLoad != null)
                this.PostLoad(this, e);
        }

        /// <summary>
        /// Raises the <see cref="EngineBase.PlayerConnected"/> event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnPlayerConnected(object sender, PlayerEventArgs e)
        {
            if (this.PlayerConnected != null)
                this.PlayerConnected(this, e);
        }

        /// <summary>
        /// Raises the <see cref="EngineBase.PlayerDisconnected"/> event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnPlayerDisconnected(object sender, PlayerEventArgs e)
        {
            if (this.PlayerDisconnected != null)
                this.PlayerDisconnected(this, e);
        }
        #endregion
    }
}
