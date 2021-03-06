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
using System.Diagnostics.Contracts;
using System.Threading;

namespace Gear.Geometry
{
    /// <summary>
    /// Represents an axis-aligned bounding box specified by two points in 3d space. This class is immutable.
    /// </summary>
    /// <remarks>
    /// The first point (A) is the front, top, right corner of the box. The second point
    /// (B) is the back, bottom, left corner of the box.
    /// </remarks>
    public class Extents3d
    {
        #region Fields
        public static readonly Extents3d Empty = new Extents3d(0, 0, 0);

        private readonly double right;
        private readonly double top;
        private readonly double front;
        private readonly double left;
        private readonly double bottom;
        private readonly double back;
        #endregion
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Extents3d"/> class.
        /// </summary>
        public Extents3d()
        {
            this.left = 0;
            this.right = 0;
            this.top = 0;
            this.bottom = 0;
            this.front = 0;
            this.back = 0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Extents3d"/> class.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public Extents3d(double x, double y, double z)
        {
            this.right = x / 2.0;
            this.left = x / -2.0;

            this.top = y / 2.0;
            this.bottom = y / -2.0;

            this.front = z / 2.0;
            this.back = z / -2.0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Extents3d"/> class.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public Extents3d(Vector3d a, Vector3d b)
            : this(a.X, a.Y, a.Z, b.X, b.Y, b.Z)
        {
            Contract.Requires(a.X >= b.X);
            Contract.Requires(a.Y >= b.Y);
            Contract.Requires(a.Z >= b.Z);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Extents3d"/> class.
        /// </summary>
        /// <param name="right"></param>
        /// <param name="top"></param>
        /// <param name="front"></param>
        /// <param name="left"></param>
        /// <param name="bottom"></param>
        /// <param name="back"></param>
        public Extents3d(double right, double top, double front, double left, double bottom, double back)
        {
            Contract.Requires(right >= left);
            Contract.Requires(front >= back);
            Contract.Requires(top >= bottom);

            this.right = right;
            this.top = top;
            this.front = front;
            this.left = left;
            this.bottom = bottom;
            this.back = back;
        }

        #endregion
        #region Properties
        public Vector3d A
        {
            get
            {
                return new Vector3d(this.right, this.top, this.front);
            }
        }

        public Vector3d B
        {
            get
            {
                return new Vector3d(this.left, this.bottom, this.back);
            }
        }

        public double Right
        {
            get
            {
                return this.right;
            }
        }

        public double Left
        {
            get
            {
                return this.left;
            }
        }

        public double Front
        {
            get
            {
                return this.front;
            }
        }

        public double Back
        {
            get
            {
                return this.back;
            }
        }

        public double Top
        {
            get
            {
                return this.top;
            }
        }

        public double Bottom
        {
            get
            {
                return this.bottom;
            }
        }

        public double Width
        {
            get
            {
                return this.Right - this.Left;
            }
        }

        public double Length
        {
            get
            {
                return this.Front - this.Back;
            }
        }

        public double Height
        {
            get
            {
                return this.Top - this.Bottom;
            }
        }

        #endregion
        #region Methods
        public static bool Equals(Extents3d e1, Extents3d e2)
        {
            if (Extents3d.ReferenceEquals(e1, null))
            {
                return Extents3d.ReferenceEquals(e2, null);
            }
            else if (Extents3d.ReferenceEquals(e2, null))
            {
                return Extents3d.ReferenceEquals(e1, null);
            }
            else
            {
                return e1 == e2;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is Extents3d)
            {
                return this == (Extents3d)obj;
            }

            return false;
        }

        public bool Equals(Extents3d other)
        {
            return Extents3d.Equals(this, other);
        }

        public Vector3d FindMidpoint()
        {
            return new Vector3d((this.left + this.right) / 2.0, (this.top + this.bottom) / 2.0, (this.front + this.back) / 2.0);
        }

        public override string ToString()
        {
            return string.Format("{0}, {1}", this.A, this.B);
        }

        public override int GetHashCode()
        {
            return this.A.GetHashCode() ^ (this.B.GetHashCode() * 31);
        }

        /// <summary>
        /// Invariant contracts for the <see cref="Extents3d"/> class.
        /// </summary>
        [ContractInvariantMethod]
        private void Invariants()
        {
            Contract.Invariant(this.Width >= 0);
            Contract.Invariant(this.Length >= 0);
            Contract.Invariant(this.Height >= 0);
            Contract.Invariant(this.Left <= this.Right);
            Contract.Invariant(this.Bottom <= this.Top);
            Contract.Invariant(this.Back <= this.Front);
        }
        #endregion
        #region Operators

        /// <summary>
        /// Determines if the current <see cref="Extents3d"/> fully contains the space defined by the specified <see cref="Extents3d"/>.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool FullyContains(Extents3d other)
        {
            return this.Right >= other.Right
                && this.Top >= other.Top
                && this.Front >= other.Front
                && this.Left <= other.Left
                && this.Bottom <= other.Bottom
                && this.Back <= other.Back;
        }

        public static Extents3d operator |(Extents3d e1, Extents3d e2)
        {
            return new Extents3d(
                e1.Right >= e2.Right ? e1.Right : e2.Right,
                                e1.Top >= e2.Top ? e1.Top : e2.Top,
                                e1.Front >= e2.Front ? e1.Front : e2.Front,

                                e1.Left <= e2.Left ? e1.Left : e2.Left,
                                e1.Bottom <= e2.Bottom ? e1.Bottom : e2.Bottom,
                                e1.Back <= e2.Back ? e1.Back : e2.Back);
        }

        public static bool operator ==(Extents3d e1, Extents3d e2)
        {
            if (object.ReferenceEquals(e1, null))
            {
                if (object.ReferenceEquals(e2, null))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (object.ReferenceEquals(e2, null))
            {
                return false;
            }

            return e1.right == e2.right
                && e1.left == e2.left
                && e1.front == e2.front
                && e1.back == e2.back
                && e1.top == e2.top
                && e1.bottom == e2.bottom;
        }

        public static bool operator !=(Extents3d e1, Extents3d e2)
        {
            return e1.right != e2.right
                || e1.left != e2.left
                || e1.front != e2.front
                || e1.back != e2.back
                || e1.top != e2.top
                || e1.bottom != e2.bottom;
        }
        #endregion

    }
}
