using System;
using System.Collections.Generic;

using System.Text;
using System.IO;

namespace Gear.Assets
{
    /// <summary>
    /// Represents a sequence of bytes that 
    /// </summary>
    public sealed class SubStream : Stream
    {
        #region Constructors
        public SubStream(Stream outerStream, long startIndex, long length)
        {
            if (!outerStream.CanSeek)
                throw new ArgumentException("outerStream must support seeking", "outerStream");

            this.outerStream = outerStream;
            this.startIndex = startIndex;
            this.length = length;
        }
        #endregion
        #region Fields
        private Stream outerStream;
        private long startIndex;
        private long length;
        private long position;
        #endregion
        #region Methods

        /// <summary>
        /// Clears all buffers for this stream and causes any buffered data to be written to the underlying device.
        /// </summary>
        public override void Flush()
        {
            this.outerStream.Flush();
        }
        /// <summary>
        /// Reads a sequence of bytes from the current stream and advances the position within the stream by the number of bytes read.
        /// </summary>
        /// <param name="buffer">An array of bytes. When this method returns, the buffer contains the specified byte array with the values
        /// between offset and (offset + count - 1) replaced by the bytes read from the current source.</param>
        /// <param name="offset">The zero-based byte offset in buffer at which to begin storing the data read from the current stream.</param>
        /// <param name="count">The maximum number of bytes to be read from the current stream.</param>
        /// <returns>The total number of bytes read into the buffer. This can be less than the number of bytes requested if that many bytes
        /// are not currently available, or zero (0) if the end of the stream has been reached.</returns>
        /// <exception cref="System.NotSupportedException">The stream does not support reading.</exception>
        public override int Read(byte[] buffer, int offset, int count)
        {
            if (!this.CanRead)
                throw new NotSupportedException();

            long pos = this.outerStream.Position;
            this.outerStream.Seek(this.startIndex + this.position, SeekOrigin.Begin);
            int read = this.outerStream.Read(buffer, offset, count);
            this.position += read;
            this.outerStream.Position = pos;

            return read;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            if (origin == SeekOrigin.Begin)
                this.position = offset;
            else if (origin == SeekOrigin.Current)
                this.position += offset;
            else if (origin == SeekOrigin.End)
                this.position = this.Length - offset;

            return this.position;
        }

        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            if (!this.CanWrite)
                throw new NotSupportedException();
        }
        #endregion
        #region Properties
        public override bool CanRead
        {
            get
            {
                return this.outerStream.CanRead;
            }
        }

        public override bool CanSeek
        {
            get
            {
                return true;
            }
        }

        public override bool CanWrite
        {
            get
            {
                return this.outerStream.CanWrite;
            }
        }
        public override long Length
        {
            get
            {
                return this.length;
            }
        }

        public override long Position
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
    }
}
