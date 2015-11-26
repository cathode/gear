/******************************************************************************
 * Gear: An open-world sandbox game for creative people.                      *
 * http://github.com/cathode/gear/                                            *
 * Copyright © 2009-2016 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT        *
 * license. See the included LICENSE file for details.                        *
 *****************************************************************************/
namespace Gear.Modeling
{
    /// <summary>
    /// Represents modes for calculating the radius of a polygon or polygonal object.
    /// </summary>
    public enum RadiusMode
    {
        /// <summary>
        /// The radius is calculated from the center of the object to each vertex of the polygon.
        /// </summary>
        Vertex = 0x0,

        /// <summary>
        /// The radius is calculated from the center of the object to the midpoint of each edge of the polygon.
        /// </summary>   
        Edge = 0x1,

        /// <summary>
        /// The radicus is calculated from the center of the object to the incenter of the face.
        /// </summary>
        Face = 0x2,
    }
}
