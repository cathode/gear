// Gear - Copyright © 2009 Will Shelley. All Rights Reserved.
// Released under the Microsoft Reference License - see license.txt for details.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GearEngine
{
    /// <summary>
    /// Supervises and directs the operation of all subsystems of the Gear engine.
    /// </summary>
    public abstract class GameEngine
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="GameEngine"/> class.
        /// </summary>
        protected GameEngine()
        {
        }
        #endregion
        #region Fields
        private readonly CommandQueue input = new CommandQueue();
        private readonly CommandQueue output = new CommandQueue();
        #endregion

        #region Properties
        /// <summary>
        /// Gets the <see cref="CommandQueue"/> where input commands are retrieved from.
        /// </summary>
        public CommandQueue Input
        {
            get
            {
                return this.input;
            }
        }
        /// <summary>
        /// Gets the <see cref="CommandQueue"/> where output commands are sent to.
        /// </summary>
        public CommandQueue Output
        {
            get
            {
                return this.output;
            }
        }
        #endregion
    }
}
