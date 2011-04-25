/******************************************************************************
 * Rust: A Managed Game Engine - http://trac.gearedstudios.com/rust/          *
 * Copyright © 2009-2011 Will 'cathode' Shelley. All Rights Reserved.         *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rust
{
    public sealed class RustGamePluginAttribute : Attribute
    {
        #region Constructors
        public RustGamePluginAttribute(string gameID, string name)
        {
            this.GameID = new Guid(gameID);
            this.Name = name;
        }
        #endregion
        #region Properties
        /// <summary>
        /// Gets the unique identifier (expressed as a <see cref="System.Guid"/>) of the game assembly.
        /// </summary>
        public Guid GameID
        {
            get;
            set;
        }
        public string Version
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }
        #endregion
    }
}
