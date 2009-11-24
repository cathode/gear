// Gear - Copyright © 2009 Will Shelley. All Rights Reserved.
// Released under the Microsoft Reference License - see license.txt for details.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gear.Commands
{
    /// <summary>
    /// Displays a comment to the shell output.
    /// </summary>
    public sealed class CommentCommand : ShellCommand
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentCommand"/> class.
        /// </summary>
        public CommentCommand()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentCommand"/> class.
        /// </summary>
        /// <param name="info">The information associated with the new <see cref="CommentCommand"/>.</param>
        public CommentCommand(string info)
        {
            this.info = info;
        }

        #endregion
        #region Fields

        private string info;

        #endregion
        #region Methods
        /// <summary>
        /// Overridden. Executes the current <see cref="CommentCommand"/>.
        /// </summary>
        /// <param name="engine"></param>
        public override void Execute(GameEngine engine)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Overridden.
        /// </summary>
        /// <param name="data">The data to parse.</param>
        public override void ParseData(string data)
        {
            this.info = data;
        }

        #endregion
        #region Properties
        /// <summary>
        /// Overridden. Gets the command id.
        /// </summary>
        public override ushort Id
        {
            get
            {
                return CommandId.Comment;
            }
        }
        /// <summary>
        /// Gets or sets the informative string associated with the current <see cref="CommentCommand"/>.
        /// </summary>
        public string Comment
        {
            get
            {
                return this.info;
            }
            set
            {
                this.info = value;
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
        /// Overridden. Gets the command name.
        /// </summary>
        public override string Name
        {
            get
            {
                return "Comment";
            }
        }
        #endregion
    }
}
