/******************************************************************************
 * Gear: An open-world sandbox game for creative people.                      *
 * http://github.com/cathode/gear/                                            *
 * Copyright © 2009-2017 William 'cathode' Shelley. All Rights Reserved.      *
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
    /// Represents hardware capabilities of a service's underlying host system.
    /// </summary>
    [ProtoContract]
    public class ServiceCapabilities
    {
        /// <summary>
        /// Gets or sets a value indicating the number of CPU cores in the service host system.
        /// </summary>
        [ProtoMember(1)]
        public int CoreCount { get; set; }

        /// <summary>
        /// Gets or sets a value indicating the number of logical execution threads for each CPU core.
        /// </summary>
        [ProtoMember(2)]
        public int ThreadsPerCore { get; set; }

        /// <summary>
        /// Gets or sets a value indicating the total memory (in bytes) available to the host system.
        /// </summary>
        [ProtoMember(3)]
        public ulong TotalMemory { get; set; }
    }
}
