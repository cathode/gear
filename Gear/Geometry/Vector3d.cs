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
    /// Represents a three-dimensional vector using three double-precision floating point numbers.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Vector3d : IEquatable<Vector3d>
    {
        #region Fields

        /// <summary>
        /// Backing field for the static <see cref="Vector3d.Zero"/> property.
        /// </summary>
        private static readonly Vector3d zero = new Vector3d(0.0, 0.0, 0.0);

        /// <summary>
        /// Backing field for the <see cref="Vector3d.X"/> property.
        /// </summary>
        private readonly double x;

        /// <summary>
        /// Backing field for the <see cref="Vector3d.Y"/> property.
        /// </summary>
        private readonly double y;

        /// <summary>
        /// Backing field for the <see cref="Vector3d.Z"/> property.
        /// </summary>
        private readonly double z;
        #endregion
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector3d"/> struct.
        /// </summary>
        /// <param name="vector">A <see cref="Gear.Geometry.Vector2d"/> instance supplying x and y values.</param>
        /// <remarks>The z value defaults to 0.</remarks>
        public Vector3d(Gear.Geometry.Vector2d vector)
        {
            this.x = vector.X;
            this.y = vector.Y;
            this.z = 0.0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector3d"/> struct.
        /// </summary>
        /// <param name="vector">A <see cref="Vector3d"/> instance supplying x, y and z values.</param>
        public Vector3d(Vector3d vector)
        {
            this.x = vector.X;
            this.y = vector.Y;
            this.z = vector.Z;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector3d"/> struct.
        /// </summary>
        /// <param name="x">The X-component of the vector.</param>
        /// <param name="y">The Y-component of the vector.</param>
        /// <param name="z">The Z-component of the vector.</param>
        public Vector3d(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        #endregion
        #region Indexers
        public double this[int element]
        {
            get
            {
                Contract.Requires(element > -1);
                Contract.Requires(element < 3);

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
                else
                {
                    throw new NotImplementedException();
                }
            }
        }
        #endregion
        #region Properties

        /// <summary>
        /// Gets the zero vector.
        /// </summary>
        public static Vector3d Zero
        {
            get
            {
                return Vector3d.zero;
            }
        }

        /// <summary>
        /// Gets a unit vector that points along the positive X-axis.
        /// </summary>
        public static Vector3d Right
        {
            get
            {
                return new Vector3d(1.0, 0.0, 0.0);
            }
        }

        /// <summary>
        /// Gets a unit vector that points along the negative X-axis.
        /// </summary>
        public static Vector3d Left
        {
            get
            {
                return new Vector3d(-1.0, 0.0, 0.0);
            }
        }

        /// <summary>
        /// Gets a unit vector that points along the positive Y-axis.
        /// </summary>
        public static Vector3d Up
        {
            get
            {
                return new Vector3d(0.0, 1.0, 0.0);
            }
        }

        /// <summary>
        /// Gets a unit vector that points along the negative Z-axis.
        /// </summary>
        public static Vector3d Down
        {
            get
            {
                return new Vector3d(0.0, -1.0, 0.0);
            }
        }

        /// <summary>
        /// Gets a unit vector that points along the positive Y-axis.
        /// </summary>
        public static Vector3d Forward
        {
            get
            {
                return new Vector3d(0.0, 0.0, 1.0);
            }
        }

        /// <summary>
        /// Gets a unit vector that points along the negative Y-axis.
        /// </summary>
        public static Vector3d Backward
        {
            get
            {
                return new Vector3d(0.0, 0.0, -1.0);
            }
        }

        /// <summary>
        /// Gets or sets the X-component of the vector.
        /// </summary>
        public double X
        {
            get
            {
                return this.x;
            }
        }

        /// <summary>
        /// Gets or sets the Y-component of the vector.
        /// </summary>
        public double Y
        {
            get
            {
                return this.y;
            }
        }

        /// <summary>
        /// Gets or sets the Z-component of the vector.
        /// </summary>
        public double Z
        {
            get
            {
                return this.z;
            }
        }
        #endregion
        #region Operators

        /// <summary>
        /// Subtracts vector b from vector a and returns a new vector as the result.
        /// </summary>
        /// <param name="left">The value that appears on the left-hand side of the operator.</param>
        /// <param name="right">The value that appears on the right-hand side of the operator.</param>
        /// <returns></returns>
        public static Vector3d operator -(Vector3d left, Vector3d right)
        {
            return new Vector3d(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
        }

        /// <summary>
        /// Calculates inequality of two vectors.
        /// </summary>
        /// <param name="left">The value that appears on the left-hand side of the operator.</param>
        /// <param name="right">The value that appears on the right-hand side of the operator.</param>
        /// <returns></returns>
        public static bool operator !=(Vector3d left, Vector3d right)
        {
            return left.X != right.X || left.Y != right.Y || left.Z != right.Z;
        }

        /// <summary>
        /// Multiplies a vector by a scalar value and returns a new vector as the result.
        /// </summary>
        /// <param name="left">The value that appears on the left-hand side of the operator.</param>
        /// <param name="right">The value that appears on the right-hand side of the operator.</param>
        /// <returns>A new <see cref="Vector3d"/> that is the result of the scalar multiplication.</returns>
        public static Vector3d operator *(Vector3d left, double right)
        {
            return new Vector3d(left.X * right, left.Y * right, left.Z * right);
        }

        /// <summary>
        /// Multiplies a vector by a scalar value and returns a new vector as the result.
        /// </summary>
        /// <param name="left">The value that appears on the left-hand side of the operator.</param>
        /// <param name="right">The value that appears on the right-hand side of the operator.</param>
        /// <returns>A new <see cref="Vector3d"/> that is the result of the scalar multiplication.</returns>
        public static Vector3d operator *(double left, Vector3d right)
        {
            return new Vector3d(left * right.X, left * right.Y, left * right.Z);
        }

        /// <summary>
        /// Divides a vector by a scalar value and returns a new vector as the result.
        /// </summary>
        /// <param name="left">The value that appears on the left-hand side of the operator.</param>
        /// <param name="right">The value that appears on the right-hand side of the operator.</param>
        /// <returns></returns>
        public static Vector3d operator /(Vector3d left, double right)
        {
            return new Vector3d(left.X / right, left.Y / right, left.Z / right);
        }

        /// <summary>
        /// Adds vector a and vector b and returns a new vector as the result.
        /// </summary>
        /// <param name="left">The left-hand vector to be added.</param>
        /// <param name="right">The right-hand vector to be added.</param>
        /// <returns></returns>
        public static Vector3d operator +(Vector3d left, Vector3d right)
        {
            return new Vector3d(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
        }

        /// <summary>
        /// Calculates equality of two vectors.
        /// </summary>
        /// <param name="left">The value that appears on the left-hand side of the operator.</param>
        /// <param name="right">The value that appears on the right-hand side of the operator.</param>
        /// <returns></returns>
        public static bool operator ==(Vector3d left, Vector3d right)
        {
            return left.X == right.X && left.Y == right.Y && left.Z == right.Z;
        }

        #endregion
        #region Methods
        public Vector3d Absolute()
        {
            return new Vector3d(Math.Abs(this.X), Math.Abs(this.Y), Math.Abs(this.Z));
        }

        /// <summary>
        /// Returns the absolute value of a specified <see cref="Vector3d"/>.
        /// </summary>
        /// <param name="vector">The <see cref="Vector3d"/> to get the absolute value of.</param>
        /// <returns>A new <see cref="Vector3d"/> that is the absolute value of <paramref name="vector"/>.</returns>
        public static Vector3d Absolute(Vector3d vector)
        {
            return new Vector3d(Math.Abs(vector.X), Math.Abs(vector.Y), Math.Abs(vector.Z));
        }

        public Vector3d Add(Vector3d other)
        {
            return new Vector3d(this.X + other.X, this.Y + other.Y, this.Z + other.Z);
        }

        /// <summary>
        /// Adds two vectors.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Vector3d Add(Vector3d a, Vector3d b)
        {
            return new Vector3d(
                a.X + b.X,
                               a.Y + b.Y,
                               a.Z + b.Z);
        }

        /// <summary>
        /// Adds three vectors.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public static Vector3d Add(Vector3d a, Vector3d b, Vector3d c)
        {
            return new Vector3d(
                a.X + b.X + c.X,
                               a.Y + b.Y + c.Y,
                               a.Z + b.Z + c.Z);
        }

        /// <summary>
        /// Adds four vectors.
        /// </summary>
        /// <param name="a">The first vector to add.</param>
        /// <param name="b">The second vector to add.</param>
        /// <param name="c">The third vector to add.</param>
        /// <param name="d">The fourth vector to add.</param>
        /// <returns>A new <see cref="Vector3d"/> that is the result of the addition.</returns>
        public static Vector3d Add(Vector3d a, Vector3d b, Vector3d c, Vector3d d)
        {
            return new Vector3d(
                a.X + b.X + c.X + d.X,
                               a.Y + b.Y + c.Y + d.Y,
                               a.Z + b.Z + c.Z + d.Z);
        }

        /// <summary>
        /// Adds an array of vectors.
        /// </summary>
        /// <param name="vectors"></param>
        /// <returns></returns>
        public static Vector3d Add(Vector3d[] vectors)
        {
            if (vectors == null)
            {
                throw new ArgumentNullException("vectors");
            }
            else if (vectors.Length < 1)
            {
                return Vector3d.Zero;
            }
            else if (vectors.Length == 1)
            {
                return vectors[0];
            }

            var r = vectors[0];
            for (int i = 1; i < vectors.Length; i++)
            {
                r += vectors[i];
            }

            return r;
        }

        /// <summary>
        /// Returns the smallest <see cref="Vector3d"/> with whole number values which is greater than or equal to the current <see cref="Vector3d"/>.
        /// </summary>
        public Vector3d Ceiling()
        {
            return new Vector3d(
                Math.Ceiling(this.X),
                               Math.Ceiling(this.Y),
                               Math.Ceiling(this.Z));
        }

        /// <summary>
        /// Returns the smallest whole <see cref="Vector3d"/> greater than or equal to the specified <see cref="Vector3d"/>.
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        public static Vector3d Ceiling(Vector3d vector)
        {
            return new Vector3d(
                Math.Ceiling(vector.X),
                               Math.Ceiling(vector.Y),
                               Math.Ceiling(vector.Z));
        }

        /// <summary>
        /// Calculates the cross product of the current <see cref="Vector3d"/> and the specified <see cref="Vector3d"/>.
        /// </summary>
        /// <param name="other">The other <see cref="Vector3d"/> instance.</param>
        public Vector3d CrossProduct(Vector3d other)
        {
            return new Vector3d(
                (this.Y * other.Z) - (this.Z * other.Y),
                                    (this.Z * other.X) - (this.X * other.Z),
                                    (this.X * other.Y) - (this.Y * other.X));
        }

        /// <summary>
        /// Calculates the cross product of two <see cref="Vector3d">Vector3's</see>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Vector3d CrossProduct(Vector3d a, Vector3d b)
        {
            return new Vector3d(
                (a.Y * b.Z) - (a.Z * b.Y),
                (a.Z * b.X) - (a.X * b.Z),
                (a.X * b.Y) - (a.Y * b.X));
        }

        /// <summary>
        /// Calculates the dot product of two vectors.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static double DotProduct(Vector3d a, Vector3d b)
        {
            return (a.X * b.X) + (a.Y * b.Y) + (a.Z * b.Z);
        }

        public double DistanceTo(Vector3d other)
        {
            return Vector3d.DistanceBetween(this, other);
        }

        public static double DistanceBetween(Vector3d a, Vector3d b)
        {
            var xd = b.x - a.x;
            var yd = b.y - a.y;
            var zd = b.z - a.z;

            return Math.Sqrt((xd * xd) + (yd * yd) + (zd * zd));
        }

        /// <summary>
        /// Divides the current vector by a scalar value.
        /// </summary>
        /// <param name="scalar"></param>
        /// <returns></returns>
        public Vector3d Divide(double scalar)
        {
            return this / scalar;
        }

        /// <summary>
        /// Compares the current <see cref="Vector3d"/> and the specified object for equality.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj is Vector3d)
            {
                return Vector3d.Equals(this, (Vector3d)obj);
            }

            return false;
        }

        public static void OrthoNormalize(ref Vector3d normal, ref Vector3d tangent)
        {
            normal = normal.Normalize();

            var proj = normal * Vector3d.DotProduct(tangent, normal);

            tangent -= proj;

            tangent = tangent.Normalize();
        }

        /// <summary>
        /// Compares the current <see cref="Vector3d"/> and the specified <see cref="Vector3d"/> for equality.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Vector3d other)
        {
            return this.X == other.X && this.Y == other.Y && this.Z == other.Z;
        }

        /// <summary>
        /// Compares two <see cref="Vector3d"/> instances for equality.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool Equals(Vector3d a, Vector3d b)
        {
            return a.X == b.X && a.Y == b.Y && a.Z == b.Z;
        }

        /// <summary>
        /// Calculates a unique hash code for the current instance.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return __HashCode.Calculate(this.X, this.Y, this.Z);
        }

        /// <summary>
        /// Interpolates the midpoint of two vectors.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Vector3d Interpolate(Vector3d a, Vector3d b)
        {
            return new Vector3d(
                (a.X * 0.5) + (b.X * 0.5),
                               (a.Y * 0.5) + (b.Y * 0.5),
                               (a.Z * 0.5) + (b.Z * 0.5));
        }

        /// <summary>
        /// Interpolates the weighted midpoint between two vectors.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="weight"></param>
        /// <returns></returns>
        public static Vector3d Interpolate(Vector3d a, Vector3d b, double weight)
        {
            if (weight < 0.0 || weight > 1.0)
            {
                throw new ArgumentOutOfRangeException("weight", "Weight must be between 0.0 and 1.0");
            }

            return new Vector3d(
                (a.X * (1.0 - weight)) + (b.X * weight),
                (a.Y * (1.0 - weight)) + (b.Y * weight),
                (a.Z * (1.0 - weight)) + (b.Z * weight));
        }

        /// <summary>
        /// Returns the midpoint of three vectors.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public static Vector3d Interpolate(Vector3d a, Vector3d b, Vector3d c)
        {
            return Vector3d.Interpolate(Vector3d.Interpolate(a, b), c, 2.0 / 3.0);
        }

        public static Vector3d InterpolateN(params Vector3d[] vectors)
        {
            Contract.Requires(vectors != null);

            double weight = 1.0 / vectors.Length;
            Vector3d r = Vector3d.Zero;
            for (int i = 0; i < vectors.Length; i++)
            {
                r += vectors[i] * weight;
            }

            return r;
        }

        /// <summary>
        /// Returns the midpoint of four vectors.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <returns></returns>
        public static Vector3d Interpolate(Vector3d a, Vector3d b, Vector3d c, Vector3d d)
        {
            return Vector3d.Interpolate(Vector3d.Interpolate(a, c), Vector3d.Interpolate(b, d));
        }

        /// <summary>
        /// Inverts the current <see cref="Vector3d"/>.
        /// </summary>
        /// <returns>A new <see cref="Vector3d"/> that is the inversion of the current <see cref="Vector3d"/>.</returns>
        public Vector3d Invert()
        {
            return new Vector3d(-this.X, -this.Y, -this.Z);
        }

        /// <summary>
        /// Inverts the specified <see cref="Vector3d"/>.
        /// </summary>
        /// <param name="vector">The <see cref="Vector3d"/> instance to invert.</param>
        /// <returns>A new <see cref="Vector3d"/> that is the inversion of the specified <see cref="Vector3d"/>.</returns>
        public static Vector3d Invert(Vector3d vector)
        {
            return new Vector3d(-vector.X, -vector.Y, -vector.Z);
        }

        /// <summary>
        /// Determines if the current <see cref="Vector3d"/> is parallel to the specified <see cref="Vector3d"/>.
        /// </summary>
        /// <param name="other">Another <see cref="Vector3d"/> instance.</param>
        /// <returns>true if the current vector is parallel to the specified vector..</returns>
        public bool IsParallelTo(Vector3d other)
        {
            if (Vector3d.CrossProduct(this, other) == Vector3d.Zero)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Multiplies the current vector by a scalar value.
        /// </summary>
        /// <param name="scalar">The double-precision floating point value to multiply the X, Y, and Z components of the current vector.</param>
        /// <returns></returns>
        public Vector3d Multiply(double scalar)
        {
            return this * scalar;
        }

        /// <summary>
        /// Subtracts a vector from the current vector.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public Vector3d Subtract(Vector3d other)
        {
            return this - other;
        }

        public Vector3d Normalize()
        {
            return Vector3d.Normalize(this);
        }

        public static Vector3d Normalize(Vector3d v)
        {
            var sum = (v.X * v.X) + (v.Y * v.Y) + (v.Z * v.Z);

            if (sum == 0)
            {
                return new Vector3d(0, 0, 0);
            }
            else
            {
                var d = Math.Sqrt(sum);
                return new Vector3d(v.X / d, v.Y / d, v.Z / d);
            }
        }

        public Vector3d Round(int digits)
        {
            Contract.Requires(digits >= 0);
            Contract.Requires(digits <= 15);

            return new Vector3d(
                Math.Round(this.x, digits),
                Math.Round(this.y, digits),
                Math.Round(this.z, digits));
        }

        public override string ToString()
        {
            return string.Format("({0}, {1}, {2})", this.X, this.Y, this.Z);
        }

        public Matrix4 ToTranslationMatrix()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}