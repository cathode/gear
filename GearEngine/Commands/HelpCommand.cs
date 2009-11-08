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
        #region Properties
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
