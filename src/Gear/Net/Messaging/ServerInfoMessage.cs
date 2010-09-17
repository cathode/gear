﻿/******************************************************************************
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
        #region Fields
        public const string DefaultMotd = "Message of the Day";
        private StringField motd;
        private VersionField version;
        #endregion
        #region Constructors
        public ServerInfoMessage()
        {
            this.motd = new StringField(ServerInfoMessage.DefaultMotd);
        }
        #endregion
        #region Properties
        public string Motd
        {
            get
            {
                return this.motd.Value;
            }
            set
            {
                this.motd.Value = value;
            }
        }

        public override MessageId Id
        {
            get
            {
                return MessageId.ServerInfo;
            }
        }
        #endregion
        #region Methods
        public override MessageField GetField(MessageFieldId id, short tag)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
