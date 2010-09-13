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

namespace Gear.Net
{
    /// <summary>
    /// A <see cref="Message"/> implementation that encapsulates information about the client.
    /// </summary>
    /// <remarks>
    /// The client MUST send this message as the FIRST message upon establishing a connection with a server.
    /// </remarks>
    public sealed class ClientInfoMessage : Message
    {
        protected override MessageId Id
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}
