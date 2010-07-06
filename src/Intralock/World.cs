using System;
using System.Collections.Generic;
using System.Text;

namespace Intralock
{
    /// <summary>
    /// Represents a virtual world.
    /// This class uses the singleton pattern; to obtain a <see cref="World"/> instance, retreive the value of the <see cref="World.Instance"/> property.
    /// </summary>
    public sealed class World
    {
        #region Fields

        /// <summary>
        /// Singleton instance of the <see cref="World"/> class.
        /// </summary>
        private static readonly World instance;

        #endregion
        #region Constructors

        /// <summary>
        /// Initializes static members of the <see cref="World"/> class.
        /// Explicit static constructor prevents this class from being marked as "beforefieldinit" by the compiler.
        /// Required for proper implementation of singleton pattern.
        /// </summary>
        static World()
        {
            World.instance = new World();
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="World"/> class from being created.
        /// </summary>
        private World()
        {
            if (instance != null)
                throw new InvalidOperationException();
        }

        #endregion
        #region Properties

        /// <summary>
        /// Gets the single instance of the <see cref="World"/> class.
        /// </summary>
        public static World Instance
        {
            get
            {
                return World.instance;
            }
        }

        #endregion
    }
}
