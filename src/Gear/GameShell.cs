/******************************************************************************
 * Gear: A Steampunk Action-RPG - http://trac.gearedstudios.com/gear/         *
 * Copyright © 2009-2010 Will 'cathode' Shelley. All Rights Reserved.         *
 * -------------------------------------------------------------------------- *
 * Contributors:                                                              *
 * - Will 'cathode' Shelley <cathode@live.com>                                *
 *****************************************************************************/
using System;
using System.IO;

namespace Gear
{
    public delegate string GameShellCommandCallback(string[] args);

    /// <summary>
    /// Processes commands entered as text by the user into game commands.
    /// </summary>
    public sealed class GameShell
    {
        #region Fields
        /// <summary>
        /// Backing field for <see cref="GameShell.Output"/> property.
        /// </summary>
        private StringWriter output = new StringWriter();

        /// <summary>
        /// Holds the collection of registered <see cref="GameShellCommand"/> items.
        /// </summary>
        private readonly GameShellCommandCollection commands;
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new current of the <see cref="GameShell"/> class.
        /// </summary>
        public GameShell()
        {
            this.commands = new GameShellCommandCollection();
        }
        #endregion
        #region Properties

        public StringWriter Output
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
        #region Methods
        public GameShellCommand GetRegisteredCommand(string name)
        {
            throw new NotImplementedException();
        }
        public void Register(GameShellCommand command)
        {
            if (!this.commands.Contains(command.Name))
                this.commands.Add(command);
        }
        public void Unregister(GameShellCommand command)
        {
            if (this.commands.Contains(command))
                this.commands.Remove(command);
        }
        #endregion
    }
}
