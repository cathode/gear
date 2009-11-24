// Gear - Copyright © 2009 Will Shelley. All Rights Reserved.
// Released under the Microsoft Reference License - see license.txt for details.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gear.Commands;
using System.IO;

namespace Gear
{
    /// <summary>
    /// Processes commands entered as text by the user into game commands.
    /// </summary>
    /// <remarks>
    /// The <see cref="GameShell"/> class must be supplied with a <see cref="CommandQueue"/>
    /// which is where commands converted from user input are sent.
    /// </remarks>
    public sealed class GameShell
    {
        #region Constructors - Public
        /// <summary>
        /// Initializes a new instance of the <see cref="GameShell"/> class.
        /// </summary>
        public GameShell()
        {
            this.target = new CommandQueue();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="GameShell"/> class.
        /// </summary>
        /// <param name="target">A <see cref="CommandQueue"/> which parsed commands will be added to.</param>
        /// <exception cref="System.ArgumentNullException">Thrown when the target parameter is null.</exception>
        public GameShell(CommandQueue target)
        {
            if (target == null)
                throw new ArgumentNullException("target");

            this.target = target;
        }
        #endregion
        #region Fields
        private readonly CommandQueue target;
        private StringWriter output = new StringWriter();
        #endregion
        #region Methods - Public
        /// <summary>
        /// Derives a <see cref="ShellCommand"/> from the specified line of text and enqueues it.
        /// </summary>
        /// <param name="line"></param>
        public void Parse(string line)
        {
            var cmd = GameShell.ParseShellCommand(line);

            this.target.Enqueue(cmd);
        }

        /// <summary>
        /// Creates and returns a <see cref="ShellCommand"/> by name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static ShellCommand CreateShellCommand(string name)
        {
            if (name == null)
                throw new ArgumentNullException("name");

            switch (name.ToLower())
            {
                case "quit":
                    return new QuitCommand();
                case "help":
                    return new HelpCommand();
                case "set":
                    return new SetCommand();
                case "info":
                    return new CommentCommand();

                default:
                    throw new GameShellParseException(string.Format(EngineResources.ShellErrorUnknownCommand, name));
            }
        }

        /// <summary>
        /// Processes a line of text and attempts to extract a shell command instance from the string.
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public static ShellCommand ParseShellCommand(string line)
        {
            if (line == null || line.Trim() == string.Empty)
                return null; // Nothing to process

            line = line.Trim();
            int sp = line.IndexOf(' ');
            string rawCmd = string.Empty;
            string rawData = string.Empty;

            if (sp == -1)
                rawCmd = line;
            else
            {
                rawCmd = line.Substring(0, sp);
                rawData = line.Substring(sp).Trim();
            }

            ShellCommand cmd = GameShell.CreateShellCommand(rawCmd);
            cmd.ParseData(rawData);

            return cmd;
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
    }
}
