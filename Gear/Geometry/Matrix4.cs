﻿/******************************************************************************
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
    /// Provides a 4-by-4 double-precision floating point matrix implementation. This type is immutable.
    /// </summary>
    /// <remarks>
    /// Values are internally represented using row-major ordering, and are addressed with A-D for rows and X-W for columns.
    /// <code>
    ///        Columns
    ///        
    ///   | X  | Y  | Z  | W  |
    /// --+----+----+----+----+
    /// A |  0 |  1 |  2 |  3 |
    /// --+----+----+----+----+
    /// B |  4 |  5 |  6 |  7 |   Rows
    /// --+----+----+----+----+
    /// C |  8 |  9 | 10 | 11 |
    /// --+----+----+----+----+
    /// D | 12 | 13 | 14 | 15 |
    /// </code>
    /// </remarks>
    [StructLayout(LayoutKind.Explicit, Size = 128, Pack = 8)]
    public struct Matrix4 : IEquatable<Matrix4>
    {
        #region Fields
        /// <summary>
        /// Holds the identity matrix.
        /// </summary>
        public static readonly Matrix4 Identity = new Matrix4(1, 0, 0, 0,
                                                              0, 1, 0, 0,
                                                              0, 0, 1, 0,
                                                              0, 0, 0, 1);
        /// <summary>
        /// Element at the first row, fourth column.
        /// </summary>
        [FieldOffset(0x00)]
        private readonly double aw;

        /// <summary>
        /// Element at the first row, first column.
        /// </summary>
        [FieldOffset(0x08)]
        private readonly double ax;

        /// <summary>
        /// Element at the first row, second column.
        /// </summary>
        [FieldOffset(0x10)]
        private readonly double ay;

        /// <summary>
        /// Element at the first row, third column.
        /// </summary>
        [FieldOffset(0x18)]
        private readonly double az;

        /// <summary>
        /// Element at the second row, fourth column.
        /// </summary>
        [FieldOffset(0x20)]
        private readonly double bw;

        /// <summary>
        /// Element at the second row, first column.
        /// </summary>
        [FieldOffset(0x28)]
        private readonly double bx;

        /// <summary>
        /// Element at the second row, second column.
        /// </summary>
        [FieldOffset(0x30)]
        private readonly double by;

        /// <summary>
        /// Element at the second row, third column.
        /// </summary>
        [FieldOffset(0x38)]
        private readonly double bz;

        /// <summary>
        /// Element at the third row, fourth column.
        /// </summary>
        [FieldOffset(0x40)]
        private readonly double cw;

        /// <summary>
        /// Element at the third row, first column.
        /// </summary>
        [FieldOffset(0x48)]
        private readonly double cx;

        /// <summary>
        /// Element at the third row, second column.
        /// </summary>
        [FieldOffset(0x50)]
        private readonly double cy;

        /// <summary>
        /// Element at the third row, third column.
        /// </summary>
        [FieldOffset(0x58)]
        private readonly double cz;

        /// <summary>
        /// Element at the fourth row, fourth column.
        /// </summary>
        [FieldOffset(0x60)]
        private readonly double dw;

        /// <summary>
        /// Element at the fourth row, first column.
        /// </summary>
        [FieldOffset(0x68)]
        private readonly double dx;

        /// <summary>
        /// Element at the fourth row, second column.
        /// </summary>
        [FieldOffset(0x70)]
        private readonly double dy;

        /// <summary>
        /// Element at the fourth row, third column.
        /// </summary>
        [FieldOffset(0x78)]
        private readonly double dz;
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix4"/> struct.
        /// </summary>
        /// <param name="a">The value of the first row.</param>
        /// <param name="b">The value of the second row.</param>
        /// <param name="c">The value of the third row.</param>
        /// <param name="d">The value of the fourth row.</param>
        public Matrix4(Vector4d a, Vector4d b, Vector4d c, Vector4d d)
        {
            this.ax = a.X;
            this.ay = a.Y;
            this.az = a.Z;
            this.aw = a.W;

            this.bx = b.X;
            this.by = b.Y;
            this.bz = b.Z;
            this.bw = b.W;

            this.cx = c.X;
            this.cy = c.Y;
            this.cz = c.Z;
            this.cw = c.W;

            this.dx = d.X;
            this.dy = d.Y;
            this.dz = d.Z;
            this.dw = d.W;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix4"/> class.
        /// </summary>
        /// <param name="ax"></param>
        /// <param name="ay"></param>
        /// <param name="az"></param>
        /// <param name="aw"></param>
        /// <param name="bx"></param>
        /// <param name="by"></param>
        /// <param name="bz"></param>
        /// <param name="bw"></param>
        /// <param name="cx"></param>
        /// <param name="cz"></param>
        /// <param name="cy"></param>
        /// <param name="cw"></param>
        /// <param name="dx"></param>
        /// <param name="dy"></param>
        /// <param name="dz"></param>
        /// <param name="dw"></param>
        public Matrix4(double ax, double ay, double az, double aw,
                       double bx, double by, double bz, double bw,
                       double cx, double cy, double cz, double cw,
                       double dx, double dy, double dz, double dw)
        {
            this.ax = ax;
            this.ay = ay;
            this.az = az;
            this.aw = aw;

            this.bx = bx;
            this.by = by;
            this.bz = bz;
            this.bw = bw;

            this.cx = cx;
            this.cy = cy;
            this.cz = cz;
            this.cw = cw;

            this.dx = dx;
            this.dy = dy;
            this.dz = dz;
            this.dw = dw;
        }
        #endregion
        #region Indexers
        public unsafe double this[int row, int col]
        {
            get
            {
                fixed (Matrix4* ptr = &this)
                    return *((double*)ptr + (((row * 4) + col) % 16));
            }
        }
        public unsafe double this[int index]
        {
            get
            {
                fixed (Matrix4* ptr = &this)
                    return *((double*)ptr + (index % 16));
            }
        }
        #endregion
        #region Properties
        /// <summary>
        /// Gets the first row of the matrix.
        /// </summary>
        public Vector4d A
        {
            get
            {
                return new Vector4d(ax, ay, az, aw);
            }
        }

        /// <summary>
        /// Gets the second row of the matrix.
        /// </summary>
        public Vector4d B
        {
            get
            {
                return new Vector4d(bx, by, bz, bw);
            }
        }

        /// <summary>
        /// Gets the third row of the matrix.
        /// </summary>
        public Vector4d C
        {
            get
            {
                return new Vector4d(cx, cy, cz, cw);
            }
        }

        /// <summary>
        /// Gets the fourth row of the matrix.
        /// </summary>
        public Vector4d D
        {
            get
            {
                return new Vector4d(dx, dy, dz, dw);
            }
        }

        /// <summary>
        /// Gets the first column of the matrix.
        /// </summary>
        public Vector4d X
        {
            get
            {
                return new Vector4d(ax, bx, cx, dx);
            }
        }

        /// <summary>
        /// Gets the second column of the matrix.
        /// </summary>
        public Vector4d Y
        {
            get
            {
                return new Vector4d(ay, by, cy, dy);
            }
        }

        /// <summary>
        /// Gets the third column of the matrix.
        /// </summary>
        public Vector4d Z
        {
            get
            {
                return new Vector4d(az, bz, cz, dz);
            }
        }

        /// <summary>
        /// Gets the fourth column of the matrix.
        /// </summary>
        public Vector4d W
        {
            get
            {
                return new Vector4d(aw, bw, cw, dw);
            }
        }
        #endregion
        #region Methods
        #region Utility Methods
        /// <summary>
        /// Overridden. Determines whether the specified <see cref="System.Object"/>
        /// is equal to the current <see cref="Gear.Client.Matrix4"/>.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with the
        /// current <see cref="Gear.Client.Matrix4"/>.</param>
        /// <returns>true if the specified <see cref="System.Object"/> is equal to
        /// the current <see cref="Gear.Client.Matrix4"/>; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (obj is Matrix4)
                return this.Equals((Matrix4)obj);

            return false;
        }

        /// <summary>
        /// Determines whether two matrices are equal.
        /// </summary>
        /// <param name="other">The <see cref="Matrix4"/> to compare to.</param>
        /// <returns>true if the specified <see cref="Matrix4"/> is equal to
        /// the current <see cref="Matrix4"/>, otherwise false.</returns>
        public bool Equals(Matrix4 other)
        {
            return this == other;
        }

        /// <summary>
        /// Determines if two matrices are equal.
        /// </summary>
        /// <param name="a">The first <see cref="Matrix4"/> to compare.</param>
        /// <param name="b">The second <see cref="Matrix4"/> to compare.</param>
        /// <returns>true if both matrices have the same values, otherwise false.</returns>
        public static bool Equals(Matrix4 a, Matrix4 b)
        {
            return a == b;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        #endregion
        #region Arithmetic Methods
        /// <summary>
        /// Adds the current matrix and the specified matrix.
        /// </summary>
        /// <param name="other"></param>
        public Matrix4 Add(Matrix4 other)
        {
            return Matrix4.Add(this, other);
        }

        /// <summary>
        /// Adds two specified <see cref="Matrix4"/> instances and returns the result as a new instance.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Matrix4 Add(Matrix4 left, Matrix4 right)
        {
            return new Matrix4(left.ax + right.ax, left.ay + right.ay, left.az + right.az, left.aw + right.aw,
                               left.bx + right.bx, left.by + right.by, left.bz + right.bz, left.bw + right.bw,
                               left.cx + right.cx, left.cy + right.cy, left.cz + right.cz, left.cw + right.cw,
                               left.dx + right.dx, left.dy + right.dy, left.dz + right.dz, left.dw + right.dw);
        }

        public Matrix4 Multiply(Matrix4 other)
        {
            return Matrix4.Multiply(this, other);
        }

        /// <summary>
        /// Calculates the product of two matrices. This operation is not commutative.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Matrix4 Multiply(Matrix4 left, Matrix4 right)
        {
            return new Matrix4(left.ax * right.ax + left.ay * right.bx + left.az * right.cx + left.aw * right.dx,
                               left.ax * right.ay + left.ay * right.by + left.az * right.cy + left.aw * right.dy,
                               left.ax * right.az + left.ay * right.bz + left.az * right.cz + left.aw * right.dz,
                               left.ax * right.aw + left.ay * right.bw + left.az * right.cw + left.aw * right.dw,

                               left.bx * right.ax + left.by * right.bx + left.bz * right.cx + left.bw * right.dx,
                               left.bx * right.ay + left.by * right.by + left.bz * right.cy + left.bw * right.dy,
                               left.bx * right.az + left.by * right.bz + left.bz * right.cz + left.bw * right.dz,
                               left.bx * right.aw + left.by * right.bw + left.bz * right.cw + left.bw * right.dw,

                               left.cx * right.ax + left.cy * right.bx + left.cz * right.cx + left.cw * right.dx,
                               left.cx * right.ay + left.cy * right.by + left.cz * right.cy + left.cw * right.dy,
                               left.cx * right.az + left.cy * right.bz + left.cz * right.cz + left.cw * right.dz,
                               left.cx * right.aw + left.cy * right.bw + left.cz * right.cw + left.cw * right.dw,

                               left.dx * right.ax + left.dy * right.bx + left.dz * right.cx + left.dw * right.dx,
                               left.dx * right.ay + left.dy * right.by + left.dz * right.cy + left.dw * right.dy,
                               left.dx * right.az + left.dy * right.bz + left.dz * right.cz + left.dw * right.dz,
                               left.dx * right.aw + left.dy * right.bw + left.dz * right.cw + left.dw * right.dw);

        }

        public Matrix4 Multiply(double s)
        {
            return Matrix4.Multiply(this, s);
        }

        /// <summary>
        /// Calculates the scalar product of a specified matrix and scalar value.
        /// </summary>
        /// <param name="m"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        public static Matrix4 Multiply(Matrix4 m, double s)
        {
            return new Matrix4(m.ax * s, m.ay * s, m.az * s, m.aw * s,
                               m.bx * s, m.by * s, m.bz * s, m.bw * s,
                               m.cx * s, m.cy * s, m.cz * s, m.cw * s,
                               m.dx * s, m.dy * s, m.dz * s, m.dw * s);

        }
        public Vector3d Multiply(Vector3d vector)
        {
            return Matrix4.Multiply(this, vector);
        }
        public static Vector3d Multiply(Matrix4 matrix, Vector3d vector)
        {
            var v4 = matrix * new Vector4d(vector.X, vector.Y, vector.Z, 1.0);
            return new Vector3d(v4.X / v4.W, v4.Y / v4.W, v4.Z / v4.W);
        }

        public Vector4d Multiply(Vector4d v)
        {
            return Matrix4.Multiply(this, v);
        }

        public static Vector4d Multiply(Matrix4 m, Vector4d v)
        {
            return new Vector4d(m.ax * v.X + m.ay * v.Y + m.az * v.Z + m.aw * v.W,
                               m.bx * v.X + m.by * v.Y + m.bz * v.Z + m.bw * v.W,
                               m.cx * v.X + m.cy * v.Y + m.cz * v.Z + m.cw * v.W,
                               m.dx * v.X + m.dy * v.Y + m.dz * v.Z + m.dw * v.W);
        }

        public Matrix4 Subtract(Matrix4 other)
        {
            return Matrix4.Subtract(this, other);
        }

        public static Matrix4 Subtract(Matrix4 a, Matrix4 b)
        {
            return new Matrix4(a.ax - b.ax, a.ay - b.ay, a.az - b.az, a.aw - b.aw,
                               a.bx - b.bx, a.by - b.by, a.bz - b.bz, a.bw - b.bw,
                               a.cx - b.cx, a.cy - b.cy, a.cz - b.cz, a.cw - b.cw,
                               a.dx - b.dx, a.dy - b.dy, a.dz - b.dz, a.dw - b.dw);
        }
        /// <summary>
        /// Transposes the current matrix and returns the result as a new instance.
        /// </summary>
        /// <returns>A new <see cref="Matrix4"/> instance that is the result of the transposition.</returns>
        public Matrix4 Transpose()
        {
            return Matrix4.Transpose(this);
        }

        /// <summary>
        /// Transposes the specified matrix and returns the result as a new instance.
        /// </summary>
        /// <param name="m">A <see cref="Matrix4"/> instance to transpose.</param>
        /// <returns>A new <see cref="Matrix4"/> instance that is the result of the transposition.</returns>
        public static Matrix4 Transpose(Matrix4 m)
        {
            return new Matrix4(m.ax, m.bx, m.cx, m.dx,
                               m.ay, m.by, m.cy, m.dy,
                               m.az, m.bz, m.cz, m.dz,
                               m.aw, m.bw, m.cw, m.dw);
        }
        #endregion

        /// <summary>
        /// Creates and returns an orthographic projection matrix for the specified width and height.
        /// </summary>
        /// <param name="w">Width</param>
        /// <param name="h">Height</param>
        /// <returns>A <see cref="Matrix4"/> that is the orthographic projection matrix for the specified width and height.</returns>
        public static Matrix4 CreateOrthographicProjectionMatrix(double width, double height, double near, double far)
        {
            //var left = (width / -2.0);
            //var right = (width / 2.0);
            //var top = (height / 2.0);
            //var bottom = (height / -2.0);

            //return Matrix4.CreateOrthographicProjectionMatrix(right, top, far, left, bottom, near);

            return Matrix4.CreateOrthographicProjectionMatrix(1, 1, 1, -1, -1, -1);

        }

        /// <summary>
        /// Creates and returns an orthographic projection matrix for the specified set of clipping planes.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="top"></param>
        /// <param name="bottom"></param>
        /// <param name="near"></param>
        /// <param name="far"></param>
        /// <returns></returns>
        public static Matrix4 CreateOrthographicProjectionMatrix(double right, double top, double far, double left, double bottom, double near)
        {
            var x = 2.0 / (right - left);
            var y = 2.0 / (top - bottom);
            var z = 2.0 / (far - near);

            var a = (right + left) / (right - left) * -1.0;
            var b = (top + bottom) / (top - bottom) * -1.0;
            var c = (far + near) / (far - near) * -1.0;

            //TODO: Fix orthographic projection
            a = b = c = 0;
            x = y = z = 1.0;

            return new Matrix4(x, 0, 0, 0,
                               0, y, 0, 0,
                               0, 0, z, 0,
                               a, b, c, 1);
        }

        /// <summary>
        /// Creates and returns a perspective projection matrix for the specified values.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="near">The distance to the near-z clipping plane.</param>
        /// <param name="far">The distance to the far-z clipping plane.</param>
        /// <returns>A <see cref="Matrix4"/> that is the perspective projection matrix for the specified values.</returns>
        public static Matrix4 CreatePerspectiveProjectionMatrix(Angle fov, double aspect, double near, double far)
        {
            return Matrix4.CreatePerspectiveProjectionMatrix(fov.Radians, aspect, near, far);

            double fovyDegrees = 60;
            double fovy = Angle.RadiansFromDegrees(fovyDegrees);
            //var width = 800.0;
            //var height = 600.0;
            //var aspect = width / height;

            //var near = 1.0;
            //var far = 1000.0;

            double f = Math.Cos(fovy) / Math.Sin(fovy);

            double m00 = f / aspect;

            double m11 = f;
            double m22 = (near + far) / (near - far);
            double m32 = (2 * (far * near)) / (near - far);
            double m23 = -1;

            var m = new Matrix4(m00, 0, 0, 0,
                                       0, m11, 0, 0,
                                       0, 0, m22, m23,
                                       0, 0, m32, 0);

            return m;
        }

        public static Matrix4 CreatePerspectiveProjectionMatrix(double fov, double aspect, double near, double far)
        {
            double width = 0.2;
            double height = 0.2;

            return new Matrix4((2 * near) / width, 0, 0, 0,
                               0, (2 * near) / height, 0, 0,
                               0, 0, far / (far - near), 1,
                               0, 0, (-far * near) / (far - near), 0);

        }

        /// <summary>
        /// Creates a matrix that describes a translation operation.
        /// </summary>
        /// <param name="t">A <see cref="Vector3d"/> that describes the translation amount.</param>
        /// <returns>A new <see cref="Matrix4"/> that describes the translation operation.</returns>
        public static Matrix4 CreateTranslationMatrix(Vector3d t)
        {
            return new Matrix4(1, 0, 0, t.X,
                               0, 1, 0, t.Y,
                               0, 0, 1, t.Z,
                               0, 0, 0, 1);
        }

        /// <summary>
        /// Creates a matrix that describes a translation operation.
        /// </summary>
        /// <param name="x">The translation along the x axis.</param>
        /// <param name="y">The translation along the y axis.</param>
        /// <param name="z">The translation along the z axis.</param>
        /// <returns>A new <see cref="Matrix4"/> that describes the translation operation.</returns>
        public static Matrix4 CreateTranslationMatrix(double x, double y, double z)
        {
            return new Matrix4(1, 0, 0, x,
                               0, 1, 0, y,
                               0, 0, 1, z,
                               0, 0, 0, 1);
        }

        /// <summary>
        /// Creates a matrix that describes a scaling operation.
        /// </summary>
        /// <param name="s">A <see cref="Vector3d"/> that describes the x, y, and z value to scale by.</param>
        /// <returns>A new <see cref="Matrix4"/> that describes the scaling operation.</returns>
        public static Matrix4 CreateScalingMatrix(Vector3d s)
        {
            return new Matrix4(s.X, 0, 0, 0,
                               0, s.Y, 0, 0,
                               0, 0, s.Z, 0,
                               0, 0, 0, 1.0);
        }

        /// <summary>
        /// Creates a matrix that describes a scaling operation.
        /// </summary>
        /// <param name="s">The value to scale by along the x, y, and z axes.</param>
        /// <returns>A new <see cref="Matrix4"/> that describes the scaling operation.</returns>
        public static Matrix4 CreateScalingMatrix(double s)
        {
            return new Matrix4(s, 0, 0, 0,
                               0, s, 0, 0,
                               0, 0, s, 0,
                               0, 0, 0, 1);
        }

        /// <summary>
        /// Creates a matrix that describes a scaling operation.
        /// </summary>
        /// <param name="x">The value to scale by along the x axis.</param>
        /// <param name="y">The value to scale by along the y axis.</param>
        /// <param name="z">The value to scale by along the z axis.</param>
        /// <returns>A new <see cref="Matrix4"/> that describes the scaling operation.</returns>
        public static Matrix4 CreateScalingMatrix(double x, double y, double z)
        {
            return new Matrix4(x, 0, 0, 0,
                               0, y, 0, 0,
                               0, 0, z, 0,
                               0, 0, 0, 1);
        }

        /// <summary>
        /// Creates a matrix that describes a rotation operation.
        /// </summary>
        /// <param name="axis">A unit <see cref="Vector3d"/> that describes the axis along which the rotation is performed.</param>
        /// <param name="angle">The amount of rotation.</param>
        /// <returns>A new <see cref="Matrix4"/> that describes the rotation operation.</returns>
        public static Matrix4 CreateRotationMatrix(Vector3d axis, Angle angle)
        {
            var q = new Quaternion(axis, angle.Radians);
            q = (q.Length == 1) ? q : q.Normalized();

            var x = q.X;
            var y = q.Y;
            var z = q.Z;
            var s = q.W;

            return new Matrix4(
                1 - 2 * ((y * y) + (z * z)), 2 * ((x * y) - (s * z)), 2 * ((x * z) + (s * y)), 0,
                2 * ((x * y) + (s * z)), 1 - 2 * ((x * x) + (z * z)), 2 * ((y * z) - (s * x)), 0,
                2 * ((x * z) - (s * y)), 2 * ((y * z) + (s * x)), 1 - 2 * ((x * x) + (y * y)), 0,
                0, 0, 0, 1);
        }

        /// <summary>
        /// Creates a matrix that describes a rotation operation.
        /// </summary>
        /// <param name="x">The x-coordinate of the axis around which the rotation is performed.</param>
        /// <param name="y">The y-coordinate of the axis around which the rotation is performed.</param>
        /// <param name="z">The z-coordinate of the axis around which the rotation is performed.</param>
        /// <param name="a">The rotation amount, in radians.</param>
        /// <returns></returns>
        public static Matrix4 CreateRotationMatrix(double x, double y, double z, double a)
        {
            throw new NotImplementedException();
            //var q = new Quaternion(new Vector3(x, y, z), a);
            //var q = (q.Length == 1) ? q : q.Normalized();

            //var x = q.X;
            //var y = q.Y;
            //var z = q.Z;
            //var s = q.W;
            //return new Matrix4(
            //    1 - 2 * ((y * y) + (z * z)), 2 * ((x * y) - (s * z)), 2 * ((x * z) + (s * y)), 0,
            //    2 * ((x * y) + (s * z)), 1 - 2 * ((x * x) + (z * z)), 2 * ((y * z) - (s * x)), 0,
            //    2 * ((x * z) - (s * y)), 2 * ((y * z) + (s * x)), 1 - 2 * ((x * x) + (y * y)), 0,
            //    0, 0, 0, 1);

        }

        public static Matrix4 CreateRotationMatrix(Vector3d vec)
        {
            return Matrix4.CreateRotationMatrix(vec.X, vec.Y, vec.Z);
        }

        /// <summary>
        /// Creates a new matrix that represents a Euler rotation.
        /// </summary>
        /// <param name="x">The rotation amount (in degress) on the X plane.</param>
        /// <param name="y">The rotation amount (in degrees) on the Y plane.</param>
        /// <param name="z">The rotation amount (in degrees) on the Z plane.</param>
        /// <returns>A new <see cref="Matrix4"/> instance containing the result of the rotation.</returns>
        public static Matrix4 CreateRotationMatrix(double x, double y, double z)
        {
            x = Angle.RadiansFromDegrees(x % 360.0);
            y = Angle.RadiansFromDegrees(y % 360.0);
            z = Angle.RadiansFromDegrees(z % 360.0);

            var cosX = Math.Cos(x);
            var sinX = Math.Sin(x);
            var cosY = Math.Cos(y);
            var sinY = Math.Sin(y);
            var cosZ = Math.Cos(z);
            var sinZ = Math.Sin(z);

            var X = new Matrix4(1, 0, 0, 0,
                                0, cosX, -sinX, 0,
                                0, sinX, cosX, 0,
                                0, 0, 0, 1);
            var Y = new Matrix4(cosY, 0, sinY, 0,
                                0, 1, 0, 0,
                                -sinY, 0, cosY, 0,
                                0, 0, 0, 1);
            var Z = new Matrix4(cosZ, -sinZ, 0, 0,
                                sinZ, cosZ, 0, 0,
                                0, 0, 1, 0,
                                0, 0, 0, 1);

            return Z * Y * X;
        }

        public static Matrix4 CreateRotationMatrix(Quaternion rotation)
        {
            return rotation.ToRotationMatrix();
        }

        public override string ToString()
        {
            return string.Format("[{0}, {1}, {2}, {3}]\r\n", this.ax, this.ay, this.az, this.aw)
                + string.Format("[{0}, {1}, {2}, {3}]\r\n", this.bx, this.by, this.bz, this.bw)
                + string.Format("[{0}, {1}, {2}, {3}]\r\n", this.cx, this.cy, this.cz, this.cw)
                + string.Format("[{0}, {1}, {2}, {3}]\r\n", this.dx, this.dy, this.dz, this.dw);
        }
        #endregion
        #region Operators
        /// <summary>
        /// Calculates the sum of two matrices.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Matrix4 operator +(Matrix4 left, Matrix4 right)
        {
            return Matrix4.Add(left, right);
        }

        /// <summary>
        /// Calculates the difference of two matrices.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Matrix4 operator -(Matrix4 left, Matrix4 right)
        {
            return Matrix4.Subtract(left, right);
        }

        /// <summary>
        /// Determines if two matrices are equal.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator ==(Matrix4 left, Matrix4 right)
        {
            return left.ax == right.ax
                && left.bx == right.bx
                && left.cx == right.cx
                && left.dx == right.dx

                && left.ay == right.ay
                && left.by == right.by
                && left.cy == right.cy
                && left.dy == right.dy

                && left.az == right.az
                && left.bz == right.bz
                && left.cz == right.cz
                && left.dz == right.dz

                && left.aw == right.aw
                && left.bw == right.bw
                && left.cw == right.cw
                && left.dw == right.dw;
        }

        /// <summary>
        /// Determines if two matrices are inequal.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator !=(Matrix4 left, Matrix4 right)
        {
            return left.ax != right.ax
                || left.bx != right.bx
                || left.cx != right.cx
                || left.dx != right.dx

                || left.ay != right.ay
                || left.by != right.by
                || left.cy != right.cy
                || left.dy != right.dy

                || left.az != right.az
                || left.bz != right.bz
                || left.cz != right.cz
                || left.dz != right.dz

                || left.aw != right.aw
                || left.bw != right.bw
                || left.cw != right.cw
                || left.dw != right.dw;
        }

        /// <summary>
        /// Calculates the product of two matrices.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Matrix4 operator *(Matrix4 left, Matrix4 right)
        {
            return Matrix4.Multiply(left, right);
        }

        /// <summary>
        /// Calculates the scalar product of a matrix.
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        public static Matrix4 operator *(Matrix4 matrix, double scalar)
        {
            return Matrix4.Multiply(matrix, scalar);
        }

        /// <summary>
        /// Multiplies a matrix by a 3-dimensional vector.
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="vector"></param>
        /// <returns></returns>
        public static Vector3d operator *(Matrix4 matrix, Vector3d vector)
        {
            return Matrix4.Multiply(matrix, vector);
        }

        /// <summary>
        /// Multiplies a matrix and a <see cref="Vertex3d"/>, effectively applying a vertex transformation.
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="vertex"></param>
        /// <returns></returns>
        public static Vertex3d operator *(Matrix4 matrix, Vertex3d vertex)
        {
            Contract.Requires(vertex != null);

            var result = Matrix4.Multiply(matrix, vertex.Position);
            return new Vertex3d(result.X, result.Y, result.Z)
            {
                Color = vertex.Color,
                Flags = vertex.Flags
            };
        }

        /// <summary>
        /// Multiplies a matrix by a 4-dimensional vector.
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="vector"></param>
        /// <returns></returns>
        public static Vector4d operator *(Matrix4 matrix, Vector4d vector)
        {
            return Matrix4.Multiply(matrix, vector);
        }
        #endregion
    }
}
