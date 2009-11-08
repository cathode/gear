﻿// Gear - Copyright © 2009 Will Shelley. All Rights Reserved.
// Released under the Microsoft Reference License - see license.txt for details.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GearEngine.Commands;
using System.IO;

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
        private StringBuilder output = new StringBuilder();
        #endregion
        #region Methods - Private
        private void ProcessShellOnly(ShellCommand cmd)
        {
            if (this.output == null)
                return; // No destination for shell output.
            
            switch (cmd.Id)
            {
                case CommandId.Comment:
                    this.Output.AppendLine(((CommentCommand)cmd).Comment);
                    break;
                case CommandId.Help:
                    var topicName = ((HelpCommand)cmd).Topic;
                    if (string.IsNullOrEmpty(topicName))
                        topicName = "help";
                    var topic = ShellCommand.CreateShellCommand(topicName);
                    if (topic != null)
                        this.Output.AppendLine(topic.HelpInfo ?? "No help available for this command.");
                    break;
            }
        }
        #endregion
        #region Methods - Public
        public void Parse(string line)
        {
            var cmd = ShellCommand.ParseShellCommand(line);

            if (cmd.GeneratesShellOutput)
                this.ProcessShellOnly(cmd);
            if (!cmd.IsShellOnly)
                this.target.Enqueue(cmd);
        }
        #endregion
        #region Properties
        public StringBuilder Output
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
