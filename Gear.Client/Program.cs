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
            // test, generate world:
            var seed = 1234;
            // Init planet generator
            var gen = new Gear.Model.Generators.PlanetGenerator();
            // Setup generator settings
            var pmin = new Gear.Model.Generators.PlanetGeneratorParameters();
            var pmax = new Gear.Model.Generators.PlanetGeneratorParameters();
            pmax.AverageDensity = 6500; // Maximum 6500 kg/m3
            pmin.AverageDensity = 4000;
            pmax.DiameterKm = 14000;
            pmin.DiameterKm = 11000;
            

            gen.ParametersMaximum = pmax;
            gen.ParametersMinimum = pmin;

            var world = gen.GenerateWorld(seed);


            Net.MessageSerializationHelper.AddMessageSubtypes();

            var renderer = new Gear.Client.Rendering.OpenGL.GLRenderer();
            renderer.Initialize(new Rendering.RendererOptions());

            var scene = new SceneGraph.Scene();
            scene.Root = new SceneGraph.Node(new Modeling.Primitives.Cone(1.0, 2.0));

            renderer.Scene = scene;
            renderer.Start();

        }
    }
}
