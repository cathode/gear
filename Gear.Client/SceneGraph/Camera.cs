/******************************************************************************
 * Gear: An open-world sandbox game for creative people.                      *
 * http://github.com/cathode/gear/                                            *
 * Copyright © 2009-2016 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT        *
 * license. See the included LICENSE file for details.                        *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using Gear.Geometry;

namespace Gear.Client.SceneGraph
{
    /// <summary>
    /// Represents a point of view and associated properties.
    /// </summary>
    public sealed class Camera : Node
    {
        #region Fields
        /// <summary>
        /// Backing field for the <see cref="Camera.FieldOfView"/> property.
        /// </summary>
        private Angle fieldOfView;

        /// <summary>
        /// Backing field for the <see cref="Camera.FocalDistance"/> property.
        /// </summary>
        private double focalDistance;

        /// <summary>
        /// Backing field for the <see cref="Camera.Mode"/> property.
        /// </summary>
        private CameraMode mode;

        /// <summary>
        /// Backing field for the <see cref="Camera.ModifierLock"/> property.
        /// </summary>
        private CameraModifierLock modifierLock;
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Gear.Client.SceneGraph.Camera"/> class.
        /// </summary>
        public Camera()
        {
            this.fieldOfView = Angle.FromDegrees(45);
            this.VisibleGroups = VisibilityGroup.All;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Gear.Client.SceneGraph.Camera"/> class
        /// with the specified field-of-view.
        /// </summary>
        /// <param name="fieldOfView">An <see cref="Gear.Client.Angle"/> determining the new
        /// camera's field-of-view range.</param>
        public Camera(Angle fieldOfView)
        {
            this.fieldOfView = fieldOfView;
        }
        #endregion
        #region Properties
        /// <summary>
        /// Gets or sets the field-of-view of the current <see cref="Gear.Client.SceneGraph.Camera"/>.
        /// </summary>
        public Angle FieldOfView
        {
            get
            {
                return this.fieldOfView;
            }
            set
            {
                this.fieldOfView = value;
            }
        }

        /// <summary>
        /// Gets or sets the focal distance of the current <see cref="Gear.Client.SceneGraph.Camera"/>
        /// </summary>
        public double FocalDistance
        {
            get
            {
                return this.focalDistance;
            }
            set
            {
                this.focalDistance = value;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="Gear.Client.SceneGraph.CameraMode"/> of the current
        /// <see cref="Gear.Client.SceneGraph.Camera"/>.
        /// </summary>
        public CameraMode Mode
        {
            get
            {
                return this.mode;
            }
            set
            {
                this.mode = value;
            }
        }

        public CameraModifierLock ModifierLock
        {
            get
            {
                return this.modifierLock;
            }
            set
            {
                this.modifierLock = value;
            }
        }

        /// <summary>
        /// Gets or sets a predefined value that indicates the facing of the camera.
        /// </summary>
        public CameraFacing Facing
        {
            get;
            set;
        }

       
        public VisibilityGroup VisibleGroups
        {
            get;
            set;
        }
        #endregion
        #region Methods
        /// <summary>
        /// Creates a camera that provides an isometric view of the scene.
        /// </summary>
        /// <returns></returns>
        public static Camera CreateIsometric()
        {
            var cam = new Camera();
            cam.mode = CameraMode.Orthographic;
            cam.Facing = CameraFacing.Isometric;
            cam.UpdateFacing();

            return cam;
        }

        /// <summary>
        /// Creates a camera that is facing the specified direction by default.
        /// </summary>
        /// <param name="facing"></param>
        /// <returns></returns>
        public static Camera CreateWithFacing(CameraFacing facing)
        {
            var cam = new Camera();
            cam.mode = CameraMode.Orthographic;
            cam.Facing = facing;
            cam.UpdateFacing();
            return cam;
        }

        /// <summary>
        /// Updates the orientation of the camera to match the facing assigned to it.
        /// </summary>
        public void UpdateFacing()
        {
            switch (this.Facing)
            {
                case CameraFacing.Up:
                    this.Orientation = Quaternion.LookAt(Vector3d.Up);
                    break;

                case CameraFacing.Down:
                    this.Orientation = Quaternion.LookAt(Vector3d.Down);
                    break;

                case CameraFacing.East:
                    this.Orientation = Quaternion.LookAt(Vector3d.Right);
                    break;

                case CameraFacing.West:
                    this.Orientation = Quaternion.LookAt(Vector3d.Left);
                    break;

                case CameraFacing.North:
                    this.Orientation = Quaternion.LookAt(Vector3d.Forward);
                    break;

                case CameraFacing.South:
                    this.Orientation = Quaternion.LookAt(Vector3d.Backward);
                    break;

                case CameraFacing.Isometric:
                    this.Orientation = new Quaternion(new Vector3d(1.0, 0.0, 0.0), Angle.FromDegrees(35.264)) * new Quaternion(Vector3d.Up, Angle.FromDegrees(45));
                    break;
            }
        }
        #endregion
    }
}
