using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gear.Client.Modeling
{
    public interface IUnaryGeometryOperation
    {
        Gear.Client.Geometry.Mesh3 Input
        {
            get;
            set;
        }
    }
}
