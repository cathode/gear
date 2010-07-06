using System;
using System.Collections.Generic;

using System.Text;

namespace Gear.Assets
{
    /// <summary>
    /// Represents a binary difference between two streams or byte arrays.
    /// </summary>
    public class Delta
    {
        #region Fields
        private readonly List<DeltaInstruction> instructions;
        #endregion
        #region Constructors
        private Delta()
        {
            this.instructions = new List<DeltaInstruction>();
        }
        #endregion
        #region Methods
        /// <summary>
        /// Calculates the binary delta between two byte arrays.
        /// </summary>
        /// <param name="original"></param>
        /// <param name="current"></param>
        /// <returns></returns>
        public static Delta Calculate(byte[] original, byte[] current)
        {
            throw new NotImplementedException();
            /*
            original = original ?? new byte[0];
            current = current ?? new byte[0];

            if (original.Length == 0 && current.Length == 0)
                return null; // No difference between two empty byte arrays.

            Delta d = new Delta();

            if (original.Length == 0)

            return d;*/
        }

        public byte[] Apply(byte[] original)
        {
            throw new NotImplementedException();
        }

        public byte[] ApplyReverse(byte[] current)
        {
            byte[] result = new byte[0];

            this.ApplyReverse(current, ref result);

            return result;
        }

        private void ApplyReverse(byte[] current, ref byte[] result)
        {
            throw new NotImplementedException();
        }

        public void Apply(byte[] original, ref byte[] result)
        {
            throw new NotImplementedException();
        }
        #endregion


    }
}
