﻿/******************************************************************************
 * Gear: A Steampunk Action-RPG - http://trac.gearedstudios.com/gear/         *
 * Copyright © 2009-2010 Will 'cathode' Shelley. All Rights Reserved.         *
 * This software is released under the terms and conditions of the Microsoft  *
 * Reference Source License (MS-RSL). See the 'license.txt' file for details. *
 *****************************************************************************/
using System;

namespace Gear.Net
{
    /// <summary>
    /// Represents a message sent over the network from one endpoint to another.
    /// </summary>
    public abstract class Message
    {
        #region Properties
        public abstract MessageId Id
        {
            get;
        }
        #endregion
        #region Methods

        #endregion
    }
}
