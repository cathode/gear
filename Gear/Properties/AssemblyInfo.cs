/******************************************************************************
 * Gear: An open-world sandbox game for creative people.                      *
 * http://github.com/cathode/gear/                                            *
 * Copyright © 2009-2016 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT        *
 * license. See the included LICENSE file for details.                        *
 *****************************************************************************/
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Gear;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("GearEngine")]
[assembly: AssemblyDescription("Main Game Engine Class Library")]

[assembly: InternalsVisibleTo("Gear.Client"),
           InternalsVisibleTo("Gear.Server")]