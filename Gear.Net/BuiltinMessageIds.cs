﻿/******************************************************************************
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

namespace Gear.Net
{
    /// <summary>
    /// Contains static definitions of message dispatch id's for 1st party message types.
    /// </summary>
    public static class BuiltinMessageIds
    {
        // System

        /// <summary>
        /// Dispatch id for the <see cref="Gear.Net.Messages.PeerGreetingMessage"/> type.
        /// </summary>
        public static readonly int PeerGreeting = 0x0001;
        public static readonly int TeardownChannel = 0x0002;

        public static readonly int PeerHandoff = 0x0010;

        // File transfer
        public static readonly int TransferStream = 0x0100;
        public static readonly int TransferStreamData = 0x0101;
        public static readonly int TransferFileReceipt = 0x0102;
        public static readonly int StreamDataPortReady = 0x0103;
        public static readonly int FileReceiveComplete = 0x0104;

        /// <summary>
        /// Dispatch id for the <see cref="Gear.Net.ChannelPlugins.RequestStreamMessage"/> type.
        /// </summary>
        public static readonly int RequestStream = 0x0105;

        // Clustering
        public static readonly int Retarget = 0x0200;

        // Networked collections

        /// <summary>
        /// Dispatch id for the <see cref="Gear.Net.Collections.NetworkedCollectionUpdateMessage"/> type.
        /// </summary>
        public static readonly int NetworkedCollectionUpdate = 0x0180;

        /// <summary>
        /// Dispatch id for the <see cref="Gear.Net.Collections.NetworkedCollectionQueryRequestMessage"/> type.
        /// </summary>
        public static readonly int NetworkedCollectionQueryRequest = 0x0181;

        /// <summary>
        /// Dispatch id for the <see cref="Gear.Net.Collections.NetworkedCollectionUpdateMessage"/> type.
        /// </summary>
        public static readonly int NetworkedCollectionQueryResponse = 0x0182;

        /// <summary>
        /// Dispatch id for the <see cref="Gear.Net.Collections.NetworkedCollectionStateMessage"/> type.
        /// </summary>
        public static readonly int NetworkedCollectionAction = 0x0183;
    }
}