﻿/******************************************************************************
 * Gear: An open-world sandbox game for creative people.                      *
 * http://github.com/cathode/gear/                                            *
 * Copyright © 2009-2014 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT        *
 * license. See the included LICENSE file for details.                        *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;

namespace Gear.Services
{
    /// <summary>
    /// Represents a message sent by a service that 
    /// </summary>
    [ProtoContract]
    public class ServiceDetail
    {
        [ProtoMember(1)]
        public Guid ServiceInstanceId { get; set; }
    }
}