﻿/******************************************************************************
 * Gear: An open-world sandbox game for creative people.                      *
 * http://github.com/cathode/gear/                                            *
 * Copyright © 2009-2017 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT        *
 * license. See the included LICENSE file for details.                        *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gear.Model
{
    /// <summary>
    /// Represents the locating coordinates of a chunk.
    /// </summary>
    public struct ChunkLocation
    {
        public long ZoneX { get; set; }

        public long ZoneY { get; set; }

        public long ZoneZ { get; set; }

        public byte ChunkId { get; set; }

        public static ChunkLocation FromWorldCoordinates(double x, double y, double z)
        {
            var cloc = new ChunkLocation();

            cloc.ZoneX = 0;
            cloc.ZoneY = 0;
            cloc.ZoneZ = 0;
            cloc.ChunkId = 0;

            return cloc;
        }

        public static bool operator ==(ChunkLocation left, ChunkLocation right)
        {
            return left.ZoneX == right.ZoneX
                && left.ZoneY == right.ZoneY
                && left.ZoneZ == right.ZoneZ
                && left.ChunkId == right.ChunkId;
        }

        public static bool operator !=(ChunkLocation left, ChunkLocation right)
        {
            return left.ZoneX != right.ZoneX
                | left.ZoneY != right.ZoneY
                | left.ZoneZ != right.ZoneZ
                | left.ChunkId != right.ChunkId;
        }
    }
}
