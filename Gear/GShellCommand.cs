/******************************************************************************
 * Gear: A Steampunk Action-RPG - http://trac.gearedstudios.com/gear/         *
 * Copyright © 2009-2011 Will 'cathode' Shelley. All Rights Reserved.         *
 *****************************************************************************/
using Gear.ShellCommands;
using System;

namespace Gear
{
    /// <summary>
    /// Represents a command processed by the game engine CLI shell.
    /// </summary>
    public abstract class GShellCommand
    {
        #region Fields
        private readonly string name;
        private readonly string usage;
        private readonly string description;
        #endregion
        #region Constructors
        public GShellCommand()
        {
            var name = ShellResources.ResourceManager.GetString(string.Format("{0}_name", this.GetType().Name));
            if (name == null)
                throw new NotImplementedException();
            else
                this.name = name;

            this.usage = ShellResources.ResourceManager.GetString(string.Format("{0}_usage", this.GetType().Name)) ?? string.Empty;
            this.description = ShellResources.ResourceManager.GetString(string.Format("{0}_description", this.GetType().Name)) ?? string.Empty;
        }
        #endregion
        #region Properties
        /// <summary>
        /// Gets or sets a string that provides a description of the current <see cref="GShellCommand"/>, which is displayed when the user requests help for the command.
        /// </summary>
        public virtual string Description
        {
            get
            {
                return this.description;
            }
        }

        /// <summary>
        /// Gets or sets the maximum number of arguments that can be present for a successful invocation of the command.
        /// </summary>
        public abstract int MaxArgs
        {
            get;
        }

        /// <summary>
        /// Gets or sets the minimum number of arguments that must be present for a successful invocation of the command.
        /// </summary>
        public abstract int MinArgs
        {
            get;
        }

        /// <summary>
        /// Gets or sets the human-readable name of the current <see cref="GShellCommand"/>. This is what is typed to invoke the command from the shell.
        /// </summary>
        public virtual string Name
        {
            get
            {
                return this.name;
            }
        }

        public virtual string Usage
        {
            get
            {
                return this.usage;
            }
        }
        #endregion
        #region Methods
        public abstract bool Execute(GShell shell, string data, UserCredentials credentials);
        #endregion
    }
}
