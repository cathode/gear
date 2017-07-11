/******************************************************************************
 * Gear: An open-world sandbox game for creative people.                      *
 * http://github.com/cathode/gear/                                            *
 * Copyright © 2009-2017 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT        *
 * license. See the included LICENSE file for details.                        *
 *****************************************************************************/
namespace Gear.Geometry
{
    /// <summary>
    /// Represents a vertex of a polygon in two-dimensional space.
    /// </summary>
    public sealed class Vertex2d 
    {
        #region Fields - Private
        /// <summary>
        /// Backing field for the <see cref="Vertex2d.Color"/> property.
        /// </summary>
        private Vector4f color;
        
        /// <summary>
        /// Backing field for the <see cref="Vertex2d.Flags"/> property.
        /// </summary>
        private VertexFlags flags;

        /// <summary>
        /// Backing field for the <see cref="Vertex2d.X"/> property.
        /// </summary>
        private double x;       
        
        /// <summary>
        /// Backing field for the <see cref="Vertex2d.Y"/> property.
        /// </summary>
        private double y;
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Vertex2d"/> class.
        /// </summary>
        public Vertex2d()
        {
        }      
        
        /// <summary>
        /// Initializes a new instance of the <see cref="Vertex2d"/> class.
        /// </summary>
        /// <param name="x">The x-coordinate of the new vertex.</param>
        /// <param name="y">The y-coordinate of the new vertex.</param>
        public Vertex2d(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
        #endregion
        #region Properties - Public
        /// <summary>
        /// Gets or sets the color of the vertex.
        /// </summary>
        public Vector4f Color
        {
            get
            {
                return this.color;
            }
            set
            {
                this.color = value;
            }
        }

        public VertexFlags Flags
        {
            get
            {
                return this.flags;
            }
            set
            {
                this.flags = value;
            }
        }

        /// <summary>
        /// Gets or sets the x-coordinate of the current <see cref="Vertex2d"/>.
        /// </summary>
        public double X
        {
            get
            {
                return this.x;
            }
            set
            {
                this.x = value;
            }
        }     
        
        /// <summary>
        /// Gets or sets the y-coordinate of the current <see cref="Vertex2d"/>.
        /// </summary>
        public double Y
        {
            get
            {
                return this.y;
            }
            set
            {
                this.y = value;
            }
        }
        #endregion


        public Vector2d ToVector2()
        {
            throw new System.NotImplementedException();
        }
    }
}
