using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime;
using System.IO;

namespace Rust
{
    /// <summary>
    /// Rust game engine.
    /// </summary>
    public static class Engine
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
            Engine.scannedPlugins.Clear();

            return new Guid[0];
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
            if (!Engine.pluginSearchPaths.Contains(fullPath))
                Engine.pluginSearchPaths.Add(fullPath);
        }

        /// <summary>
        /// Unregisters a previously registered plugin search path.
        /// </summary>
        /// <param name="path"></param>
        public static void UnregisterGamePluginSearchPath(string path)
        {
            Engine.pluginSearchPaths.Remove(path);
        }
        #endregion
    }
}
