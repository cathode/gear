/******************************************************************************
 * Gear: A Steampunk Action-RPG - http://trac.gearedstudios.com/gear/         *
 * Copyright © 2009-2011 Will 'cathode' Shelley. All Rights Reserved.         *
 * This software is released under the terms and conditions of the Microsoft  *
 * Reference Source License (MS-RSL). See the 'license.txt' file for details. *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gear.Net
{
    public sealed class MessageReceiveState
    {
        public byte[] Buffer
        {
            get;
            set;
        }
        public int ReceivedBytes
        {
            get;
            set;
        }
        public Message Message
        {
            get;
            set;
        }
        public bool HeaderDone
        {
            get;
            set;
        }
        public int Payload
        {
            get;
            set;
        }
        public byte FieldCount
        {
            get;
            set;
        }
    }
}
