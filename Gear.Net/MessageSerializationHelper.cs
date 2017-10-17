﻿/******************************************************************************
 * Gear: An open-world sandbox game for creative people.                      *
 * http://github.com/cathode/gear/                                            *
 * Copyright © 2009-2017 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT        *
 * license. See the included LICENSE file for details.                        *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;

namespace Gear.Net
{
    public static class MessageSerializationHelper
    {
        public static void AddMessageSubtypes(Assembly asm = null)
        {
            if (asm == null)
            {
                asm = Assembly.GetExecutingAssembly();
            }

            var types = asm.GetTypes();
            var meta = ProtoBuf.Meta.RuntimeTypeModel.Default.Add(typeof(IMessage), true);

            foreach (var t in types.Where(e => e.GetInterfaces().Contains(typeof(IMessage))))
            {
                IMessage instance = Activator.CreateInstance(t) as IMessage;
                meta.AddSubType(instance.DispatchId, t);
                MessageSerializationHelper.OnMessageTypeDiscovered(instance.DispatchId, t);
            }
        }

        private static void OnMessageTypeDiscovered(int dispatchId, Type t)
        {
            MessageSerializationHelper.MessageTypeDiscovered?.Invoke(null, new MessageTypeDiscoveredEventArgs { DispatchId = dispatchId, DiscoveredType = t });
        }

        public static event EventHandler<MessageTypeDiscoveredEventArgs> MessageTypeDiscovered;
    }

    
}