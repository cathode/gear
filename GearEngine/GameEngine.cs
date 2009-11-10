﻿// Gear - Copyright © 2009 Will Shelley. All Rights Reserved.
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
    public delegate void CommandProcessor(Command cmd);

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
            // Initialize readonly fields
            this.input = new CommandQueue();
            this.output = new CommandQueue();
            this.shell = new GameShell(this.input);
            this.processors = new Dictionary<int, CommandProcessor>();

            // Set up event trigger so commands get processed.
            this.Input.NonEmpty += new EventHandler(Input_NonEmpty);

            // Register command processors for generic commands
            this.RegisterCommandProcessor(CommandId.Comment, new CommandProcessor(this.P_Comment));
            this.RegisterCommandProcessor(CommandId.Help, new CommandProcessor(this.P_Help));
            this.RegisterCommandProcessor(CommandId.Quit, new CommandProcessor(this.P_Quit));
            this.RegisterCommandProcessor(CommandId.Set, new CommandProcessor(this.P_Set));
        }

        #endregion
        #region Fields

        private bool active = false;
        private readonly CommandQueue input;
        private readonly CommandQueue output;
        private readonly GameShell shell;
        private readonly Dictionary<int, CommandProcessor> processors;
        #endregion
        #region Methods - Private

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
        private void P_Help(Command cmd)
        {
            var c = (HelpCommand)cmd;

            var topicName = c.Topic;
            if (string.IsNullOrEmpty(topicName))
                topicName = "help";
            var topic = GameShell.CreateShellCommand(topicName);
            if (topic != null)
                this.Shell.Output.WriteLine(topic.HelpInfo ?? "No help available for this command.");
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
        #region Methods - Protected

        /// <summary>
        /// Processes a command.
        /// </summary>
        /// <param name="cmd"></param>
        protected void ProcessCommand(Command cmd)
        {
            if (this.processors.ContainsKey(cmd.Id))
                this.processors[cmd.Id](cmd);
        }

        protected void RegisterCommandProcessor(int id, CommandProcessor callback)
        {
            if (!this.processors.ContainsKey(id))
                this.processors.Add(id, callback);
            else
                this.processors[id] = callback;
        }

        #endregion
        #region Properties

        /// <summary>
        /// Indicates if the engine is actively processing the input queue.
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
    }
}