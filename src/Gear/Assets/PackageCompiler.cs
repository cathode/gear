/******************************************************************************
 * Gear: A Steampunk Action-RPG - http://trac.gearedstudios.com/gear/         *
 * Copyright © 2009-2011 Will 'cathode' Shelley. All Rights Reserved.         *
 * This software is released under the terms and conditions of the Microsoft  *
 * Reference Source License (MS-RSL). See the 'license.txt' file for details. *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Gear.Assets
{
    /// <summary>
    /// Provides a configurable game asset package compiler.
    /// </summary>
    public sealed class PackageCompiler
    {
        #region Fields
        /// <summary>
        /// Backing field for the <see cref="PackageCompiler.InputFile"/> property.
        /// </summary>
        private string inputFile;

        /// <summary>
        /// Backing field for the <see cref="PackageCompiler.OutputFile"/> property.
        /// </summary>
        private string outputFile;

        private Stream stream;
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="PackageCompiler"/> class.
        /// </summary>
        public PackageCompiler()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PackageCompiler"/> class.
        /// </summary>
        /// <param name="inputFile">The path to the input file.</param>
        public PackageCompiler(string inputFile)
        {
            this.inputFile = inputFile;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PackageCompiler"/> class.
        /// </summary>
        /// <param name="inputFile">The path to the input file.</param>
        /// <param name="outputFile">The path to the output file.</param>
        public PackageCompiler(string inputFile, string outputFile)
        {
            this.inputFile = inputFile;
            this.outputFile = outputFile;
        }
        #endregion
        #region Properties
        /// <summary>
        /// Gets or sets the path to the XML file defining the package to compile.
        /// </summary>
        public string InputFile
        {
            get
            {
                return this.inputFile;
            }
            set
            {
                this.inputFile = value;
            }
        }

        /// <summary>
        /// Gets or sets the path to the compiled package file.
        /// </summary>
        /// <remarks>
        /// If this is set to anything other than null before the <see cref="PackageCompiler.Compile"/> method is called,
        /// this output file path will be used instead of anything defined in input file's XML.
        /// </remarks>
        public string OutputFile
        {
            get
            {
                return this.outputFile;
            }
            set
            {
                this.outputFile = value;
            }
        }

        /// <summary>
        /// Gets or sets a value that indicates whether the compilation should fail if the target of a file import is missing.
        /// </summary>
        public bool FailOnMissingImport
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a vlaue that indicates whether a partially compiled package should be deleted if the compilation fails before completing.
        /// </summary>
        public bool DeletePartialPackageOnFail
        {
            get;
            set;
        }
        #endregion
        #region Methods
        /// <summary>
        /// Runs the compilation.
        /// </summary>
        /// <returns>true if the compilation was successful; otherwise, false.</returns>
        public bool Compile()
        {
            if (this.OutputFile == null)
                this.OutputFile = Path.ChangeExtension(this.InputFile, Package.DefaultFileExtension);

            var doc = XDocument.Load(this.InputFile);
            var root = doc.Root;

            var name = root.Attribute("Name").Value;
            var version = Version.Parse(root.Attribute("Version").Value);
            var description = (root.Attribute("Description") ?? new XAttribute("Description", null)).Value;
            var id = Guid.Parse(root.Attribute("Id").Value);

            // Pre-scan entire package and generate a list of AssetIdentifiers and the XPath to the element containing the asset they identify.
            PreScanState state = new PreScanState();
            state.Group = root;
            this.PreScan(state);

            // Prep data
            if (name.Length > byte.MaxValue)
                name = name.Substring(0, byte.MaxValue);

            // Write header to package
            int length = 4; // fourcc
            length += 16; // id
            length += 32; // version
            length += Encoding.UTF8.GetByteCount(name);
            length += 1; // prefix for name
            length += Encoding.UTF8.GetByteCount(description);
            length += 4; // prefix for description
            length += 4; // index offset
            DataBuffer buffer = new DataBuffer(length, DataBufferMode.BigEndian);

            buffer.WriteInt32(Package.FourCC);
            buffer.WriteGuid(id);
            buffer.WriteVersion(version);
            buffer.WriteByte((byte)name.Length);
            buffer.WriteStringUtf8(name);
            buffer.WriteInt32(description.Length);
            buffer.WriteStringUtf8(description);

            var indexPlaceholderOffset = buffer.Position;
            buffer.WriteInt32(0); // Allow for the position of the asset index to be added later.

            this.stream = File.OpenWrite(this.OutputFile);
            this.stream.Write(buffer.Contents, 0, buffer.Contents.Length);

            // Scan each element and add asset data.
            foreach (var pair in from p in state.Assets
                                 orderby p.Value.Kind
                                 select p)
            {
                Console.WriteLine("Compiling: {0}{1}", pair.Value.Group, pair.Value.Name);
            }

            this.stream.Close();
            return true;
        }

        private void PreScan(PreScanState state)
        {
            Console.WriteLine("PreScanning Group: {0}", state.Path);
            var workingGroup = state.Group;
            foreach (var group in state.Group.Elements("Group"))
            {
                state.Group = group;
                var append = group.Attribute("Name").Value + "/";
                state.Path += append;
                this.PreScan(state);
                state.Path = state.Path.Substring(0, state.Path.Length - append.Length);
            }
            state.Group = workingGroup;

            // == Pre-scan items in current group ==
            var kinds = Enum.GetValues(typeof(AssetKind));

            foreach (var kind in kinds.Cast<AssetKind>())
                foreach (var node in state.Group.Elements(Enum.GetName(typeof(AssetKind), kind)))
                    state.Assets.Add(node.GetAbsoluteXPath(),
                        new AssetIdentifier(kind, 
                            Guid.Parse((node.Attribute("Id") ?? new XAttribute("Version", Guid.Empty.ToString())).Value),
                            Version.Parse((node.Attribute("Version") ?? new XAttribute("Version", "0.0.0.0")).Value),
                            node.Attribute("Name").Value, state.Path));
        }
        #endregion
        #region Types
        private sealed class PreScanState
        {
            internal XElement Group;
            internal string Path = "/";
            internal readonly Dictionary<string, AssetIdentifier> Assets = new Dictionary<string, AssetIdentifier>();
        }

        internal sealed class CompileState
        {
            internal DataBuffer Buffer;
        }
        #endregion
    }
    internal static class XExtensions
    {
        /// <summary>
        /// Get the absolute XPath to a given XElement, including the namespace.
        /// (e.g. "/a:people/b:person[6]/c:name[1]/d:last[1]").
        /// </summary>
        internal static string GetAbsoluteXPath(this XElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }

            Func<XElement, string> relativeXPath = e =>
            {
                int index = e.IndexPosition();

                var currentNamespace = e.Name.Namespace;

                string name;
                if (currentNamespace == null)
                {
                    name = e.Name.LocalName;
                }
                else
                {
                    string namespacePrefix = e.GetPrefixOfNamespace(currentNamespace);
                    name = namespacePrefix + ":" + e.Name.LocalName;
                }

                // If the element is the root, no index is required
                return (index == -1) ? "/" + name : string.Format
                (
                    "/{0}[{1}]",
                    name,
                    index.ToString()
                );
            };

            var ancestors = from e in element.Ancestors()
                            select relativeXPath(e);

            return string.Concat(ancestors.Reverse().ToArray()) +
                   relativeXPath(element);
        }

        /// <summary>
        /// Get the index of the given XElement relative to its
        /// siblings with identical names. If the given element is
        /// the root, -1 is returned.
        /// </summary>
        /// <param name="element">
        /// The element to get the index of.
        /// </param>
        internal static int IndexPosition(this XElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }

            if (element.Parent == null)
            {
                return -1;
            }

            int i = 1; // Indexes for nodes start at 1, not 0

            foreach (var sibling in element.Parent.Elements(element.Name))
            {
                if (sibling == element)
                {
                    return i;
                }

                i++;
            }

            throw new InvalidOperationException
                ("element has been removed from its parent.");
        }
    }
}
