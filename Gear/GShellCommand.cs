/******************************************************************************
 * Gear: A game of block-based sandbox fun. http://github.com/cathode/gear/   *
 * Copyright © 2009-2013 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the Microsoft  *
 * Reference Source License (MS-RSL). See the 'license.txt' file for details. *
 *****************************************************************************/
using System;
using Gear.ShellCommands;

namespace Gear
{
    /// <summary>
    /// Represents a command processed by the game engine CLI shell.
    /// </summary>
    public abstract class GShellCommand
    {
        #region Fields
        /// <summary>
        /// Backing field for the <see cref="GShellCommand.Name"/> property.
        /// </summary>
        private readonly string name;

        /// <summary>
        /// Backing field for the <see cref="GShellCommand.Usage"/> property.
        /// </summary>
        private readonly string usage;

        /// <summary>
        /// Backing field for the <see cref="GShellCommand.Description"/> property.
        /// </summary>
        private readonly string description;
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="GShellCommand"/> class.
        /// </summary>
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
        /// Gets a string that provides a description of what the command does. This property is localized.
        /// </summary>
        public virtual string Description
        {
            get
            {
                return this.description;
            }
        }

        /// <summary>
        /// Gets the maximum number of arguments that can be present for a successful invocation of the command.
        /// </summary>
        public abstract int MaxArgs
        {
            get;
        }

        /// <summary>
        /// Gets the minimum number of arguments that must be present for a successful invocation of the command.
        /// </summary>
        public abstract int MinArgs
        {
            get;
        }

        /// <summary>
        /// Gets the name of the command, which is what is used to invoke the command via the command-line engine shell. This property is localized.
        /// </summary>
        public virtual string Name
        {
            get
            {
                return this.name;
            }
        }

        /// <summary>
        /// Gets the message that is displayed to the player which describes how the command and it's arguments are used. This property is localized.
        /// </summary>
        public virtual string Usage
        {
            get
            {
                return this.usage;
            }
        }
        #endregion
        #region Methods
        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="shell">The <see cref="GShell"/> instance to execute against.</param>
        /// <param name="data">Any arguments or data provided by the player.</param>
        /// <param name="credentials">The credentials of the player that invoked the command.</param>
        /// <returns>true if the command executed without any errors; otherwise false.</returns>
        public abstract bool Execute(GShell shell, string data, PlayerCredentials credentials);
        #endregion
    }
}
