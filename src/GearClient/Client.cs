﻿/************************************************************************
 * Gear: A Steampunk Action-RPG - http://trac.gearedstudios.com/gear/   *
 * Copyright © 2009-2010 Will 'cathode' Shelley. All Rights Reserved.   *
 * -------------------------------------------------------------------- *
 * Contributors:                                                        *
 * - Will 'cathode' Shelley <cathode@live.com>                          *
 ************************************************************************/

namespace Gear.Client
{
    internal static class Client
    {
        #region Fields
        private static ClientEngine engine;
        #endregion
        #region Methods
        /// <summary>
        /// GearClient application entry point.
        /// </summary>
        /// <param name="args"></param>
        internal static void Main(string[] args)
        {
            // Initialize the client game engine.
            Client.engine = new ClientEngine();
        }
        #endregion
    }
}