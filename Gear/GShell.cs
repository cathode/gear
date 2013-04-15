/******************************************************************************
 * Gear: A Steampunk Action-RPG - http://trac.gearedstudios.com/gear/         *
 * Copyright © 2009-2011 Will 'cathode' Shelley. All Rights Reserved.         *
 *****************************************************************************/
using System;
using System.IO;
using Gear.ShellCommands;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Gear
{
    public delegate string GameShellCommandCallback(string[] args);

    /// <summary>
    /// Provides a CLI interface/shell around the game engine.
    /// </summary>
    public sealed class GShell
    {
        #region Fields
        /// <summary>
        /// Backing field for <see cref="GShell.Output"/> property.
        /// </summary>
        private StringWriter output = new StringWriter();

        /// <summary>
        /// Holds the collection of registered <see cref="GShellCommand"/> items.
        /// </summary>
        private readonly GameShellCommandCollection commands;

        private readonly EngineBase engine;
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new current of the <see cref="GShell"/> class.
        /// </summary>
        public GShell(EngineBase engine)
        {
            this.engine = engine;
            this.commands = new GameShellCommandCollection();

            this.RegisterBuiltinCommands();
        }
        #endregion
        #region Properties

        public StringWriter Output
        {
            get
            {
                return this.output;
            }
            set
            {
                this.output = value;
            }
        }

        public System.Collections.ObjectModel.ReadOnlyCollection<GShellCommand> RegisteredCommands
        {
            get
            {
                return new System.Collections.ObjectModel.ReadOnlyCollection<GShellCommand>(this.commands);
            }
        }
        #endregion
        #region Methods
        /// <summary>
        /// Executes a line (or lines) of input text.
        /// </summary>
        /// <param name="input"></param>
        public void Parse(string input)
        {
            if (string.IsNullOrEmpty(input))
                return;

            input = input.Trim();
            var i = input.IndexOf(' ');
            string cmdWord = string.Empty;
            string cmdData = string.Empty;
            if (i < 0)
            {
                cmdWord = input;
            }
            else
            {
                cmdWord = input.Substring(0, i);
                cmdData = input.Substring(i);
            }
            var cmd = this.GetRegisteredCommand(cmdWord);

            if (cmd != null)
                cmd.Execute(this, cmdData, null);
        }

        public GShellCommand GetRegisteredCommand(string name)
        {
            if (string.IsNullOrEmpty(name))
                return null;

            if (this.commands.Contains(name))
                return this.commands[name];
            else
                return null;
        }

        /// <summary>
        /// Registers a command that can be processed by the current <see cref="GShell"/> instance.
        /// </summary>
        /// <param name="command"></param>
        public void Register(GShellCommand command)
        {
            if (!this.commands.Contains(command.Name))
                this.commands.Add(command);
        }

        /// <summary>
        /// Registers commands that can be processed by the current <see cref="GShell"/> instance.
        /// </summary>
        /// <param name="commands"></param>
        public void Register(params GShellCommand[] commands)
        {
            foreach (var cmd in commands)
                this.Register(cmd);
        }
        public void Unregister(GShellCommand command)
        {
            if (this.commands.Contains(command))
                this.commands.Remove(command);
        }

        private void RegisterBuiltinCommands()
        {
            try
            {
                this.Register(new HelpCommand(),
                    new ListCommand(),
                    new SetCommand(),
                    new SayCommand(),
                    new ExitCommand());
            }
            catch
            {
                Log.Write("Failed registering one or more builtin commands.");
            }
        }

        public void Write(string message)
        {
            this.output.WriteLine(message);
        }

        [ContractInvariantMethod]
        private void Invariants()
        {
            Contract.Invariant(this.commands != null);
            Contract.Invariant(this.engine != null);
            Contract.Invariant(this.output != null);
        }
        #endregion
    }
}
