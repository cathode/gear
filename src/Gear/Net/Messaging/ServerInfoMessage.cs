/******************************************************************************
 * Gear: A Steampunk Action-RPG - http://trac.gearedstudios.com/gear/         *
 * Copyright © 2009-2010 Will 'cathode' Shelley. All Rights Reserved.         *
 * This software is released under the terms and conditions of the Microsoft  *
 * Reference Source License (MS-RSL). See the 'license.txt' file for details. *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gear.Net.Messaging
{
    /// <summary>
    /// A <see cref="Message"/> implementation that encapsulates information about the server.
    /// </summary>
    /// <remarks>
    /// The server MUST send this message as the FIRST message sent upon accepting a connection from the client.
    /// </remarks>
    public sealed class ServerInfoMessage : Message
    {
        public override MessageId Id
        {
            get
            {
                return MessageId.ServerInfo;
            }
        }


        public override MessageField GetField(MessageFieldId id, byte tag)
        {
            throw new NotImplementedException();
        }
    }
}
