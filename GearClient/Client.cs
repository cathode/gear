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
            var window = new OgreWindow();
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

        internal class SceneCreator
        {
            internal SceneCreator(OgreWindow window)
            {
                window.SceneCreating += new OgreWindow.SceneEventHandler(this.CreateScene);
            }
            private void CreateScene(OgreWindow window)
            {
                var mgr = window.SceneManager;

                mgr.AmbientLight = new ColourValue(1, 1, 1);

                var ent = mgr.CreateEntity("Robot", "robot.mesh");
                var node = mgr.RootSceneNode.CreateChildSceneNode("RobotNode");
                node.AttachObject(ent);

                var ent2 = mgr.CreateEntity("Robot2", "robot.mesh");
                var node2 = mgr.RootSceneNode.CreateChildSceneNode("RobotNode2", new Vector3(50, 0, 0));
                node2.AttachObject(ent2);
            }
        }
    }
}
