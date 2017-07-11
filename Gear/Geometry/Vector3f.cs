/******************************************************************************
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
using System.Runtime.InteropServices;
using System.Diagnostics.Contracts;


namespace Gear.Geometry
{
    /// <summary>
    /// Represents a 3-dimensional vector with 32-bit (single precision) floating point coordinate values. This type is immutable.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct Vector3f : IEquatable<Vector3f>
    {
        #region Fields
        /// <summary>
        /// Backing field for the static <see cref="Vector3f.Zero"/> property.
        /// </summary>
        private static readonly Vector3f zero = new Vector3f(0.0f, 0.0f, 0.0f);

        /// <summary>
        /// Backing field for the <see cref="Vector3f.X"/> property.
        /// </summary>
        [FieldOffset(0)]
        private readonly float x;

        /// <summary>
        /// Backing field for the <see cref="Vector3f.Y"/> property.
        /// </summary>
        [FieldOffset(4)]
        private readonly float y;

        /// <summary>
        /// Backing field for the <see cref="Vector3f.Z"/> property.
        /// </summary>
        [FieldOffset(8)]
        private readonly float z;
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Vector3f"/> struct.
        /// </summary>
        /// <param name="vector">A <see cref="Gear.Geometry.Vector2d"/> instance supplying x and y values.</param>
        /// <remarks>The z value defaults to 0.</remarks>
        public Vector3f(Gear.Geometry.Vector2f vector)
        {
            this.x = vector.X;
            this.y = vector.Y;
            this.z = 0.0f;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector3f"/> struct.
        /// </summary>
        /// <param name="vector">A <see cref="Vector3f"/> instance supplying x, y and z values.</param>
        public Vector3f(Vector3f vector)
        {
            this.x = vector.X;
            this.y = vector.Y;
            this.z = vector.Z;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector3f"/> struct.
        /// </summary>
        /// <param name="x">The X-component of the vector.</param>
        /// <param name="y">The Y-component of the vector.</param>
        /// <param name="z">The Z-component of the vector.</param>
        public Vector3f(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        #endregion
        #region Indexers
        public float this[int element]
        {
            get
            {
                Contract.Requires(element > -1);
                Contract.Requires(element < 3);

                if (element == 0)
                    return this.x;
                else if (element == 1)
                    return this.y;
                else if (element == 2)
                    return this.z;
                else
                    throw new NotImplementedException();
            }
        }
        #endregion
        #region Properties
        /// <summary>
        /// Gets the zero vector.
        /// </summary>
        public static Vector3f Zero
        {
            get
            {
                return Vector3f.zero;
            }
        }

        /// <summary>
        /// Gets a unit vector that points along the positive X-axis.
        /// </summary>
        public static Vector3f Right
        {
            get
            {
                return new Vector3f(1.0f, 0.0f, 0.0f);
            }
        }

        /// <summary>
        /// Gets a unit vector that points along the negative X-axis.
        /// </summary>
        public static Vector3f Left
        {
            get
            {
                return new Vector3f(-1.0f, 0.0f, 0.0f);
            }
        }

        /// <summary>
        /// Gets a unit vector that points along the positive Y-axis.
        /// </summary>
        public static Vector3f Up
        {
            get
            {
                return new Vector3f(0.0f, 1.0f, 0.0f);
            }
        }

        /// <summary>
        /// Gets a unit vector that points along the negative Z-axis.
        /// </summary>
        public static Vector3f Down
        {
            get
            {
                return new Vector3f(0.0f, -1.0f, 0.0f);
            }
        }

        /// <summary>
        /// Gets a unit vector that points along the positive Y-axis.
        /// </summary>
        public static Vector3f Forward
        {
            get
            {
                return new Vector3f(0.0f, 0.0f, 1.0f);
            }
        }

        /// <summary>
        /// Gets a unit vector that points along the negative Y-axis.
        /// </summary>
        public static Vector3f Backward
        {
            get
            {
                return new Vector3f(0.0f, 0.0f, -1.0f);
            }
        }

        /// <summary>
        /// Gets or sets the X-component of the vector.
        /// </summary>
        public float X
        {
            get
            {
                return this.x;
            }
        }

        /// <summary>
        /// Gets or sets the Y-component of the vector.
        /// </summary>
        public float Y
        {
            get
            {
                return this.y;
            }
        }

        /// <summary>
        /// Gets or sets the Z-component of the vector.
        /// </summary>
        public float Z
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
        public static Vector3f operator -(Vector3f left, Vector3f right)
        {
            return new Vector3f(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
        }

        /// <summary>
        /// Calculates inequality of two vectors.
        /// </summary>
        /// <param name="left">The value that appears on the left-hand side of the operator.</param>
        /// <param name="right">The value that appears on the right-hand side of the operator.</param>
        /// <returns></returns>
        public static bool operator !=(Vector3f left, Vector3f right)
        {
            return left.X != right.X || left.Y != right.Y || left.Z != right.Z;
        }

        /// <summary>
        /// Multiplies a vector by a scalar value and returns a new vector as the result.
        /// </summary>
        /// <param name="left">The value that appears on the left-hand side of the operator.</param>
        /// <param name="right">The value that appears on the right-hand side of the operator.</param>
        /// <returns>A new <see cref="Vector3f"/> that is the result of the scalar multiplication.</returns>
        public static Vector3f operator *(Vector3f left, float right)
        {
            return new Vector3f(left.X * right, left.Y * right, left.Z * right);
        }

        /// <summary>
        /// Multiplies a vector by a scalar value and returns a new vector as the result.
        /// </summary>
        /// <param name="left">The value that appears on the left-hand side of the operator.</param>
        /// <param name="right">The value that appears on the right-hand side of the operator.</param>
        /// <returns>A new <see cref="Vector3f"/> that is the result of the scalar multiplication.</returns>
        public static Vector3f operator *(float left, Vector3f right)
        {
            return new Vector3f(left * right.X, left * right.Y, left * right.Z);
        }

        /// <summary>
        /// Divides a vector by a scalar value and returns a new vector as the result.
        /// </summary>
        /// <param name="left">The value that appears on the left-hand side of the operator.</param>
        /// <param name="right">The value that appears on the right-hand side of the operator.</param>
        /// <returns></returns>
        public static Vector3f operator /(Vector3f left, float right)
        {
            return new Vector3f(left.X / right, left.Y / right, left.Z / right);
        }

        /// <summary>
        /// Adds vector a and vector b and returns a new vector as the result.
        /// </summary>
        /// <param name="left">The left-hand vector to be added.</param>
        /// <param name="right">The right-hand vector to be added.</param>
        /// <returns></returns>
        public static Vector3f operator +(Vector3f left, Vector3f right)
        {
            return new Vector3f(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
        }

        /// <summary>
        /// Calculates equality of two vectors.
        /// </summary>
        /// <param name="left">The value that appears on the left-hand side of the operator.</param>
        /// <param name="right">The value that appears on the right-hand side of the operator.</param>
        /// <returns></returns>
        public static bool operator ==(Vector3f left, Vector3f right)
        {
            return left.X == right.X && left.Y == right.Y && left.Z == right.Z;
        }
        #endregion
        #region Methods
        public Vector3f Absolute()
        {
            return new Vector3f(Math.Abs(this.X), Math.Abs(this.Y), Math.Abs(this.Z));
        }

        /// <summary>
        /// Returns the absolute value of a specified <see cref="Vector3f"/>.
        /// </summary>
        /// <param name="vector">The <see cref="Vector3f"/> to get the absolute value of.</param>
        /// <returns>A new <see cref="Vector3f"/> that is the absolute value of <paramref name="vector"/>.</returns>
        public static Vector3f Absolute(Vector3f vector)
        {
            return new Vector3f(Math.Abs(vector.X), Math.Abs(vector.Y), Math.Abs(vector.Z));
        }

        public Vector3f Add(Vector3f other)
        {
            return new Vector3f(this.X + other.X, this.Y + other.Y, this.Z + other.Z);
        }

        /// <summary>
        /// Adds two vectors.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Vector3f Add(Vector3f a, Vector3f b)
        {
            return new Vector3f(a.X + b.X,
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
        public static Vector3f Add(Vector3f a, Vector3f b, Vector3f c)
        {
            return new Vector3f(a.X + b.X + c.X,
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
        /// <returns>A new <see cref="Vector3f"/> that is the result of the addition.</returns>
        public static Vector3f Add(Vector3f a, Vector3f b, Vector3f c, Vector3f d)
        {
            return new Vector3f(a.X + b.X + c.X + d.X,
                               a.Y + b.Y + c.Y + d.Y,
                               a.Z + b.Z + c.Z + d.Z);
        }

        /// <summary>
        /// Adds an array of vectors.
        /// </summary>
        /// <param name="vectors"></param>
        /// <returns></returns>
        public static Vector3f Add(Vector3f[] vectors)
        {
            if (vectors == null)
                throw new ArgumentNullException("vectors");
            else if (vectors.Length < 1)
                return Vector3f.Zero;
            else if (vectors.Length == 1)
                return vectors[0];
            var r = vectors[0];
            for (int i = 1; i < vectors.Length; i++)
                r += vectors[i];

            return r;
        }

        /// <summary>
        /// Returns the smallest <see cref="Vector3f"/> with whole number values which is greater than or equal to the current <see cref="Vector3f"/>.
        /// </summary>
        public Vector3f Ceiling()
        {
            return new Vector3f((float)Math.Ceiling(this.X),
                               (float)Math.Ceiling(this.Y),
                               (float)Math.Ceiling(this.Z));
        }

        /// <summary>
        /// Returns the smallest whole <see cref="Vector3f"/> greater than or equal to the specified <see cref="Vector3f"/>.
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        public static Vector3f Ceiling(Vector3f vector)
        {
            return new Vector3f((float)Math.Ceiling(vector.X),
                               (float)Math.Ceiling(vector.Y),
                               (float)Math.Ceiling(vector.Z));
        }

        /// <summary>
        /// Calculates the cross product of the current <see cref="Vector3f"/> and the specified <see cref="Vector3f"/>.
        /// </summary>
        /// <param name="other">The other <see cref="Vector3f"/> instance.</param>
        public Vector3f CrossProduct(Vector3f other)
        {
            return new Vector3f((this.Y * other.Z) - (this.Z * other.Y),
                                    (this.Z * other.X) - (this.X * other.Z),
                                    (this.X * other.Y) - (this.Y * other.X));
        }

        /// <summary>
        /// Calculates the cross product of two <see cref="Vector3f">Vector3's</see>
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Vector3f CrossProduct(Vector3f a, Vector3f b)
        {
            return new Vector3f(
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
        public static float DotProduct(Vector3f a, Vector3f b)
        {
            return (a.X * b.X) + (a.Y * b.Y) + (a.Z * b.Z);
        }

        public float DistanceTo(Vector3f other)
        {
            return Vector3f.DistanceBetween(this, other);
        }

        public static float DistanceBetween(Vector3f a, Vector3f b)
        {
            var xd = b.x - a.x;
            var yd = b.y - a.y;
            var zd = b.z - a.z;

            return (float)Math.Sqrt(xd * xd + yd * yd + zd * zd);
        }

        /// <summary>
        /// Divides the current vector by a scalar value.
        /// </summary>
        /// <param name="scalar"></param>
        /// <returns></returns>
        public Vector3f Divide(float scalar)
        {
            return this / scalar;
        }

        /// <summary>
        /// Compares the current <see cref="Vector3f"/> and the specified object for equality.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj is Vector3f)
                return Vector3f.Equals(this, (Vector3f)obj);

            return false;
        }

        public static void OrthoNormalize(ref Vector3f normal, ref Vector3f tangent)
        {
            normal = normal.Normalize();

            var proj = normal * Vector3f.DotProduct(tangent, normal);

            tangent -= proj;

            tangent = tangent.Normalize();
        }


        /// <summary>
        /// Compares the current <see cref="Vector3f"/> and the specified <see cref="Vector3f"/> for equality.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Vector3f other)
        {
            return this.X == other.X && this.Y == other.Y && this.Z == other.Z;
        }

        /// <summary>
        /// Compares two <see cref="Vector3f"/> instances for equality.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool Equals(Vector3f a, Vector3f b)
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
        public static Vector3f Interpolate(Vector3f a, Vector3f b)
        {
            return new Vector3f((a.X * 0.5f) + (b.X * 0.5f),
                               (a.Y * 0.5f) + (b.Y * 0.5f),
                               (a.Z * 0.5f) + (b.Z * 0.5f));
        }

        /// <summary>
        /// Interpolates the weighted midpoint between two vectors.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="weight"></param>
        /// <returns></returns>
        public static Vector3f Interpolate(Vector3f a, Vector3f b, float weight)
        {
            if (weight < 0.0f || weight > 1.0f)
                throw new ArgumentOutOfRangeException("weight", "Weight must be between 0.0f and 1.0f");
            return new Vector3f((a.X * (1.0f - weight)) + (b.X * weight),
                (a.Y * (1.0f - weight)) + (b.Y * weight),
                (a.Z * (1.0f - weight)) + (b.Z * weight));
        }

        /// <summary>
        /// Returns the midpoint of three vectors.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public static Vector3f Interpolate(Vector3f a, Vector3f b, Vector3f c)
        {
            return Vector3f.Interpolate(Vector3f.Interpolate(a, b), c, 2.0f / 3.0f);
        }

        public static Vector3f InterpolateN(params Vector3f[] vectors)
        {
            Contract.Requires(vectors != null);

            float weight = 1.0f / vectors.Length;
            Vector3f r = Vector3f.Zero;
            for (int i = 0; i < vectors.Length; i++)
                r += vectors[i] * weight;

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
        public static Vector3f Interpolate(Vector3f a, Vector3f b, Vector3f c, Vector3f d)
        {
            return Vector3f.Interpolate(Vector3f.Interpolate(a, c), Vector3f.Interpolate(b, d));
        }

        /// <summary>
        /// Inverts the current <see cref="Vector3f"/>.
        /// </summary>
        /// <returns>A new <see cref="Vector3f"/> that is the inversion of the current <see cref="Vector3f"/>.</returns>
        public Vector3f Invert()
        {
            return new Vector3f(-this.X, -this.Y, -this.Z);
        }

        /// <summary>
        /// Inverts the specified <see cref="Vector3f"/>.
        /// </summary>
        /// <param name="vector">The <see cref="Vector3f"/> instance to invert.</param>
        /// <returns>A new <see cref="Vector3f"/> that is the inversion of the specified <see cref="Vector3f"/>.</returns>
        public static Vector3f Invert(Vector3f vector)
        {
            return new Vector3f(-vector.X, -vector.Y, -vector.Z);
        }

        /// <summary>
        /// Determines if the current <see cref="Vector3f"/> is parallel to the specified <see cref="Vector3f"/>.
        /// </summary>
        /// <param name="other">Another <see cref="Vector3f"/> instance.</param>
        /// <returns>true if the current vector is parallel to the specified vector..</returns>
        public bool IsParallelTo(Vector3f other)
        {
            if (Vector3f.CrossProduct(this, other) == Vector3f.Zero)
                return true;
            return false;
        }

        /// <summary>
        /// Multiplies the current vector by a scalar value.
        /// </summary>
        /// <param name="scalar">The float-precision floating point value to multiply the X, Y, and Z components of the current vector.</param>
        /// <returns></returns>
        public Vector3f Multiply(float scalar)
        {
            return this * scalar;
        }

        /// <summary>
        /// Subtracts a vector from the current vector.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public Vector3f Subtract(Vector3f other)
        {
            return this - other;
        }
        public Vector3f Normalize()
        {
            return Vector3f.Normalize(this);
        }

        public static Vector3f Normalize(Vector3f v)
        {
            var sum = (v.X * v.X) + (v.Y * v.Y) + (v.Z * v.Z);

            if (sum == 0)
            {
                return new Vector3f(0, 0, 0);
            }
            else
            {
                var d = (float)Math.Sqrt(sum);
                return new Vector3f(v.X / d, v.Y / d, v.Z / d);
            }
        }

        public Vector3f Round(int digits)
        {
            Contract.Requires(digits >= 0);
            Contract.Requires(digits <= 15);

            return new Vector3f(
                (float)Math.Round(this.x, digits),
                (float)Math.Round(this.y, digits),
                (float)Math.Round(this.z, digits));
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
