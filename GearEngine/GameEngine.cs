// Gear - Copyright © 2009 Will Shelley. All Rights Reserved.
// Released under the Microsoft Reference License - see license.txt for details.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

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
            this.Input.NonEmpty += new EventHandler(Input_NonEmpty);

            this.engineThread = new Thread(new ThreadStart(this.ProcessQueuedInput));
        }

        #endregion
        #region Fields

        private bool active = false;
        private readonly CommandQueue input = new CommandQueue();
        private readonly CommandQueue output = new CommandQueue();
        private Thread engineThread;

        #endregion
        #region Methods

        private void Input_NonEmpty(object sender, EventArgs e)
        {
            if (this.Active)
                return;

            ThreadPool.QueueUserWorkItem(new WaitCallback(this.ProcessQueuedInputCallback));
        }

        private void ProcessQueuedInputCallback(object state)
        {
            this.active = true;
            Console.WriteLine(DateTime.Now.Ticks.ToString() + ": Started queue processing");

            this.ProcessQueuedInput();

            this.active = false;
            Console.WriteLine(DateTime.Now.Ticks.ToString() + ": Ended queue processing");
        }

        private void ProcessQueuedInput()
        {
            while (this.Input.Count > 0)
            {
                var current = this.Input.Dequeue();

                this.ProcessInputCommand(current);
            }
        }
        protected virtual void ProcessInputCommand(Command command)
        {
            Console.WriteLine(command.Id);
        }
        #endregion
        #region Properties
        public bool Active
        {
            get
            {
                return this.active;
            }
        }
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
