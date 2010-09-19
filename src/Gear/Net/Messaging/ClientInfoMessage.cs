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
    /// A <see cref="Message"/> implementation that encapsulates information about the client.
    /// </summary>
    /// <remarks>
    /// The client MUST send this message as the FIRST message upon establishing a connection with a server.
    /// </remarks>
    public sealed class ClientInfoMessage : Message
    {
        #region Fields
        private readonly GuidField clientId;
        private readonly StringField name;
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new current of the <see cref="ClientInfoMessage"/> class.
        /// </summary>
        public ClientInfoMessage()
        {
            this.clientId = new GuidField();
            this.name = new StringField();

            this.Fields = new Field[]
            {
                this.clientId,
                this.name,
            };

            this.ClientId = Guid.NewGuid();
        }
        #endregion
        #region Properties
        /// <summary>
        /// Gets or sets the unique id of the client.
        /// </summary>
        public Guid ClientId
        {
            get
            {
                return this.clientId.Value;
            }
            set
            {
                this.clientId.Value = value;
            }
        }
        public string Name
        {
            get
            {
                return this.name.Value;
            }
            set
            {
                this.name.Value = value;
            }
        }

        /// <summary>
        /// Gets the <see cref="MessageId"/> of the current <see cref="Message"/>.
        /// </summary>
        public override MessageId Id
        {
            get
            {
                return MessageId.ClientInfo;
            }
        }
        #endregion
        #region Methods
        public override Field GetField(FieldKind id, short tag)
        {
            if (id == FieldKind.Guid)
                return this.clientId;
            else if (id == FieldKind.String)
                return this.name;

            return null;
        }
        #endregion
    }
}
