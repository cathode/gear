using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gear.Client.Geometry
{
    public interface IPath
    {
        #region Properties
        IEnumerable<Vector3> Points
        {
            get;
        }
        #endregion
    }
}
