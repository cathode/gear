/******************************************************************************
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
using Gear.Net.Messages;

namespace Gear.Services
{
    /// <summary>
    /// Provides the service that manages the simulation of zones.
    /// </summary>
    public class ZoneNodeService : ServiceBase
    {
        /// <summary>
        /// Handles requests from a client for zone data.
        /// </summary>
        /// <param name="message"></param>
        [ServiceMessageHandler]
        public void Handler_ClientRequestingZoneData(ZoneDataRequestMessage message)
        {
            //TODO: Implement some type of transactional locking so that any block updates for the zone are sent to the client.
            // 1. Determine if this zone node is authoritative for the requested zone data.

            // 2. Retrieve the zone data

            // 2a. If the requested zone data doesn't yet exist, it needs to be generated.

            // 2b. Wait until the necessary chunks are generated.

            // 3. Push the zone data into a message

            // 4. Queue the message for sending

        }

        /// <summary>
        /// Message handler for client-initiated block update.
        /// </summary>
        /// <param name="message"></param>
        [ServiceMessageHandler]
        public void Handler_PeerSendingBlockUpdate(BlockUpdateMessage message)
        {

        }
    }
}
