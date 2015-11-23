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
using System.Runtime;
using System.Diagnostics;
using System.Reflection;
using ProtoBuf;

namespace Gear.Net
{
    public class MessageSerializationHelper
    {
        public static void AddMessageSubtypes(Assembly asm = null)
        {
            if (asm == null)
                asm = Assembly.GetExecutingAssembly();

            var types = asm.GetTypes();
            var meta = ProtoBuf.Meta.RuntimeTypeModel.Default.Add(typeof(IMessage), true);

            foreach (var t in types.Where(e => e.GetInterfaces().Contains(typeof(IMessage))))
            {
                IMessage instance = Activator.CreateInstance(t) as IMessage;
                meta.AddSubType(instance.DispatchId, t);
            }
        }
    }
}
