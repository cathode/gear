/******************************************************************************
 * Gear: A Steampunk Action-RPG - http://trac.gearedstudios.com/gear/         *
 * Copyright © 2009-2010 Will 'cathode' Shelley. All Rights Reserved.         *
 * This software is released under the terms and conditions of the Microsoft  *
 * Reference Source License (MS-RSL). See the 'license.txt' file for details. *
 *****************************************************************************/
using System;

namespace Gear.Net
{
    /// <summary>
    /// Represents a message sent over the network from one endpoint to another.
    /// </summary>
    public abstract class Message
    {
        #region Properties
        protected abstract MessageId Id
        {
            get;
        }
        #endregion
        #region Methods

        /// <summary>
        /// When overridden in a derived class, gets the number of bytes that would be required to serialize
        /// the current <see cref="Message"/> for transport over the network.
        /// </summary>
        /// <returns>The total number of bytes including all headers, footers, etc.</returns>
        public virtual int GetByteCount()
        {
            int count = 2; // Message Id

            var fields = this.GetFieldData();
            for (int i = 0; i < fields.Length; i++)
                count += fields[i].Length + 4;

            return count;
        }

        /// <summary>
        /// Writes the message to the buffer.
        /// </summary>
        /// <param name="buffer">The buffer to write to.</param>
        /// <param name="startIndex">The index in the buffer at which to start writing.</param>
        /// <returns>The number of bytes written.</returns>
        public int WriteTo(byte[] buffer, int startIndex)
        {
            var fields = this.GetFieldData();
            int p = startIndex;
            buffer[p++] = (byte)((ushort)this.Id << 8);
            buffer[p++] = (byte)this.Id;
            
            buffer[p++] = 0; // placeholder for message size
            buffer[p++] = 0;

            for (int i = 0; i < fields.Length; i++)
            {
                var f = fields[i];

                throw new NotImplementedException();
            }
            return -1; // Nothing written.
        }

        public int ReadFrom(byte[] buffer, int startIndex)
        {
            throw new NotImplementedException();
        }

        protected abstract MessageField[] GetFieldData();
        #endregion
    }
}
