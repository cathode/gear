/******************************************************************************
 * Gear: A game of block-based sandbox fun. http://github.com/cathode/gear/   *
 * Copyright © 2009-2013 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the Microsoft  *
 * Reference Source License (MS-RSL). See the 'license.txt' file for details. *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gear.Serialization
{
    /// <summary>
    /// Represents a packet comprised of one or more fields containing data and/or sub-fields.
    /// </summary>
    public class GPacket
    {
        private List<Field> fields;

        public Field this[string field]
        {
            get
            {
                return this.fields.FirstOrDefault(f => f.Name == field);
            }
            set
            {
                throw new NotImplementedException();

                //if (!this.fields.FirstOrDefault(f => f.Name == field).Assign(value))
                //    throw new NotImplementedException();
            }
        }

        public Field this[int field]
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }

    /// <summary>
    /// Represents a schema that describes the layout of a <see cref="GPacket"/>.
    /// </summary>
    public class GMessageSchema
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GMessageSchema"/> class.
        /// </summary>
        public GMessageSchema()
        {
            this.Fields = new List<FieldDescriptor>();
        }
        public List<FieldDescriptor> Fields
        {
            get;
            private set;
        }

        public bool Validate(GPacket message)
        {
            throw new NotImplementedException();

        }
    }
    public class FieldDescriptor
    {
        public string Name
        {
            get;
            set;
        }

        public bool IsRequired
        {
            get;
            set;
        }
        public int MinOccurences
        {
            get;
            set;
        }
        public int MaxOccurances
        {
            get;
            set;
        }

    }
}
