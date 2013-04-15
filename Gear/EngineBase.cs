/******************************************************************************
 * Gear: A Steampunk Action-RPG - http://trac.gearedstudios.com/gear/         *
 * Copyright © 2009-2011 Will 'cathode' Shelley. All Rights Reserved.         *
 *****************************************************************************/
using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics.Contracts;

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

        /// <summary>
        /// Backing field for the <see cref="ResourceSearchPaths"/> property.
        /// </summary>
        private readonly HashSet<string> resourceSearchPaths;

        /// <summary>
        /// Backing field for the <see cref="PluginSearchPaths"/> property.
        /// </summary>
        private readonly HashSet<string> pluginSearchPaths;

        private readonly HashSet<BlockDefinition> blocks;
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="EngineBase"/> class.
        /// </summary>
        protected EngineBase()
        {
            this.resourceSearchPaths = new HashSet<string>();
            this.pluginSearchPaths = new HashSet<string>();
            this.blocks = new HashSet<BlockDefinition>();
            this.shell = new GShell(this);

            this.WorkingDirectory = Environment.CurrentDirectory;

            if (true) // TODO: evaluate whether we should always do this
            {
                this.RegisterAssetSearchPaths("./Assets/");
                this.RegisterPluginSearchPaths("./Plugins/");
            }
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
                Contract.Ensures(Contract.Result<GShell>() != null);

                return this.shell;
            }
        }

        /// <summary>
        /// Gets or sets the path to the current working directory.
        /// </summary>
        public string WorkingDirectory
        {
            get;
            set;
        }

        /// <summary>
        /// Gets a collection of paths on the local filesystem that are searched for plugins to load into the engine.
        /// </summary>
        public IEnumerable<string> PluginSearchPaths
        {
            get
            {
                Contract.Ensures(Contract.Result<IEnumerable<string>>() != null);

                return this.pluginSearchPaths;
            }
        }

        /// <summary>
        /// Gets a collection of paths on the local filesystem taht are searched for resources / assets to load into the engine.
        /// </summary>
        public IEnumerable<string> ResourceSearchPaths
        {
            get
            {
                Contract.Ensures(Contract.Result<IEnumerable<string>>() != null);
                return this.resourceSearchPaths;
            }
        }

        /// <summary>
        /// Gets or sets the active <see cref="World"/> that the engine will run against.
        /// </summary>
        public World ActiveWorld
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether to use built-in block definitions for basic types of blocks (air, dirt, stone).
        /// </summary>
        public bool UseBuiltinBlocks
        {
            get;
            set;
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
        /// Adds one or more paths to the engine which are searched for load-able plugins when the engine is initialized.
        /// </summary>
        /// <param name="paths"></param>
        public virtual void RegisterPluginSearchPaths(params string[] paths)
        {
            Contract.Requires(paths != null);

            foreach (var path in paths.Where(p => p != null).Select(p => System.IO.Path.GetFullPath(p)))
                this.pluginSearchPaths.Add(path);
        }

        /// <summary>
        /// Adds one or more paths to the engine which are searched for load-able assets when the engine is initialized.
        /// </summary>
        /// <param name="paths"></param>
        public virtual void RegisterAssetSearchPaths(params string[] paths)
        {
            Contract.Requires(paths != null);

            foreach (var path in paths.Where(p => p != null).Select(p => System.IO.Path.GetFullPath(p)))
                this.resourceSearchPaths.Add(path);
        }

        /// <summary>
        /// Registers a block definition with the engine.
        /// </summary>
        /// <param name="blocks"></param>
        public virtual void RegisterBlock(params BlockDefinition[] blocks)
        {
            Contract.Requires(blocks != null);

            foreach (var block in blocks.Where(b => b != null))
                this.blocks.Add(block);
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

            if (this.UseBuiltinBlocks)
            {
                this.RegisterBlock(new BlockDefinition
                {
                    Name = "air",
                    TypeId = 0x0000
                },
                new BlockDefinition
                {
                    Name = "dirt",
                    TypeId = 0x0001
                }
                );
            }
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

        [ContractInvariantMethod]
        private void ContractInvariants()
        {
            Contract.Invariant(this.blocks != null);
            Contract.Invariant(this.pluginSearchPaths != null);
            Contract.Invariant(this.resourceSearchPaths != null);
            Contract.Invariant(this.shell != null);
        }
        #endregion
    }
}
