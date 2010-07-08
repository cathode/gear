/* Gear - A Steampunk Action-RPG --- http://trac.gearedstudios.com/gear/
 * Copyright © 2009-2010 Will 'cathode' Shelley. All Rights Reserved. */
using System;
using System.Diagnostics;
using System.Threading;

namespace Gear
{
    /// <summary>
    /// Supervises and directs the operation of all subsystems of the Gear engine.
    /// </summary>
    public abstract class GameEngine
    {
        #region Fields
        //private readonly CommandQueue input;
        //private readonly CommandQueue output;
        private readonly GameShell shell;
        private bool active = false;
        
        #endregion
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GameEngine"/> class.
        /// </summary>
        protected GameEngine()
        {
            // Initialize readonly fields
            this.shell = new GameShell();

            // Set up event trigger so commands get processed.
        }

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

        /// <summary>
        /// Gets the <see cref="GameShell"/> that provides a CLI interaction with the engine.
        /// </summary>
        public GameShell Shell
        {
            get
            {
                return this.shell;
            }
        }
        #endregion
        #region Methods

        #endregion
    }
}
