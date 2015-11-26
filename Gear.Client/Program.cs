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
using Gear.Model;

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
            // test, generate world:
            var seed = 1234;
            // Init planet generator
            var world = new PlanetWorld(seed);
            // Setup generator settings
            var pmin = new PlanetWorldParameters();
            var pmax = new PlanetWorldParameters();
            pmax.AverageDensity = 6500; // Maximum 6500 kg/m3
            pmin.AverageDensity = 4000;
            pmax.DiameterKm = 14000;
            pmin.DiameterKm = 11000;

            world.Initialize(pmin, pmax);


            Net.MessageSerializationHelper.AddMessageSubtypes();

            var renderer = new Gear.Client.Rendering.OpenGL.GLRenderer();
            //var renderer = new Gear.Client.Rendering.Software.SoftwareRenderer();
            renderer.Initialize(new Rendering.RendererOptions());

            var scene = new SceneGraph.Scene();
            scene.Root = new SceneGraph.Node(new Modeling.Primitives.Cone(1.0, 2.0));

            renderer.Scene = scene;
            renderer.Start();

        }
    }
}
