/******************************************************************************
 * Gear: A Steampunk Action-RPG - http://trac.gearedstudios.com/gear/         *
 * Copyright © 2009-2011 Will 'cathode' Shelley. All Rights Reserved.         *
 * This software is released under the terms and conditions of the Microsoft  *
 * Reference Source License (MS-RSL). See the 'license.txt' file for details. *
 *****************************************************************************/
using System;
using System.Threading;

namespace Gear
{
    /// <summary>
    /// Supervises and directs the operation of all subsystems of the Gear engine.
    /// </summary>
    public abstract class EngineBase
    {
        #region Fields
        private bool active = false;
        private bool isLoaded;
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new current of the <see cref="EngineBase"/> class.
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

        /// <summary>
        /// Raised at regular periodic intervals when repeated tasks should be executed.
        /// </summary>
        public event EventHandler Update;

        /// <summary>
        /// Raised when the engine is started.
        /// </summary>
        public event EventHandler Starting;
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
        public bool IsLoaded
        {
            get
            {
                return this.isLoaded;
            }
        }
        #endregion
        #region Methods
        public bool Load()
        {
            if (this.IsLoaded)
                return true;

            try
            {
                this.OnPreLoad(EventArgs.Empty);
                this.OnPostLoad(EventArgs.Empty);
                this.isLoaded = true;
            }
            catch
            {
                this.isLoaded = false;
            }

            return this.isLoaded;
        }
        /// <summary>
        /// Runs the engine. This method blocks until the engine is terminated.
        /// </summary>
        public void Run()
        {
            if (!this.IsLoaded)
                this.Load();

            this.OnStarting(EventArgs.Empty);
            while (true)
            {
                this.OnUpdate(EventArgs.Empty);
                Thread.Sleep(1);
            }
        }
        /// <summary>
        /// Raises the <see cref="EngineBase.PreLoad"/> event.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnPreLoad(EventArgs e)
        {
            if (this.PreLoad != null)
                this.PreLoad(this, e);
        }

        /// <summary>
        /// Raises the <see cref="EngineBase.PostLoad"/> event.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnPostLoad(EventArgs e)
        {
            if (this.PostLoad != null)
                this.PostLoad(this, e);
        }

        /// <summary>
        /// Raises the <see cref="EngineBase.PlayerConnected"/> event.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnPlayerConnected(PlayerEventArgs e)
        {
            if (this.PlayerConnected != null)
                this.PlayerConnected(this, e);
        }

        /// <summary>
        /// Raises the <see cref="EngineBase.PlayerDisconnected"/> event.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnPlayerDisconnected(PlayerEventArgs e)
        {
            if (this.PlayerDisconnected != null)
                this.PlayerDisconnected(this, e);
        }

        /// <summary>
        /// Raises the <see cref="EngineBase.Update"/> event.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnUpdate(EventArgs e)
        {
            if (this.Update != null)
                this.Update(this, e);
        }

        protected virtual void OnStarting(EventArgs e)
        {
            if (this.Starting != null)
                this.Starting(this, e);
        }
        #endregion
    }
}
