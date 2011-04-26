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
using System.IO;

namespace Gear.Assets
{
    public static class PackageManager
    {
        #region Fields
        private static readonly List<string> searchPaths = new List<string>();
        private static readonly List<Guid> skip = new List<Guid>();
        private static readonly List<Package> loaded = new List<Package>();
        #endregion
        #region Methods
        public static void RegisterPackageSearchPath(string path)
        {
            string fullPath = Path.GetFullPath(path);
            if (!Directory.Exists(path))
                throw new DirectoryNotFoundException(fullPath);
            if (!PackageManager.searchPaths.Contains(fullPath))
                PackageManager.searchPaths.Add(fullPath);
        }

        public static Package[] LoadAllPackages()
        {
            var loaded = new List<Package>();

            foreach (var path in from p in PackageManager.searchPaths
                                 from f in Directory.GetFiles(p)
                                 where Path.GetExtension(f) == Package.DefaultFileExtension
                                 select f)
            {
               
                    var pkg = Package.Open(path);
                    if (!PackageManager.skip.Contains(pkg.Id))
                        loaded.Add(pkg);
                    if (!PackageManager.loaded.Any(x => x.Id == pkg.Id))
                        PackageManager.loaded.Add(pkg);
               
            }
            return loaded.ToArray();
        }
        #endregion
    }
}
