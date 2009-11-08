// Gear - Copyright © 2009 Will Shelley. All Rights Reserved.
// Released under the Microsoft Reference License - see license.txt for details.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GearEngine
{
    /// <summary>
    /// Processes commands entered as text by the user into game commands.
    /// </summary>
    /// <remarks>
    /// The <see cref="GameConsole"/> class must be supplied with a <see cref="CommandQueue"/>
    /// which is where commands converted from user input are sent.
    /// </remarks>
    public sealed class GameConsole
    {
        #region Constructors - Public
        /// <summary>
        /// Initializes a new instance of the <see cref="GameConsole"/> class.
        /// </summary>
        public GameConsole()
        {
            this.target = new CommandQueue();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="GameConsole"/> class.
        /// </summary>
        /// <param name="target">A <see cref="CommandQueue"/> which parsed commands will be added to.</param>
        /// <exception cref="System.ArgumentNullException">Thrown when the target parameter is null.</exception>
        public GameConsole(CommandQueue target)
        {
            if (target == null)
                throw new ArgumentNullException("target");

            this.target = target;
        }
        #endregion
        #region Fields

        
        private readonly CommandQueue target;

        #endregion

        #region Methods - Public
        /// <summary>
        /// Parses text input from the user into a command, which is then added to the target queue.
        /// </summary>
        /// <param name="input"></param>
        public void Parse(string input)
        {
            throw new GameConsoleParseException();
            //throw new NotImplementedException();
        }
        #endregion
        #region Properties

        #endregion
    }
}
