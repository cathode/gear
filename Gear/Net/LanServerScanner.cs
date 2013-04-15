/******************************************************************************
 * Gear: A Steampunk Action-RPG - http://trac.gearedstudios.com/gear/         *
 * Copyright © 2009-2013 William 'cathode' Shelley. All Rights Reserved.      *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics.Contracts;
using Gear.Serialization;

namespace Gear.Net
{

    public class ServerInfo
    {
        public IPEndPoint EndPoint
        {
            get;
            set;
        }

        public Version ServerVersion
        {
            get;
            set;
        }

        public int PlayerCount
        {
            get;
            set;
        }
    }

    /// <summary>
    /// Scans for local area network (LAN) servers and maintains a list of discovered servers.
    /// </summary>
    public static class LanServerScanner
    {
        /// <summary>
        /// Holds the port number that the client uses to listen for replies from servers.
        /// </summary>
        public const ushort ReplyToPort = 21037;

        /// <summary>
        /// Holds the UDP port number that the server listens on for broadcasts from clients.
        /// </summary>
        public const ushort ScanBroadcastPort = 21073;

        private static bool isScanning;
        private static TimeSpan duration;
        private static DateTime started;
        private static UdpClient client;

        private static List<ServerInfo> discoveredServers = new List<ServerInfo>();


        private GPacket GenerateBroadcastMessage()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Begins a scan of the local network for servers. The scan lasts for the specified number of seconds.
        /// </summary>
        /// <param name="duration"></param>
        public static void BeginDiscovery(int timeout)
        {
            Contract.Requires(timeout > 0);

            if ((started + duration) < DateTime.Now)
                isScanning = false;
            else if (isScanning)
                return;

            discoveredServers.Clear();
            isScanning = true;
            started = DateTime.Now;
            duration = TimeSpan.FromSeconds(timeout);

            if (client == null)
                client = new UdpClient(LanServerScanner.ReplyToPort);

            client.EnableBroadcast = true;
            client.MulticastLoopback = false;
            // Listen for replies.
            client.BeginReceive(new AsyncCallback(ReceiveCallback), client);

            var msg = new Gear.Net.Messaging.ClientInfoMessage();
            //msg.ClientId = this.clientId;
            msg.Name = "Debug-Test";
            //msg.Id = MessageId.ClientInfo;
            client.BeginSend(new byte[] { 0x01, 0x02, 0x03, 0x04 }, 4, new IPEndPoint(IPAddress.Broadcast, ScanBroadcastPort), new AsyncCallback(SendCallback), client);
        }

        private static void SendCallback(IAsyncResult ar)
        {
            Contract.Requires(ar != null);

            var client = ar.AsyncState as UdpClient;

            if (client == null)
                return;

            var sent = client.EndSend(ar);

            Log.Write("Sent " + sent.ToString() + " bytes.");
        }

        private static void ReceiveCallback(IAsyncResult ar)
        {
            Contract.Requires(ar != null);

            var client = ar.AsyncState as UdpClient;

            if (client == null)
                return;

            var ep = new IPEndPoint(IPAddress.Any, LanServerScanner.ReplyToPort);

            var data = client.EndReceive(ar, ref ep);
            Log.Write("received reply from server");
        }

        public class DiscoveredServerEventArgs
        {

        }
    }
}
