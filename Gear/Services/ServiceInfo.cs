/******************************************************************************
 * Gear: An open-world sandbox game for creative people.                      *
 * http://github.com/cathode/gear/                                            *
 * Copyright © 2009-2016 William 'cathode' Shelley. All Rights Reserved.      *
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
    /// Represents the data in a service announcement which is broadcast over the LAN.
    /// </summary>
    [ProtoContract]
    public class ServiceInfo
    {
        /// <summary>
        /// Gets or sets the instance id of the service.
        /// </summary>
        [ProtoMember(1, IsRequired = true)]
        public Guid ServiceInstanceId { get; set; }

        /// <summary>
        /// Gets or sets the service identifier.
        /// </summary>
        [ProtoMember(2, IsRequired = true)]
        public ServerService ServiceType { get; set; }

        [ProtoMember(3, IsRequired = true)]
        public string ListenAddress { get; set; }

        [ProtoMember(4, IsRequired = true)]
        public ushort ListenPort { get; set; }

        [ProtoMember(5, IsRequired = false)]
        public string ServiceName { get; set; }

        [ProtoMember(6, IsRequired = false)]
        public ServiceDetail Detail { get; set; }
    }
}
