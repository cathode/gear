﻿/******************************************************************************
 * Gear: An open-world sandbox game for creative people.                      *
 * http://github.com/cathode/gear/                                            *
 * Copyright © 2009-2017 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT        *
 * license. See the included LICENSE file for details.                        *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace Gear.Modeling
{
    public delegate bool ProcedureParameterValidator(ProcedureParameter parameter, object value);

    /// <summary>
    /// Represents a parameter of a procedural mesh.
    /// </summary>
    public class ProcedureParameter
    {
        #region Fields
        public static readonly ProcedureParameterValidator DefaultValidator = delegate
        {
            return true;
        };

        private readonly string name;
        private ProceduralMesh owner;
        private readonly ProcedureParameterValidator validator;
        private object value;
        #endregion
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ProcedureParameter"/> class.
        /// </summary>
        /// <param name="name"></param>
        ///
        public ProcedureParameter(string name)
        {
            Contract.Requires(name != null);

            this.name = name;
            this.validator = ProcedureParameter.DefaultValidator;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProcedureParameter"/> class.
        /// </summary>
        /// <param name="validator"></param>
        public ProcedureParameter(string name, ProceduralMesh owner, ProcedureParameterValidator validator)
        {
            Contract.Requires(name != null);
            Contract.Requires(owner != null);

            this.name = name;
            this.owner = owner;
            this.validator = validator;
        }
        #endregion
        #region Properties

        /// <summary>
        /// Gets or sets the name of the parameter.
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }
        }

        /// <summary>
        /// Gets or sets the value assigned to the parameter.
        /// </summary>
        public object Value
        {
            get
            {
                return this.value;
            }

            set
            {
                if (this.validator(this, value))
                {
                    this.value = value;
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
        }

        public ProceduralMesh Owner
        {
            get
            {
                return this.owner;
            }

            internal set
            {
                Contract.Requires(value != null);

                this.owner = value;
            }
        }

        #endregion
        #region Methods
        [ContractInvariantMethod]
        private void Invariants()
        {
            Contract.Invariant(this.Name != null);
        }
        #endregion
    }
}
