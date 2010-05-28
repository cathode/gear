using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gear.Assets
{
    public struct DeltaInstruction
    {
        private int index;
        private byte[] newValues;
        private DeltaOperation operation;

        public int Index
        {
            get
            {
                return this.index;
            }
            set
            {
                this.index = value;
            }
        }
        public byte[] NewValues
        {
            get
            {
                return this.newValues;
            }
            set
            {
                this.newValues = value;
            }
        }
        public DeltaOperation Operation
        {
            get
            {
                return this.operation;
            }
            set
            {
                this.operation = value;
            }
        }
    }
}
