/******************************************************************************
 * Rust: A Managed Game Engine - http://trac.gearedstudios.com/rust/          *
 * Copyright © 2009-2011 Will 'cathode' Shelley. All Rights Reserved.         *
 * This software is released under the terms and conditions of the Microsoft  *
 * Reference Source License (MS-RSL). See the 'license.txt' file for details. *
 *****************************************************************************/
/******************************************************************************
 * Rust: A Managed Game Engine - http://trac.gearedstudios.com/rust/          *
 * Copyright © 2009-2011 Will 'cathode' Shelley. All Rights Reserved.         *
 * This software is released under the terms and conditions of the Microsoft  *
 * Reference Source License (MS-RSL). See the 'license.txt' file for details. *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Rust.Assets
{
    /// <summary>
    /// Compiles a package XML file into a binary package file that can be accessed at runtime.
    /// </summary>
    public sealed class PackageCompiler
    {
        public PackageCompiler()
        {
        }

        public bool Compile(string source, string target)
        {
            var doc = XDocument.Load(source, LoadOptions.PreserveWhitespace);
            var package = Package.Create(target);

            var root = doc.Root;

            // Compile header/metadata
            var meta = root.Element("Header");
            if (meta != null)
            {
                if (meta.Element("Name") != null)
                    package.Name = meta.Element("Name").Value;
                if (meta.Element("Summary") != null)
                    package.Description = meta.Element("Summary").Value;
                if (meta.Element("Author") != null)
                    package.Author = meta.Element("Author").Value;
                if (meta.Element("Company") != null)
                package.Company = meta.Element("Company").Value;
                if (meta.Element("Copyright") != null)
                package.Copyright = meta.Element("Copyright").Value;

                package.Id = new Guid(meta.Element("Id").Value);
                package.Version = new Version(meta.Element("Version").Value);
            }
            package.Close();

            return true;
        }
    }
}
