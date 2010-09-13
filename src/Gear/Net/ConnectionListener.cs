/******************************************************************************
 * Gear: A Steampunk Action-RPG - http://trac.gearedstudios.com/gear/         *
 * Copyright © 2009-2010 Will 'cathode' Shelley. All Rights Reserved.         *
 * This software is released under the terms and conditions of the Microsoft  *
 * Reference License (MS-RL). See the 'license.txt' file for details.         *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Gear.Net
{
    /// <summary>
    /// Listens for incoming connections over TCP/IP.
    /// </summary>
    public class ConnectionListener
    {
        #region Fields
        private Socket listener;
        #endregion
        #region Constructors

        #endregion
        #region Events

        #endregion
        #region Properties

        #endregion
        #region Methods
        public void Start()
        {
            Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public void Stop()
        {

        }
        #endregion
    }
}
