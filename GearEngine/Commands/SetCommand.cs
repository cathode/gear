using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GearEngine.Commands
{
    /// <summary>
    /// Represents a command that assigns or alters a session variable.
    /// </summary>
    public sealed class SetCommand : Command
    {
        #region Methods
        public override bool Execute(GameSession session)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region Properties
        /// <summary>
        /// Gets the name of the command.
        /// </summary>
        public override string Name
        {
            get
            {
                return "set";
            }
        }

        /// <summary>
        /// Gets the description of the command.
        /// </summary>
        public override string Description
        {
            get
            {
                return EngineResources.CommandSetDescription;
            }
        }
        #endregion
    }
}
