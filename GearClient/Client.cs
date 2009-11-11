// Gear - Copyright © 2009 Will Shelley. All Rights Reserved.
// Released under the Microsoft Reference License - see license.txt for details.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GearEngine;
using Mogre;
using MogreFramework;

namespace GearClient
{
    internal static class Client
    {
        #region Fields
        internal static ClientEngine Engine;
        #endregion
        #region Methods
        /// <summary>
        /// GearClient application entry point.
        /// </summary>
        /// <param name="args"></param>
        internal static void Main(string[] args)
        {
            // Initialize client engine
            Client.Engine = new ClientEngine();
            var shellUI = new GearEngine.Winforms.GameShellForm();
            shellUI.Shell = Client.Engine.Shell;

            // Initialize MOGRE rendering
            var window = new GearWindow();
            var creator = new Client.SceneCreator(window);

            try
            {
                shellUI.Show();
                window.Go();
            }
            catch (System.Runtime.InteropServices.SEHException)
            {
                if (OgreException.IsThrown)
                    MessageBox.Show(OgreException.LastException.FullDescription,
                                    "An Ogre exception has occurred!");
                else
                    throw;
            }
        }
        #endregion

        internal class GearWindow : OgreWindow
        {
            protected override void CreateCamera()
            {
                var cam = this.SceneManager.CreateCamera("PlayerCam");

                cam.Position = new Vector3(50, 50, 100);
                cam.LookAt(Vector3.ZERO);
                cam.NearClipDistance = 5;

                this.Camera = cam;
            }
            protected override void CreateViewport()
            {
                var vp = this.RenderWindow.AddViewport(this.Camera);

                vp.BackgroundColour = ColourValue.Black;
                this.Camera.AspectRatio = vp.ActualWidth / (float)vp.ActualHeight;

                this.Viewport = vp;
            }
        }

        internal class SceneCreator
        {
            internal SceneCreator(OgreWindow window)
            {
                window.SceneCreating += new OgreWindow.SceneEventHandler(this.CreateScene);
            }
            private void CreateScene(OgreWindow window)
            {
                var mgr = window.SceneManager;

                mgr.AmbientLight = ColourValue.Black;
                mgr.ShadowTechnique = ShadowTechnique.SHADOWTYPE_STENCIL_ADDITIVE;

                var ent = mgr.CreateEntity("Ninja", "ninja.mesh");
                ent.CastShadows = true;
                var node = mgr.RootSceneNode.CreateChildSceneNode("RobotNode");
                node.AttachObject(ent);

                var plane = new Plane(Vector3.UNIT_Y, 0);
                MeshManager.Singleton.CreatePlane("ground", ResourceGroupManager.DEFAULT_RESOURCE_GROUP_NAME, plane, 1500, 1500, 20, 20, true, 1, 5, 5, Vector3.UNIT_Z);
                ent = mgr.CreateEntity("GroundEntity", "ground");
                mgr.RootSceneNode.CreateChildSceneNode().AttachObject(ent);
                ent.SetMaterialName("Examples/Rockwall");
                ent.CastShadows = false;

                var light = mgr.CreateLight("Light1");
                light.Type = Light.LightTypes.LT_POINT;
                light.Position = new Vector3(0, 150, 250);
                light.DiffuseColour = ColourValue.Red;
                light.SpecularColour = ColourValue.Red;
            }
        }
    }
}
