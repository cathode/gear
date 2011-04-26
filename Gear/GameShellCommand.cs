/************************************************************************
 * Gear: A Steampunk Action-RPG - http://trac.gearedstudios.com/gear/   *
 * Copyright © 2009-2011 Will 'cathode' Shelley. All Rights Reserved.   *
 * -------------------------------------------------------------------- *
 * Contributors:                                                        *
 * - Will 'cathode' Shelley <cathode@live.com>                          *
 ************************************************************************/

namespace Gear
{
    /// <summary>
    /// Defines a game shell command.
    /// </summary>
    public sealed class GameShellCommand
    {
        #region Fields
        /// <summary>
        /// Backing field for the <see cref="GameShellCommand.Name"/> property.
        /// </summary>
        private string name;
        
        /// <summary>
        /// Backing field for the <see cref="GameShellCommand.MinArgs"/> property.
        /// </summary>
        private int minArgs;

        /// <summary>
        /// Backing field for the <see cref="GameShellCommand.MaxArgs"/> property.
        /// </summary>
        private int maxArgs;

        /// <summary>
        /// Backing field for the <see cref="GameShellCommand.Callback"/> property.
        /// </summary>
        private GameShellCommandCallback callback;

        /// <summary>
        /// Backing field for the <see cref="GameShellCommand.Description"/> property.
        /// </summary>
        private string description;
        #endregion
        #region Properties
        /// <summary>
        /// Gets or sets the <see cref="GameShellCommandCallback"/> that is invoked when the user runs the command.
        /// </summary>
        public GameShellCommandCallback Callback
        {
            get
            {
                return this.callback;
            }
            set
            {
                this.callback = value;
            }
        }

        /// <summary>
        /// Gets or sets a string that provides a description of the current <see cref="GameShellCommand"/>, which is displayed when the user requests help for the command.
        /// </summary>
        public string Description
        {
            get
            {
                return this.description;
            }
            set
            {
                this.description = value;
            }
        }

        /// <summary>
        /// Gets or sets the maximum number of arguments that can be present for a successful invocation of the command.
        /// </summary>
        public int MaxArgs
        {
            get
            {
                return this.maxArgs;
            }
            set
            {
                this.maxArgs = value;
            }
        }

        /// <summary>
        /// Gets or sets the minimum number of arguments that must be present for a successful invocation of the command.
        /// </summary>
        public int MinArgs
        {
            get
            {
                return this.minArgs;
            }
            set
            {
                this.minArgs = value;
            }
        }

        /// <summary>
        /// Gets or sets the human-readable name of the current <see cref="GameShellCommand"/>. This is what is typed to invoke the command from the shell.
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }
        #endregion
    }
}
