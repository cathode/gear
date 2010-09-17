/******************************************************************************
 * Gear: A Steampunk Action-RPG - http://trac.gearedstudios.com/gear/         *
 * Copyright © 2009-2010 Will 'cathode' Shelley. All Rights Reserved.         *
 * This software is released under the terms and conditions of the Microsoft  *
 * Reference Source License (MS-RSL). See the 'license.txt' file for details. *
 *****************************************************************************/
using System;

namespace Gear
{
    /// <summary>
    /// Enumerates endianness modes supported by the <see cref="DataBuffer"/> class.
    /// </summary>
    public enum DataBufferMode
    {
        /// <summary>
        /// Indicates the data buffer will use the system's native endianness.
        /// </summary>
        System = 0x0,

        /// <summary>
        /// Indicates the data buffer will read/write values as little-endian.
        /// </summary>
        LittleEndian,

        /// <summary>
        /// Indicates the data buffer will read/write values as big-endian.
        /// </summary>
        BigEndian,

        /// <summary>
        /// Indicates the data buffer will read/write values as network byte order (big-endian).
        /// </summary>
        NetworkByteOrder = BigEndian,

        /// <summary>
        /// Indicates the data buffer will read/write values as host byte order (system endianness).
        /// </summary>
        HostByteOrder = System,
    }

    /// <summary>
    /// Represents a buffer that primitive types can be decoded from/encoded to.
    /// </summary>
    public sealed class DataBuffer
    {
        #region Fields
        private byte[] contents;
        private int position;
        private DataBufferMode mode;
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new current of the <see cref="DataBuffer"/> class.
        /// </summary>
        /// <param name="capacity">The fixed capacity of the buffer.</param>
        public DataBuffer(int capacity)
        {
            this.contents = new byte[capacity];
            this.Mode = DataBufferMode.System;
        }

        public DataBuffer(int capacity, DataBufferMode mode)
        {
            this.contents = new byte[capacity];
            this.Mode = mode;
        }
        /// <summary>
        /// Initializes a new current of the <see cref="DataBuffer"/> class.
        /// </summary>
        /// <param name="contents"></param>
        public DataBuffer(byte[] contents)
        {
            this.contents = contents;
            this.Mode = DataBufferMode.System;
        }

        public DataBuffer(byte[] contents, DataBufferMode mode)
        {
            this.contents = contents;
            this.Mode = mode;
        }
        #endregion
        #region Properties
        /// <summary>
        /// Gets the underlying byte array of the current data buffer.
        /// </summary>
        public byte[] Contents
        {
            get
            {
                return this.contents;
            }
            set
            {
                this.contents = value;
            }
        }

        public DataBufferMode Mode
        {
            get
            {
                return this.mode;
            }
            set
            {
                if (value == DataBufferMode.System)
                    this.mode = (BitConverter.IsLittleEndian) ? DataBufferMode.LittleEndian : DataBufferMode.BigEndian;
                else
                    this.mode = value;
            }
        }

        /// <summary>
        /// Gets or sets the position within the buffer where the next decode or encode will start.
        /// </summary>
        public int Position
        {
            get
            {
                return this.position;
            }
            set
            {
                this.position = value;
            }
        }
        #endregion
        #region Methods
        public byte ReadByte()
        {
            byte result = this.contents[this.position];
            this.position += 1;
            return result;
        }
        public byte[] ReadBytes(int count)
        {
            throw new NotImplementedException();
        }
        public int readBytes(byte[] buffer, int startIndex, int count)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Decodes the next two bytes from the buffer as a 16-bit signed integer value,
        /// and advances the current position by two.
        /// </summary>
        /// <returns>The decoded 16-bit signed integer value.</returns>
        public short ReadInt16()
        {
            // No bitshift operator for short, have to use int and cast when returning.
            int result;

            if (this.Mode == DataBufferMode.BigEndian)
                result = contents[position] << 8
                       | contents[position + 1];
            else
                result = contents[position]
                       | contents[position + 1] << 8;

            position += 2;
            return (short)result;
        }

        /// <summary>
        /// Decodes the next four bytes from the buffer as a 32-bit signed integer value,
        /// and advances the current position by four.
        /// </summary>
        /// <returns>The decoded 32-bit signed integer value.</returns>
        public int ReadInt32()
        {
            int result;

            if (this.Mode == DataBufferMode.BigEndian)
                result = contents[position + 0] << 24
                       | contents[position + 1] << 16
                       | contents[position + 2] << 8
                       | contents[position + 3];

            else
                result = contents[position + 0]
                       | contents[position + 1] << 8
                       | contents[position + 2] << 16
                       | contents[position + 3] << 24;

            position += 4;
            return result;
        }

