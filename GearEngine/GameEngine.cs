// Gear - Copyright © 2009 Will Shelley. All Rights Reserved.
// Released under the Microsoft Reference License - see license.txt for details.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;

using GearEngine.Commands;

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
            this.input = new CommandQueue();
            this.output = new CommandQueue();
            this.shell = new GameShell(this.input);

            this.Input.NonEmpty += new EventHandler(Input_NonEmpty);

            this.engineThread = new Thread(new ThreadStart(this.ProcessQueuedInput));
        }

        #endregion
        #region Fields

        private bool active = false;
        private readonly CommandQueue input;
        private readonly CommandQueue output;
        private readonly GameShell shell;
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

            this.ProcessQueuedInput();

            this.active = false;
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
            switch (command.Id)
            {
                case CommandId.Comment:
                    this.Shell.Output.WriteLine(((CommentCommand)command).Comment);
                    break;

                case CommandId.Help:
                    var topicName = ((HelpCommand)command).Topic;
                    if (string.IsNullOrEmpty(topicName))
                        topicName = "help";
                    var topic = GameShell.CreateShellCommand(topicName);
                    if (topic != null)
                        this.Shell.Output.WriteLine(topic.HelpInfo ?? "No help available for this command.");
                    break;

                case CommandId.Quit:
                    Process.GetCurrentProcess().Kill();
                    break;
            }
            
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

        public GameShell Shell
        {
            get
            {
                return this.shell;
            }
        }
        #endregion
    }
}
