// Gear - Copyright © 2009 Will Shelley. All Rights Reserved.
// Released under the Microsoft Reference License - see license.txt for details.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GearEngine.Commands
{
    /// <summary>
    /// Provides shared functionality for commands that can be invoked from the game shell.
    /// </summary>
    public abstract class ShellCommand : Command
    {
        #region Methods

        /// <summary>
        /// When overridden in a derived class, parses the data in string form and assimilates it into the current shell command.
        /// </summary>
        /// <param name="data">The data to parse.</param>
        public abstract void ParseData(string data);

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

            ShellCommand cmd = ShellCommand.CreateShellCommand(rawCmd);
            cmd.ParseData(rawData);

            return cmd;
        }

        #endregion
        #region Properties

        /// <summary>
        /// Indicates if the current shell command is only used within the user-interactive shell.
        /// </summary>
        /// <remarks>
        /// Shell-only commands bypass the normal command queue target of the game shell.
        /// </remarks>
        public virtual bool IsShellOnly
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Indicates if the results of the shell command should be displayed to the shell.
        /// </summary>
        public virtual bool GeneratesShellOutput
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// When overridden in a derived class, gets the name of the current command.
        /// </summary>
        public abstract string Name
        {
            get;
        }

        /// <summary>
        /// Gets the localized help string that describes the operation of the current command to the user.
        /// </summary>
        public virtual string HelpInfo
        {
            get
            {
                return EngineResources.ResourceManager.GetString("ShellCmdHelp_" + this.Name);
            }
        }

        #endregion
    }
}
