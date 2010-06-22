using System;
using System.Collections.Generic;
using System.Linq;
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
