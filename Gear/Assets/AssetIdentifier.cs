/******************************************************************************
 * Gear: A Managed Game Engine - http://trac.gearedstudios.com/Gear/          *
 * Copyright © 2009-2011 Will 'cathode' Shelley. All Rights Reserved.         *
 * This software is released under the terms and conditions of the Microsoft  *
 * Reference Source License (MS-RSL). See the 'license.txt' file for details. *
 *****************************************************************************/
using System;

namespace Gear.Assets
{
    /// <summary>
    /// Identifies a specific asset without describing the location of the asset.
    /// </summary>
    public sealed class AssetIdentifier
    {
        #region Fields
        private AssetKind kind;
        private string name;
        private Version version;
        private Guid id;
        private string group;
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="AssetIdentifier"/> class.
        /// </summary>
        public AssetIdentifier()
        {
            this.Kind = AssetKind.Binary;
            this.Id = Guid.NewGuid();
            this.Name = string.Empty;
            this.Version = new Version();
            this.Group = "/";
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="AssetIdentifier"/> class.
        /// </summary>
        /// <param name="kind"></param>
        /// <param name="id"></param>
        /// <param name="version"></param>
        /// <param name="name"></param>
        /// <param name="group"></param>
        public AssetIdentifier(AssetKind kind, Guid id, Version version, string name, string group)
        {
            this.Kind = kind;
            this.Id = id;
            this.Version = version;
            this.Name = name;
            this.Group = group;
        }
        #endregion
        #region Properties
        public AssetKind Kind
        {
            get
            {
                return this.kind;
            }
            set
            {
                this.kind = value;
            }
        }
        public Version Version
        {
            get
            {
                return this.version;
            }
            set
            {
                this.version = value;
            }
        }
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value ?? string.Empty;
            }
        }
        public Guid Id
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }
        public string Group
        {
            get
            {
                return this.group;
            }
            set
            {
                this.group = value ?? "/";
            }
        }
        #endregion
    }
}
