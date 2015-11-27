/******************************************************************************
 * Gear: An open-world sandbox game for creative people.                      *
 * http://github.com/cathode/gear/                                            *
 * Copyright © 2009-2016 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT        *
 * license. See the included LICENSE file for details.                        *
 *****************************************************************************/
using System;
using System.Diagnostics.Contracts;

namespace Gear.Geometry
{
    /// <summary>
    /// A two-dimensional double-precision floating point vector. This type is immutable.
    /// </summary>
    /// <remarks>
    /// This type is immutable.
    /// </remarks>
    public struct Vector2d 
    {
        #region Fields
        /// <summary>
        /// Backing field for the <see cref="Vector2d.North"/> property.
        /// </summary>
        private static readonly Vector2d north = new Vector2d(0.0, 1.0);     
       
        /// <summary>
        /// Backing field for the <see cref="Vector2d.East"/> property.
        /// </summary>
        private static readonly Vector2d east = new Vector2d(1.0, 0.0);     
        
        /// <summary>
        /// Backing field for the <see cref="Vector2d.South"/> property.
        /// </summary>
        private static readonly Vector2d south = new Vector2d(0.0, -1.0);     
        
        /// <summary>
        /// Backing field for the <see cref="Vector2d.West"/> property.
        /// </summary>
        private static readonly Vector2d west = new Vector2d(-1.0, 0.0);    
        
        /// <summary>
        /// Backing field for the <see cref="Vector2d.Zero"/> property.
        /// </summary>
        private static readonly Vector2d zero = new Vector2d(0.0, 0.0);     
        
        /// <summary>
        /// Backing field for the <see cref="Vector2d.X"/> property.
        /// </summary>
        private readonly double x;     
        
