/******************************************************************************
 * Gear: A Steampunk Action-RPG - http://trac.gearedstudios.com/gear/         *
 * Copyright © 2009-2011 Will 'cathode' Shelley. All Rights Reserved.         *
 *****************************************************************************/
using System;
using System.IO;

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

        #endregion
        #region Methods
        /// <summary>
        /// Executes a line (or lines) of input text.
        /// </summary>
        /// <param name="input"></param>
        public void Execute(string input)
        {
            input = input.Trim();
            var cmdWord = input.Substring(0, input.IndexOf(' '));

            var cmd = this.GetRegisteredCommand(cmdWord);
        }

        public GShellCommand GetRegisteredCommand(string name)
        {
            throw new NotImplementedException();
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
            var setCmd = new GShellCommand("set", null);
        }
        #endregion
    }
}
