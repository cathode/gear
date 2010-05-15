using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gear.Assets
{
    /// <summary>
    /// Represents a binary difference between two streams or byte arrays.
    /// </summary>
    public class Delta
    {

        public static Delta Calculate(byte[] original, byte[] current)
        {
            throw new NotImplementedException();
        }

        public byte[] Apply(byte[] original)
        {
            throw new NotImplementedException();
        }

        public byte[] ApplyReverse(byte[] current)
        {
            throw new NotImplementedException();
        }

        public void Apply(byte[] p, ref byte[] byRef)
        {
            throw new NotImplementedException();
        }
    }
}
