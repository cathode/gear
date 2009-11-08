// Gear - Copyright © 2009 Will Shelley. All Rights Reserved.
// Released under the Microsoft Reference License - see license.txt for details.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GearEngine.Commands;

namespace GearEngine
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
        #endregion
        #region Methods - Public
        /// <summary>
        /// Parses text input from the user into a command, which is then added to the target queue.
        /// </summary>
        /// <param name="line"></param>
        public void Parse(string line)
        {
            if (line == null || line.Trim() == string.Empty)
                return; // Nothing to process

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

            ShellCommand cmd;

            switch (rawCmd.ToLower())
            {
                case "quit":
                    cmd = new QuitCommand();
                    break;
                case "help":
                    cmd = new HelpCommand();
                    break;
                case "set":
                    cmd = new SetCommand();
                    break;

                default:
                    throw new GameShellParseException(string.Format(EngineResources.ShellErrorUnknownCommand, rawCmd));
            }

            cmd.ParseData(rawData);

            throw new GameShellParseException();
            //throw new NotImplementedException();
        }
        #endregion
        #region Properties

        #endregion
    }
}