        /// <summary>
        /// Decodes the next eight bytes from the buffer as a 64-bit signed integer value,
        /// and advances the current position by eight.
        /// </summary>
        /// <returns>The decoded 64-bit signed integer value.</returns>
        public long ReadInt64()
        {
            long result;

            if (this.Mode == DataBufferMode.BigEndian)
                result = contents[position + 0] << 56
                       | contents[position + 1] << 48
                       | contents[position + 2] << 40
                       | contents[position + 3] << 32
                       | contents[position + 4] << 24
                       | contents[position + 5] << 16
                       | contents[position + 6] << 8
                       | contents[position + 7];
            else
                result = contents[position + 0]
                       | contents[position + 1] << 8
                       | contents[position + 2] << 16
                       | contents[position + 3] << 24
                       | contents[position + 4] << 32
                       | contents[position + 5] << 40
                       | contents[position + 6] << 48
                       | contents[position + 7] << 56;

            position += 8;
            return result;
        }

        /// <summary>
        /// Decodes the next two bytes from the buffer as a 16-bit unsigned integer value,
        /// and advances the current position by two.
        /// </summary>
        /// <returns>The decoded 16-bit unsigned integer value.</returns>
        public ushort ReadUInt16()
        {
            // No bitshift operators for ushort, have to use uint and cast when returning.
            uint result;

            if (this.Mode == DataBufferMode.BigEndian)
                result = (uint)contents[position + 0] << 8
                       | (uint)contents[position + 1];
            else
                result = (uint)contents[position + 0]
                       | (uint)contents[position + 1] << 8;

            position += 2;
            return (ushort)result;
        }

        /// <summary>
        /// Decodes the next four bytes from the buffer as a 32-bit unsigned integer value,
        /// and advances the current position by four.
        /// </summary>
        /// <returns>The decoded 32-bit unsigned integer value.</returns>
        public uint ReadUInt32()
        {
            uint result;

            if (this.Mode == DataBufferMode.BigEndian)
                result = (uint)contents[position + 0] << 24
                       | (uint)contents[position + 1] << 16
                       | (uint)contents[position + 2] << 8
                       | (uint)contents[position + 3];

            else
                result = (uint)contents[position + 0]
                       | (uint)contents[position + 1] << 8
                       | (uint)contents[position + 2] << 16
                       | (uint)contents[position + 3] << 24;

            position += 4;
            return result;
        }

        /// <summary>
        /// Decodes the next eight bytes from the buffer as a 64-bit signed integer value,
        /// and advances the current position by eight.
        /// </summary>
        /// <returns>The decoded 64-bit signed integer value.</returns>
        public ulong ReadUInt64()
        {
            ulong result;

            if (this.Mode == DataBufferMode.BigEndian)
                result = (ulong)contents[position + 0] << 56
                       | (ulong)contents[position + 1] << 48
                       | (ulong)contents[position + 2] << 40
                       | (ulong)contents[position + 3] << 32
                       | (ulong)contents[position + 4] << 24
                       | (ulong)contents[position + 5] << 16
                       | (ulong)contents[position + 6] << 8
                       | (ulong)contents[position + 7];

            else
                result = (ulong)contents[position + 0]
                       | (ulong)contents[position + 1] << 8
                       | (ulong)contents[position + 2] << 16
                       | (ulong)contents[position + 3] << 24
                       | (ulong)contents[position + 4] << 32
                       | (ulong)contents[position + 5] << 40
                       | (ulong)contents[position + 6] << 48
                       | (ulong)contents[position + 7] << 56;

            position += 8;
            return result;
        }

        public void WriteByte(byte value)
        {
            this.contents[this.position] = value;
            this.position += 1;
        }
        /// <summary>
        /// Writes the specified 16-bit signed integer to the buffer and advances
        /// the current position by two.
        /// </summary>
        /// <param name="value">A 16-bit signed integer value to write to the buffer.</param>
        public void WriteInt16(short value)
        {
            if (this.Mode == DataBufferMode.BigEndian)
            {
                contents[position + 0] = (byte)(value >> 8);
                contents[position + 1] = (byte)value;
            }
            else
            {
                contents[position + 0] = (byte)value;
                contents[position + 1] = (byte)(value >> 8);
            }

            position += 2;
        }

        /// <summary>
        /// Writes the specified 32-bit signed integer to the buffer and advances
        /// the current position by four.
        /// </summary>
        /// <param name="value">A 32-bit signed integer value to write to the buffer.</param>
        public void WriteInt32(int value)
        {
            if (this.Mode == DataBufferMode.BigEndian)
            {
                contents[position + 0] = (byte)(value >> 24);
                contents[position + 1] = (byte)(value >> 16);
                contents[position + 2] = (byte)(value >> 8);
                contents[position + 3] = (byte)value;
            }
            else
            {
                contents[position + 0] = (byte)value;
                contents[position + 1] = (byte)(value >> 8);
                contents[position + 2] = (byte)(value >> 16);
                contents[position + 3] = (byte)(value >> 24);
            }

            position += 4;
        }

