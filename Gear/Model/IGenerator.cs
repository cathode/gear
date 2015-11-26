﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gear.Client.Geometry;

namespace Gear.Model
{
    public interface IGenerator
    {
        World GenerateWorld(int seed);

        
    }
}
