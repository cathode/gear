/******************************************************************************
 * Gear: A Steampunk Action-RPG - http://trac.gearedstudios.com/gear/         *
 * Copyright © 2009-2011 Will 'cathode' Shelley. All Rights Reserved.         *
 * This software is released under the terms and conditions of the Microsoft  *
 * Reference Source License (MS-RSL). See the 'license.txt' file for details. *
 *****************************************************************************/
using System;
using System.Collections.Generic;

using System.Text;

namespace Gear.Assets
{
    /// <summary>
    /// Represents an entry within a <see cref="PackageIndex"/>
    /// </summary>
    public sealed class PackageIndexEntry
    {
        #region Properties
        public Guid UniqueId
        {
            get;
            set;
        }
        public ulong Offset
        {
            get;
            set;
        }
        public string Path
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