        /// <summary>
        /// Writes the specified 64-bit signed integer to the buffer and advances
        /// the current position by eight.
        /// </summary>
        /// <param name="value">A 64-bit signed integer value to write to the buffer.</param>
        public void WriteInt64(long value)
        {
            if (this.Mode == DataBufferMode.BigEndian)
            {
                contents[position + 0] = (byte)(value >> 56);
                contents[position + 1] = (byte)(value >> 48);
                contents[position + 2] = (byte)(value >> 40);
                contents[position + 3] = (byte)(value >> 32);
                contents[position + 4] = (byte)(value >> 24);
                contents[position + 5] = (byte)(value >> 16);
                contents[position + 6] = (byte)(value >> 8);
                contents[position + 7] = (byte)value;
            }
            else
            {
                contents[position + 0] = (byte)value;
                contents[position + 1] = (byte)(value >> 8);
                contents[position + 2] = (byte)(value >> 16);
                contents[position + 3] = (byte)(value >> 24);
                contents[position + 4] = (byte)(value >> 32);
                contents[position + 5] = (byte)(value >> 40);
                contents[position + 6] = (byte)(value >> 48);
                contents[position + 7] = (byte)(value >> 56);
            }

            position += 8;
        }

        /// <summary>
        /// Writes the specified 16-bit unsigned integer to the buffer and advances
        /// the current position by two.
        /// </summary>
        /// <param name="value">A 16-bit unsigned integer value to write to the buffer.</param>
        public void WriteUInt16(ushort value)
        {
            if (this.Mode == DataBufferMode.BigEndian)
            {
                contents[position + 0] = (byte)(value >> 8);
                contents[position + 1] = (byte)value;
            }
            else
            {
                contents[position + 0] = (byte)value;
                contents[position + 1] = (byte)(value >> 8);
            }

            position += 2;
        }

        /// <summary>
        /// Writes the specified 32-bit unsigned integer to the buffer and advances
        /// the current position by two.
        /// </summary>
        /// <param name="value">A 32-bit unsigned integer value to write to the buffer.</param>
        public void WriteUInt32(uint value)
        {
            if (this.Mode == DataBufferMode.BigEndian)
            {
                contents[position + 0] = (byte)(value >> 24);
                contents[position + 1] = (byte)(value >> 16);
                contents[position + 2] = (byte)(value >> 8);
                contents[position + 3] = (byte)value;
            }
            else
            {
                contents[position + 0] = (byte)value;
                contents[position + 1] = (byte)(value >> 8);
                contents[position + 2] = (byte)(value >> 16);
                contents[position + 3] = (byte)(value >> 24);
            }

            position += 4;
        }

        /// <summary>
        /// Writes the specified 64-bit unsigned integer to the buffer and advances
        /// the current position by two.
        /// </summary>
        /// <param name="value">A 64-bit unsigned integer value to write to the buffer.</param>
        public void WriteUInt64(ulong value)
        {
            if (this.Mode == DataBufferMode.BigEndian)
            {
                contents[position + 0] = (byte)(value >> 56);
                contents[position + 1] = (byte)(value >> 48);
                contents[position + 2] = (byte)(value >> 40);
                contents[position + 3] = (byte)(value >> 32);
                contents[position + 4] = (byte)(value >> 24);
                contents[position + 5] = (byte)(value >> 16);
                contents[position + 6] = (byte)(value >> 8);
                contents[position + 7] = (byte)value;
            }
            else
            {
                contents[position + 0] = (byte)value;
                contents[position + 1] = (byte)(value >> 8);
                contents[position + 2] = (byte)(value >> 16);
                contents[position + 3] = (byte)(value >> 24);
                contents[position + 4] = (byte)(value >> 32);
                contents[position + 5] = (byte)(value >> 40);
                contents[position + 6] = (byte)(value >> 48);
                contents[position + 7] = (byte)(value >> 56);
            }

            position += 8;
        }

        public int WriteStringAscii(string value)
        {
            throw new NotImplementedException();
        }

        public int WriteStringUtf8(string value)
        {
            throw new NotImplementedException();
        }

        public int WriteStringUtf16(string value)
        {
            throw new NotImplementedException();
        }
        public int WriteBytes(byte[] value)
        {
            return this.WriteBytes(value, 0, value.Length);
        }
        public int WriteBytes(byte[] value, int startIndex, int count)
        {
            int n;
            for (n = 0; n < count; n++)
                contents[position + n] = value[startIndex + n];

            position += n;
            return n;
        }
        #endregion
    }
}
