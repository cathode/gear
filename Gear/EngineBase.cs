/******************************************************************************
 * Gear: A Steampunk Action-RPG - http://trac.gearedstudios.com/gear/         *
 * Copyright © 2009-2011 Will 'cathode' Shelley. All Rights Reserved.         *
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
        /// <summary>
        /// Backing field for the <see cref="EngineBase.IsRunning"/> property.
        /// </summary>
        private bool isRunning = false;

        /// <summary>
        /// Backing field for the <see cref="EngineBase.IsInitialized"/> property.
        /// </summary>
        private bool isInitialized;

        /// <summary>
        /// Backing field for the <see cref="EngineBase.Shell"/> property.
        /// </summary>
        private GShell shell;
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
        public event EventHandler Initializing;

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
        /// Gets a value indicating whether the engine is running.
        /// </summary>
        public bool IsRunning
        {
            get
            {
                return this.isRunning;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the current <see cref="EngineBase"/> has been initialized and is ready to be run.
        /// </summary>
        public bool IsInitialized
        {
            get
            {
                return this.isInitialized;
            }
        }

        /// <summary>
        /// Gets the <see cref="GShell"/> instance that processes player-input commands for the current <see cref="EngineBase"/>.
        /// </summary>
        public GShell Shell
        {
            get
            {
                return this.shell;
            }
        }
        #endregion
        #region Methods
        /// <summary>
        /// Initializes the <see cref="EngineBase"/> instance.
        /// </summary>
        /// <returns>true if the initialization succeeded; otherwise false.</returns>
        public bool Initialize()
        {
            if (this.IsInitialized)
                return true;

            this.shell = new GShell(this);

            try
            {
                this.OnInitializing(EventArgs.Empty);
                this.isInitialized = true;
            }
            catch
            {
                this.isInitialized = false;
            }

            return this.isInitialized;
        }

        /// <summary>
        /// Runs the engine. This method blocks until the engine is terminated.
        /// </summary>
        public virtual void Run()
        {
            if (!this.IsInitialized)
                this.Initialize();

            this.OnStarting(EventArgs.Empty);
            while (true)
            {
                //Log.Write("Update.", "engine", LogMessageGroup.Debug);
                this.OnUpdate(EventArgs.Empty);
                Thread.Sleep(100);
            }
        }

        /// <summary>
        /// Raises the <see cref="EngineBase.Initializing"/> event.
        /// </summary>
        /// <param name="e">Event data associated with the event.</param>
        protected virtual void OnInitializing(EventArgs e)
        {
            if (this.Initializing != null)
                this.Initializing(this, e);
        }

        /// <summary>
        /// Raises the <see cref="EngineBase.PlayerConnected"/> event.
        /// </summary>
        /// <param name="e">Event data associated with the event.</param>
        protected virtual void OnPlayerConnected(PlayerEventArgs e)
        {
            if (this.PlayerConnected != null)
                this.PlayerConnected(this, e);
        } 

        /// <summary>
        /// Raises the <see cref="EngineBase.PlayerDisconnected"/> event.
        /// </summary>
        /// <param name="e">Event data associated with the event.</param>
        protected virtual void OnPlayerDisconnected(PlayerEventArgs e)
        {
            if (this.PlayerDisconnected != null)
                this.PlayerDisconnected(this, e);
        }

        /// <summary>
        /// Raises the <see cref="EngineBase.Update"/> event.
        /// </summary>
        /// <param name="e">Event data associated with the event.</param>
        protected virtual void OnUpdate(EventArgs e)
        {
            if (this.Update != null)
                this.Update(this, e);
        }

        /// <summary>
        /// Raises the <see cref="EngineBase.Starting"/> event.
        /// </summary>
        /// <param name="e">Event data associated with the event.</param>
        protected virtual void OnStarting(EventArgs e)
        {
            if (this.Starting != null)
                this.Starting(this, e);
        }
        #endregion
    }
}
