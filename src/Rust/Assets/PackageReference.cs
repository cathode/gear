﻿/******************************************************************************
 * Rust: A Managed Game Engine - http://trac.gearedstudios.com/rust/          *
 * Copyright © 2009-2011 Will 'cathode' Shelley. All Rights Reserved.         *
 * This software is released under the terms and conditions of the Microsoft  *
 * Reference Source License (MS-RSL). See the 'license.txt' file for details. *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rust.Assets
{
    public sealed class PackageReference
    {
        public Guid ID
        {
            get;
            set;
        }
        public Version Version
        {
            get;
            set;
        }
        public byte[] Signature
        {
            get;
            set;
        }
        public bool RequireSpecificVersion
        {
            get;
            set;
        }
        public bool RequireDigitalSignature
        {
            get;
            set;
        }
    }
}
