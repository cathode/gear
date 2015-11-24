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
using System.Windows.Forms;
using Gear.Client.UI;

namespace Gear.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            /* Gear Client main entry point.

             * Initialization process:
             * 
             * 1. Set up environment
             * 2. Set up logging
             * 3. ???
             * 4. PROFIT!!!
             */
            if (args.Length > 0)
            {
                // Check if the client is being invoked in 'shell-only' mode.
                if (args.Any(e => e == "--shell"))
                {
                    ShellProgram.Run();
                }

            }

            Net.MessageSerializationHelper.AddMessageSubtypes();

            var renderer = new Gear.Client.Rendering.Software.SoftwareRenderer();
            renderer.Initialize(new Rendering.RendererOptions());

            var scene = new SceneGraph.Scene();
            scene.Root = new SceneGraph.Node(new Modeling.Primitives.Cone(1.0, 2.0));

            renderer.Scene = scene;
            renderer.Start();

        }
    }
}
