using System;
using System.Diagnostics;
using System.Threading;
using Gear.Commands;

namespace Gear
{
    /// <summary>
    /// Supervises and directs the operation of all subsystems of the Gear engine.
    /// </summary>
    public abstract class GameEngine
    {
        #region Fields
        private readonly CommandQueue input;
        private readonly CommandQueue output;
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
            this.input = new CommandQueue();
            this.output = new CommandQueue();
            this.shell = new GameShell(this.input);

            // Set up event trigger so commands get processed.
            this.Input.NonEmpty += new EventHandler(this.Input_NonEmpty);
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

        /// <summary>
        /// Processes a command.
        /// </summary>
        /// <param name="cmd"></param>
        protected void ProcessCommand(Command cmd)
        {
            if (cmd == null)
                throw new ArgumentNullException("cmd");

            cmd.Execute(this);
        }

        private void Input_NonEmpty(object sender, EventArgs e)
        {
            if (this.Active)
                return;

            ThreadPool.QueueUserWorkItem(new WaitCallback(this.ProcessQueuedInputCallback));
        }

        private void ProcessQueuedInput()
        {
            while (this.Input.Count > 0)
            {
                var current = this.Input.Dequeue();

                this.ProcessCommand(current);
            }
        }

        private void ProcessQueuedInputCallback(object state)
        {
            this.active = true;

            this.ProcessQueuedInput();

            this.active = false;
        }

        private void P_Comment(Command cmd)
        {
            var c = (CommentCommand)cmd;

            this.Shell.Output.WriteLine(c.Comment);
        }
        private void P_Quit(Command cmd)
        {
            var c = (QuitCommand)cmd;

            Process.GetCurrentProcess().Kill();
        }
        private void P_Set(Command cmd)
        {
        }

        #endregion
    }
}
