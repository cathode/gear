﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Gear {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class EngineResources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal EngineResources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager current used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Gear.EngineResources", typeof(EngineResources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Displays help for a command..
        /// </summary>
        public static string ShellCmdHelp_Help {
            get {
                return ResourceManager.GetString("ShellCmdHelp_Help", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Quits the game..
        /// </summary>
        public static string ShellCmdHelp_Quit {
            get {
                return ResourceManager.GetString("ShellCmdHelp_Quit", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Sets a session parameter to a new value..
        /// </summary>
        public static string ShellCmdHelp_Set {
            get {
                return ResourceManager.GetString("ShellCmdHelp_Set", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No help is available for that command..
        /// </summary>
        public static string ShellErrorNoHelp {
            get {
                return ResourceManager.GetString("ShellErrorNoHelp", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unknown command: {0}.
        /// </summary>
        public static string ShellErrorUnknownCommand {
            get {
                return ResourceManager.GetString("ShellErrorUnknownCommand", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Client:.
        /// </summary>
        public static string ShellUIClient {
            get {
                return ResourceManager.GetString("ShellUIClient", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Server:.
        /// </summary>
        public static string ShellUIServer {
            get {
                return ResourceManager.GetString("ShellUIServer", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Gear Shell.
        /// </summary>
        public static string ShellUITitle {
            get {
                return ResourceManager.GetString("ShellUITitle", resourceCulture);
            }
        }
    }
}
