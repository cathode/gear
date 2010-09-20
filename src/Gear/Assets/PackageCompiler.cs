/******************************************************************************
 * Gear: A Steampunk Action-RPG - http://trac.gearedstudios.com/gear/         *
 * Copyright © 2009-2010 Will 'cathode' Shelley. All Rights Reserved.         *
 * This software is released under the terms and conditions of the Microsoft  *
 * Reference Source License (MS-RSL). See the 'license.txt' file for details. *
 *****************************************************************************/
using System;

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
        #endregion
        #region Methods
        /// <summary>
        /// Runs the compilation.
        /// </summary>
        /// <returns>true if the compilation was successful; otherwise, false.</returns>
        public bool Compile()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
