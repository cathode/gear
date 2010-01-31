/* Copyright © 2009-2010 Will Shelley. All Rights Reserved.
   See the included license.txt file for details. */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gear.Commands
{
    /// <summary>
    /// A command that displays help for a specific topic.
    /// </summary>
    public sealed class HelpCommand : ShellCommand
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="HelpCommand"/>.
        /// </summary>
        public HelpCommand()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HelpCommand"/>
        /// </summary>
        /// <param name="topic">The topic that help is requested for.</param>
        public HelpCommand(string topic)
        {
        }

        #endregion
        #region Fields

        private string topic;

        #endregion
        #region Methods
        /// <summary>
        /// Overridden. Executes the current <see cref="HelpCommand"/>.
        /// </summary>
        /// <param name="engine"></param>
        public override void Execute(GameEngine engine)
        {
            var topicName = (string.IsNullOrEmpty(this.Topic)) ? "help" : this.Topic;

            var topicCmd = GameShell.CreateShellCommand(topicName);
            if (topicCmd != null)
                engine.Shell.Output.WriteLine(topicCmd.HelpInfo ?? EngineResources.ShellErrorNoHelp);
            else
                engine.Shell.Output.WriteLine(EngineResources.ShellErrorUnknownCommand, topicName);
        }
        /// <summary>
        /// Overridden. Parses data into the current shell command.
        /// </summary>
        /// <param name="data"></param>
        public override void ParseData(string data)
        {
            this.topic = data.Trim();
        }

        #endregion
        #region Properties

        /// <summary>
        /// Overridden. Gets the <see cref="CommandId"/> of the current command.
        /// </summary>
        public override ushort Id
        {
            get
            {
                return CommandId.Help;
            }
        }

        /// <summary>
        /// Overridden.
        /// </summary>
        public override bool IsShellOnly
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Overridden. Gets the name of the command.
        /// </summary>
        public override string Name
        {
            get
            {
                return "Help";
            }
        }

        /// <summary>
        /// Gets or sets the topic that help is requested for.
        /// </summary>
        public string Topic
        {
            get
            {
                return this.topic;
            }
            set
            {
                this.topic = value;
            }
        }

        #endregion
    }
}