        /// <summary>
        /// Backing field for the <see cref="Vector2d.Y"/> property.
        /// </summary>
        private readonly double y;
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Vector2d"/> struct.
        /// </summary>
        /// <param name="vector">An <see cref="Vector2d"/> containing the X and Y values to use for the new <see cref="Vector2d"/> instance.</param>
        public Vector2d(Vector2d vector)
        {
            this.x = vector.X;
            this.y = vector.Y;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector2d"/> struct.
        /// </summary>
        /// <param name="x">The x-component of the new vector.</param>
        /// <param name="y">The y-component of the new vector.</param>
        public Vector2d(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
        #endregion
        #region Properties
        /// <summary>
        /// Gets a unit vector pointing due east.
        /// </summary>
        public static Vector2d East
        {
            get
            {
                return Vector2d.east;
            }
        }

        /// <summary>
        /// Gets a unit vector pointing due north.
        /// </summary>
        public static Vector2d North
        {
            get
            {
                return Vector2d.north;
            }
        }

        /// <summary>
        /// Gets a unit vector pointing due south.
        /// </summary>
        public static Vector2d South
        {
            get
            {
                return Vector2d.south;
            }
        }

        /// <summary>
        /// Gets a unit vector pointing due west.
        /// </summary>
        public static Vector2d West
        {
            get
            {
                return Vector2d.west;
            }
        }

        /// <summary>
        /// Gets the x-component of the <see cref="Vector2d"/>.
        /// </summary>
        public double X
        {
            get
            {
                return this.x;
            }
        }

        /// <summary>
        /// Gets the y-component of the <see cref="Vector2d"/>.
        /// </summary>
        public double Y
        {
            get
            {
                return this.y;
            }
        }

        /// <summary>
        /// Gets the zero vector.
        /// </summary>
        public static Vector2d Zero
        {
            get
            {
                return new Vector2d(0.0, 0.0);
            }
        }
        #endregion
        #region Methods
        /// <summary>
        /// Converts the current <see cref="Vector2d"/> to it's absolute value.
        /// </summary>
        public Vector2d Absolute()
        {
            return new Vector2d(Math.Abs(this.X), Math.Abs(this.Y));
        }

        /// <summary>
        /// Returns the absolute value of a specified vector.
        /// </summary>
        /// <param name="v">The <see cref="Vector2d"/> to get the absolute value of.</param>
        /// <returns>A new <see cref="Vector2d"/> that is the absolute value of <paramref name="vector"/>.</returns>
        public static Vector2d Absolute(Vector2d v)
        {
            return new Vector2d(Math.Abs(v.X), Math.Abs(v.Y));
        }

        /// <summary>
        /// Adds a specified vector to the current instance.
        /// </summary>
        /// <param name="other">The <see cref="Vector2d"/> to add to the current <see cref="Vector2d"/>.</param>
        public Vector2d Add(Vector2d other)
        {
            return new Vector2d(this.X + other.X, this.Y + other.Y);
        }

        /// <summary>
        /// Adds two <see cref="Vector2d"/> instances.
        /// </summary>
        /// <param name="a">The first <see cref="Vector2d"/> to add.</param>
        /// <param name="b">The second <see cref="Vector2d"/> to add.</param>
        /// <returns>A new <see cref="Vector2d"/> containing the sum of inputs.</returns>
        public static Vector2d Add(Vector2d a, Vector2d b)
        {
            return new Vector2d(a.X + b.X, a.Y + b.Y);
        }

        /// <summary>
        /// Adds three <see cref="Vector2d"/> instances.
        /// </summary>
        /// <param name="a">The first <see cref="Vector2d"/> to add.</param>
        /// <param name="b">The second <see cref="Vector2d"/> to add.</param>
        /// <param name="c">The third <see cref="Vector2d"/> to add.</param>
        /// <returns>A new <see cref="Vector2d"/> containing the sum of inputs.</returns>
        public static Vector2d Add(Vector2d a, Vector2d b, Vector2d c)
        {
            return new Vector2d(a.X + b.X + c.X, a.Y + b.Y + c.Y);
        }

        /// <summary>
        /// Adds four <see cref="Vector2d"/> instances.
        /// </summary>
        /// <param name="a">The first <see cref="Vector2d"/> to add.</param>
        /// <param name="b">The second <see cref="Vector2d"/> to add.</param>
        /// <param name="c">The third <see cref="Vector2d"/> to add.</param>
        /// <param name="d">The fourth <see cref="Vector2d"/> to add.</param>
        /// <returns>A new <see cref="Vector2d"/> containing the sum of inputs.</returns>
        public static Vector2d Add(Vector2d a, Vector2d b, Vector2d c, Vector2d d)
        {
            return new Vector2d(a.X + b.X + c.X + d.X, a.Y + b.Y + c.Y + d.Y);
        }

        /// <summary>
        /// Returns the smallest whole <see cref="Vector2d"/> greater than or equal to the current <see cref="Vector2d"/>.
        /// </summary>
        public Vector2d Ceiling()
        {
            return new Vector2d(Math.Ceiling(this.X), Math.Ceiling(this.Y));
        }

        /// <summary>
        /// Returns the smallest whole <see cref="Vector2d"/> greater than or equal to the specified <see cref="Vector2d"/>.
        /// </summary>
        /// <param name="v">A <see cref="Vector2d"/> instance.</param>
        /// <returns>A new <see cref="Vector2d"/> instance representing the ceiling of <paramref name="vector"/>.</returns>
        public static Vector2d Ceiling(Vector2d v)
        {
            return new Vector2d(Math.Ceiling(v.X), Math.Ceiling(v.Y));
        }

        /// <summary>
        /// Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <param name="obj">Another object to compare to.</param>
        /// <returns>true if obj and this instance are the same type and represent the same value; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (obj is Vector2d)
                return Vector2d.Equals(this, (Vector2d)obj);
            return false;
        }

        /// <summary>
        /// Indicates whether this instance and a specified <see cref="Vector2d"/> are equal.
        /// </summary>
        /// <param name="other">Another <see cref="Vector2d"/> to compare to.</param>
        /// <returns>true if <paramref name="other"/> and this instance represent the same value; otherwise, false.</returns>
        public bool Equals(Vector2d other)
        {
            return (this.X == other.X) && (this.Y == other.Y);
        }

        /// <summary>
        /// Determines if two <see cref="Vector2d"/> instances are equal.
        /// </summary>
        /// <param name="a">The first instance to compare.</param>
        /// <param name="b">The second instance to compare.</param>
        /// <returns>true if both instances represent the same value; otherwise false.</returns>
        public static bool Equals(Vector2d a, Vector2d b)
        {
            return (a.X == b.X) && (a.Y == b.Y);
        }

        /// <summary>
        /// Floors the current <see cref="Vector2d"/>.
        /// </summary>
        public Vector2d Floor()
        {
            return new Vector2d(Math.Floor(this.X), Math.Floor(this.Y));
        }

        /// <summary>
        /// Returns the largest whole <see cref="Vector2d"/> less than or equal to the specified <see cref="Vector2d"/>.
        /// </summary>
        /// <param name="vector">A <see cref="Vector2d"/> instance.</param>
        /// <returns>A new <see cref="Vector2d"/> instance representing the floor of <paramref name="vector"/>.</returns>
        public static Vector2d Floor(Vector2d vector)
        {
            return new Vector2d(Math.Floor(vector.X), Math.Floor(vector.Y));
        }

        /// <summary>
        /// Overridden. Calculates a hash code for the current <see cref="Vector2d"/>.
        /// </summary>
        /// <returns>A hash code for the current <see cref="Vector2d"/>.</returns>
        public override int GetHashCode()
        {
            return __HashCode.Calculate(this.X, this.Y);
        }

        /// <summary>
        /// Multiplies a <see cref="Vector2d"/> by a scalar value.
        /// </summary>
        /// <param name="scalar">The scalar value to multiply by.</param>
        public Vector2d Multiply(double scalar)
        {
            return new Vector2d(this.X * scalar, this.Y * scalar);
        }

        /// <summary>
        /// Multiplies a <see cref="Vector2d"/> by a scalar value.
        /// </summary>
        /// <param name="vector">The <see cref="Vector2d"/> to be multiplied.</param>
        /// <param name="scalar">The scalar value to multiply by.</param>
        /// <returns>A new <see cref="Vector2d"/> that is the result of the multiplication.</returns>
        public static Vector2d Multiply(Vector2d vector, double scalar)
        {
            return new Vector2d(vector.X * scalar, vector.Y * scalar);
        }

        /// <summary>
        /// Rounds the current vector to the nearest integral value.
        /// </summary>
        public Vector2d Round()
        {
            return new Vector2d(Math.Round(this.X), Math.Round(this.Y));
        }

        /// <summary>
        /// Rounds the current vector to the specified number of fractional digits.
        /// </summary>
        /// <param name="digits">The number of fractional digits to round to.</param>
        public Vector2d Round(int digits)
        {
            Contract.Requires(digits >= 0);
            Contract.Requires(digits <= 15);

            return new Vector2d(Math.Round(this.X, digits), Math.Round(this.Y, digits));
        }

        /// <summary>
        /// Rounds a vector to the nearest integral value, and returns the result as a new instance.
        /// </summary>
        /// <param name="vector">The <see cref="Vector2d"/> to be rounded.</param>
        /// <returns>A new <see cref="Vector2d"/> instance that is the rounded form of the specified vector.</returns>
        public static Vector2d Round(Vector2d vector)
        {
            return new Vector2d(Math.Round(vector.X), Math.Round(vector.Y));
        }

        /// <summary>
        /// Rounds a vector to specified number of fractional digits, and returns the result as a new instance.
        /// </summary>
        /// <param name="vector">The <see cref="Vector2d"/> to be rounded.</param>
        /// <param name="digits">The number of fractional digits to round to.</param>
        /// <returns>A new <see cref="Vector2d"/> instance that is the rounded form of the specified vector.</returns>
        public static Vector2d Round(Vector2d vector, int digits)
        {
            Contract.Requires(digits >= 0);
            Contract.Requires(digits <= 15);

            return new Vector2d(Math.Round(vector.X, digits), Math.Round(vector.Y, digits));
        }

        /// <summary>
        /// Subtracts a specified <see cref="Vector2d"/> from the current instance.
        /// </summary>
        /// <param name="other">The <see cref="Vector2d"/> to subtract from the current instance.</param>
        public Vector2d Subtract(Vector2d other)
        {
            return new Vector2d(this.X - other.X, this.Y - other.Y);
        }

        /// <summary>
        /// Subtracts two vectors.
        /// </summary>
        /// <param name="a">The <see cref="Vector2d"/> to subtract from.</param>
        /// <param name="b">The <see cref="Vector2d"/> to subtract.</param>
        /// <returns>A new <see cref="Vector2d"/> that is the result of the subtraction.</returns>
        public static Vector2d Subtract(Vector2d a, Vector2d b)
        {
            return new Vector2d(a.X - b.X, a.Y - b.Y);
        }
        #endregion
        #region Operators
        /// <summary>
        /// Multiplies a <see cref="Vector2d"/> by a scalar value.
        /// </summary>
        /// <param name="vector">The <see cref="Vector2d"/> to be multiplied.</param>
        /// <param name="scalar">The scalar value to multiply by.</param>
        /// <returns>A new <see cref="Vector2d"/> that is the result of the multiplication.</returns>
        public static Vector2d operator *(Vector2d vector, double scalar)
        {
            return new Vector2d(vector.X * scalar, vector.Y * scalar);
        }

        /// <summary>
        /// Multiplies a <see cref="Vector2d"/> by a scalar value.
        /// </summary>
        /// <param name="vector">The <see cref="Vector2d"/> to be multiplied.</param>
        /// <param name="scalar">The scalar value to multiply by.</param>
        /// <returns>A new <see cref="Vector2d"/> that is the result of the multiplication.</returns>
        public static Vector2d operator *(double scalar, Vector2d vector)
        {
            return vector * scalar;
        }

        /// <summary>
        /// Divides a <see cref="Vector2d"/> by a scalar value.
        /// </summary>
        /// <param name="vector">The <see cref="Vector2d"/> to be divided.</param>
        /// <param name="scalar">The scalar value to divide by.</param>
        /// <returns>A new <see cref="Vector2d"/> that is the result of the division.</returns>
        public static Vector2d operator /(Vector2d vector, double scalar)
        {
            return new Vector2d(vector.X / scalar, vector.Y / scalar);
        }

        /// <summary>
        /// Adds two <see cref="Vector2d"/> instances.
        /// </summary>
        /// <param name="left">The <see cref="Vector2d"/> on the left of the operator.</param>
        /// <param name="right">The <see cref="Vector2d"/> on the right of the operator.</param>
        /// <returns>A new <see cref="Vector2d"/> that is the result of the addition.</returns>
        public static Vector2d operator +(Vector2d left, Vector2d right)
        {
            return new Vector2d(left.X + right.X, left.Y + right.Y);
        }

        /// <summary>
        /// Subtracts two <see cref="Vector2d"/> instances.
        /// </summary>
        /// <param name="left">The <see cref="Vector2d"/> on the left of the operator.</param>
        /// <param name="right">The <see cref="Vector2d"/> on the right of the operator.</param>
        /// <returns>A new <see cref="Vector2d"/> that is the result of the subtraction.</returns>
        public static Vector2d operator -(Vector2d left, Vector2d right)
        {
            return new Vector2d(left.X - right.X, left.Y - right.Y);
        }

        /// <summary>
        /// Compares two <see cref="Vector2d"/> instances for equality.
        /// </summary>
        /// <param name="left">The <see cref="Vector2d"/> on the left of the operator.</param>
        /// <param name="right">The <see cref="Vector2d"/> on the right of the operator.</param>
        /// <returns>true if <paramref name="left"/> represents the same value as <paramref name="right"/>; otherwise false.</returns>
        public static bool operator ==(Vector2d left, Vector2d right)
        {
            return (left.X == right.X) && (left.Y == right.Y);
        }

        /// <summary>
        /// Compares two <see cref="Vector2d"/> instances for inequality.
        /// </summary>
        /// <param name="left">The <see cref="Vector2d"/> on the left of the operator.</param>
        /// <param name="right">The <see cref="Vector2d"/> on the right of the operator.</param>
        /// <returns>true if <paramref name="left"/> represents a different value than <paramref name="right"/>; otherwise false.</returns>
        public static bool operator !=(Vector2d left, Vector2d right)
        {
            return (left.X != right.X) || (left.Y != right.Y);
        }
        #endregion
    }
}
