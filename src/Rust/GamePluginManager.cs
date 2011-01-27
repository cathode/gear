/******************************************************************************
 * Rust: A Managed Game Engine - http://trac.gearedstudios.com/rust/          *
 * Copyright © 2009-2010 Will 'cathode' Shelley. All Rights Reserved.         *
 * This software is released under the terms and conditions of the Microsoft  *
 * Reference Source License (MS-RSL). See the 'license.txt' file for details. *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Rust
{
    public static class GamePluginManager
    {
        #region Fields
        private static readonly List<string> pluginSearchPaths = new List<string>();
        private static readonly Dictionary<Guid, string> scannedPlugins = new Dictionary<Guid, string>();
        #endregion
        #region Properties

        #endregion
        #region Methods
        /// <summary>
        /// Loads the game plugin assembly given a unique game ID.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static System.Reflection.Assembly LoadGamePlugin(Guid gameID)
        {
            throw new NotImplementedException();
        }

        public static Guid[] ScanForGamePlugins()
        {
            GamePluginManager.scannedPlugins.Clear();

            return new Guid[0];
        }

        public static IEnumerable<GamePlugin> GetAvailablePlugins()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Registers the specified path as a location where game plugins will be scanned/loaded from.
        /// </summary>
        /// <param name="path"></param>
        public static void RegisterGamePluginSearchPath(string path)
        {
            string fullPath = Path.GetFullPath(path);
            if (!Directory.Exists(path))
                throw new DirectoryNotFoundException(fullPath);
            if (!GamePluginManager.pluginSearchPaths.Contains(fullPath))
                GamePluginManager.pluginSearchPaths.Add(fullPath);
        }

        /// <summary>
        /// Unregisters a previously registered plugin search path.
        /// </summary>
        /// <param name="path"></param>
        public static void UnregisterGamePluginSearchPath(string path)
        {
            GamePluginManager.pluginSearchPaths.Remove(path);
        }
        #endregion
    }
}
