// Gear - Copyright © 2009 Will Shelley. All Rights Reserved.
// Released under the Microsoft Reference License - see license.txt for details.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GearEngine.Commands
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
        /// Overridden. Parses data into the current shell command.
        /// </summary>
        /// <param name="data"></param>
        public override void ParseData(string data)
        {
            throw new NotImplementedException();
        }

        #endregion
        #region Properties

        /// <summary>
        /// Overridden. Gets the <see cref="CommandId"/> of the current command.
        /// </summary>
        public override CommandId Id
        {
            get
            {
                return CommandId.Help;
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
