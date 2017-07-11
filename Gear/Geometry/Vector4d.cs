/******************************************************************************
 * Gear: An open-world sandbox game for creative people.                      *
 * http://github.com/cathode/gear/                                            *
 * Copyright © 2009-2017 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT        *
 * license. See the included LICENSE file for details.                        *
 *****************************************************************************/
using System;
using System.Runtime.InteropServices;
using System.Diagnostics.Contracts;

namespace Gear.Geometry
{
    /// <summary>
    /// A four-dimensional double-precision floating point vector.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct Vector4d
    {
        #region Fields

        /// <summary>
        /// Backing field for the <see cref="Vector4d.X"/> property.
        /// </summary>
        [FieldOffset(0x00)]
        private readonly double x;

        /// <summary>
        /// Backing field for the <see cref="Vector4d.Y"/> property.
        /// </summary>
        [FieldOffset(0x08)]
        private readonly double y;

        /// <summary>
        /// Backing field for the <see cref="Vector4d.Z"/> property.
        /// </summary>
        [FieldOffset(0x10)]
        private readonly double z;

        /// <summary>
        /// Backing field for the <see cref="Vector4d.W"/> property.
        /// </summary>
        [FieldOffset(0x18)]
        private readonly double w;
        #endregion
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector4d"/> struct.
        /// </summary>
        /// <param name="x">The x-coordinate of the new vector.</param>
        /// <param name="y">The y-coordinate of the new vector.</param>
        /// <param name="z">The z-coordinate of the new vector.</param>
        /// <param name="w">The w-coordinate of the new vector.</param>
        public Vector4d(double x, double y, double z, double w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        #endregion
        #region Indexers
        public double this[int element]
        {
            get
            {
                Contract.Requires(element > -1);
                Contract.Requires(element < 4);

                if (element == 0)
                {
                    return this.x;
                }
                else if (element == 1)
                {
                    return this.y;
                }
                else if (element == 2)
                {
                    return this.z;
                }
                else if (element == 3)
                {
                    return this.w;
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
        }
        #endregion
        #region Properties

        /// <summary>
        /// Gets or sets the x-component of the vector.
        /// </summary>
        public double X
        {
            get
            {
                return this.x;
            }
        }

        /// <summary>
        /// Gets or sets the y-component of the vector.
        /// </summary>
        public double Y
        {
            get
            {
                return this.y;
            }
        }

        /// <summary>
        /// Gets or sets the z-component of the vector.
        /// </summary>
        public double Z
        {
            get
            {
                return this.z;
            }
        }

        /// <summary>
        /// Gets or sets the w-component of the vector.
        /// </summary>
        public double W
        {
            get
            {
                return this.w;
            }
        }

        /// <summary>
        /// Gets the four-dimensional zero vector.
        /// </summary>
        public static Vector4d Zero
        {
            get
            {
                return new Vector4d();
            }
        }

        #endregion
        #region Methods
        public Vector4d Add(Vector4d other)
        {
            return new Vector4d(
                this.x + other.x,
                               this.y + other.y,
                               this.z + other.z,
                               this.w + other.w);
        }

        public static Vector4d Add(Vector4d a, Vector4d b)
        {
            return new Vector4d(
                a.x + b.x,
                               a.y + b.y,
                               a.z + b.z,
                               a.w + b.w);
        }

        public Vector4d Clamp()
        {
            double x = (this.x < 0.0) ? 0.0 : (this.x > 1.0) ? 1.0 : this.x;
            double y = (this.y < 0.0) ? 0.0 : (this.y > 1.0) ? 1.0 : this.y;
            double z = (this.z < 0.0) ? 0.0 : (this.z > 1.0) ? 1.0 : this.z;
            double w = (this.w < 0.0) ? 0.0 : (this.w > 1.0) ? 1.0 : this.w;

            return new Vector4d(x, y, z, w);
        }

        public Vector4d Clamp(double min, double max)
        {
            double x = (this.x < min) ? min : (this.x > max) ? max : this.x;
            double y = (this.y < min) ? min : (this.y > max) ? max : this.y;
            double z = (this.z < min) ? min : (this.z > max) ? max : this.z;
            double w = (this.w < min) ? min : (this.w > max) ? max : this.w;

            return new Vector4d(x, y, z, w);
        }

        public static Vector4d Clamp(Vector4d v)
        {
            double x = (v.x < 0.0) ? 0.0 : (v.x > 1.0) ? 1.0 : v.x;
            double y = (v.y < 0.0) ? 0.0 : (v.y > 1.0) ? 1.0 : v.y;
            double z = (v.z < 0.0) ? 0.0 : (v.z > 1.0) ? 1.0 : v.z;
            double w = (v.w < 0.0) ? 0.0 : (v.w > 1.0) ? 1.0 : v.w;

            return new Vector4d(x, y, z, w);
        }

        public static Vector4d Clamp(Vector4d v, double min, double max)
        {
            double x = (v.x < min) ? min : (v.x > max) ? max : v.x;
            double y = (v.y < min) ? min : (v.y > max) ? max : v.y;
            double z = (v.z < min) ? min : (v.z > max) ? max : v.z;
            double w = (v.w < min) ? min : (v.w > max) ? max : v.w;

            return new Vector4d(x, y, z, w);
        }

        /// <summary>
        /// Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <param name="obj">Another object to compare to.</param>
        /// <returns>true if obj and this instance are the same type and represent the same value; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (obj is Vector4d)
            {
                return this == (Vector4d)obj;
            }

            return false;
        }

        /// <summary>
        /// Determines if two <see cref="Vector4d"/> instances are equal.
        /// </summary>
        /// <param name="first">The first <see cref="Vector4d"/> to compare.</param>
        /// <param name="second">The second <see cref="Vector4d"/> to compare.</param>
        /// <returns>true if both instances represent the same value; otherwise, false.</returns>
        public static bool Equals(Vector4d first, Vector4d second)
        {
            return first == second;
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
        public override int GetHashCode()
        {
            return __HashCode.Calculate(this.x, this.y, this.z, this.w);
        }

        public override string ToString()
        {
            return string.Format("{0}, {1}, {2}, {3}", this.x, this.y, this.z, this.w);
        }

        public Vector4d Normalize()
        {
            throw new NotImplementedException();
        }
        #endregion
        #region Operators

        /// <summary>
        /// Determines if two <see cref="Vector4d"/> instances are equal.
        /// </summary>
        /// <param name="left">The first <see cref="Vector4d"/> to compare.</param>
        /// <param name="right">The second <see cref="Vector4d"/> to compare.</param>
        /// <returns>true if both instances represent the same value; otherwise, false.</returns>
        public static bool operator ==(Vector4d left, Vector4d right)
        {
            return left.w == right.w && left.x == right.x && left.y == right.y && left.z == right.z;
        }

        /// <summary>
        /// Determines if two <see cref="Vector4d"/> instances are inequal.
        /// </summary>
        /// <param name="left">The first <see cref="Vector4d"/> to compare.</param>
        /// <param name="right">The second <see cref="Vector4d"/> to compare.</param>
        /// <returns>true if both instances represent different values; otherwise, false.</returns>
        public static bool operator !=(Vector4d left, Vector4d right)
        {
            return left.w != right.w || left.x != right.x || left.y != right.y || left.z != right.z;
        }

        public static Vector4d operator +(Vector4d left, Vector4d right)
        {
            return new Vector4d(
                left.x + right.x,
                               left.y + right.y,
                               left.z + right.z,
                               left.w + right.w);
        }
        #endregion
    }
}
