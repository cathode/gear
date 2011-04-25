/******************************************************************************
 * Rust: A Managed Game Engine - http://trac.gearedstudios.com/rust/          *
 * Copyright © 2009-2011 Will 'cathode' Shelley. All Rights Reserved.         *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime;
using System.IO;
using SlimDX;
using System.Diagnostics;

namespace Rust
{
    /// <summary>
    /// Rust game engine.
    /// </summary>
    public static class Engine
    {
        #region Fields
        public static readonly EventLog Log = new EventLog(1);
        private static double deltaTime;
        private static Stopwatch stopwatch = new Stopwatch();
        private static volatile bool running = false;
        
        #endregion
        #region Events
        public static event EventHandler Tick;
        #endregion
        #region Properties
        public static double TickRate
        {
            get;
            set;
        }
        #endregion
        #region Methods
        public static void Initialize()
        {
            Engine.TickRate = 0.0;
        }
        public static void Start()
        {
#if DEBUG
            Console.WriteLine("Frequency: {0} (High-resolution: {1})", Stopwatch.Frequency, Stopwatch.IsHighResolution);
#endif
            Engine.running = true;
            Engine.stopwatch.Start();
            while (Engine.running)
                Engine.OnTick();
        }
        public static void Stop()
        {
            Engine.running = false;
        }
        private static void OnTick()
        {
            Engine.stopwatch.Stop();
            Engine.deltaTime = 1.0 / (Engine.stopwatch.ElapsedTicks % Stopwatch.Frequency);

            if (Engine.Tick != null)
                Engine.Tick(null, EventArgs.Empty);

            Engine.stopwatch.Restart();
        }
        #endregion
    }
}
