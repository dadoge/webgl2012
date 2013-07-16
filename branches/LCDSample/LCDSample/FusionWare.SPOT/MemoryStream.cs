////////////////////////////////////////////////
// DESCRIPTION:
//    MemoryStream implementation
//
// Legal Notices:
//   Copyright (c) 2008, Telliam Consulting, LLC.
//   All rights reserved.
//
//   Redistribution and use in source and binary forms, with or without modification,
//   are permitted provided that the following conditions are met:
//
//   * Redistributions of source code must retain the above copyright notice, this list
//     of conditions and the following disclaimer.
//   * Redistributions in binary form must reproduce the above copyright notice, this
//     list of conditions and the following disclaimer in the documentation and/or other
//     materials provided with the distribution.
//   * Neither the name of Telliam Consulting nor the names of its contributors may be
//     used to endorse or promote products derived from this software without specific
//     prior written permission.
//
//   THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY
//   EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES
//   OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT
//   SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT,
//   INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED
//   TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR 
//   BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN
//   CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN 
//   ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH
//   DAMAGE. 
//

using System;
using System.IO;

namespace FusionWare.SPOT.IO
{
    /// <summary>Creates a stream whose backing store is a byte array in memory.</summary>
    /// <remarks>
    /// The standard Micro Framework libraries provide the basic abstract Stream class
    /// but provide no concrete implementations. This class provides an implmentation
    /// of a stream on a byte array in memory. Many communications protocols use binary
    /// data and this combined with the BinaryReader and BinaryWriter classes makes things
    /// a lot simpler for handling such protocols.
    /// </remarks>
    [Serializable, Obsolete("Use System.IO.MemoryStream instead")]
    public class MemoryStream : Stream
    {
        /// <summary>Creates a new Memory Stream that is expandable</summary>
        public MemoryStream()
            : this(0)
        {
        }

        /// <summary>Creates a new MemoryStream with the specified capacity</summary>
        /// <param name="Capacity">Capacity for the stream in bytes</param>
        public MemoryStream(int Capacity)
        {
            if(Capacity < 0)
                throw new ArgumentOutOfRangeException("Capacity", "Negative capacity not allowed");

            this.Buffer = new byte[Capacity];
            this._Capacity = Capacity;
            this._Length = 0;
            this.Expandable = true;
            this._CanWrite = true;
            this.AllowGetBuffer = true;
            this.BufferOrigin = 0;
            this.IsDisposed = false;
        }

        /// <summary>Creates new memory stream using the provided Buffer</summary>
        /// <param name="Buffer">byte array to use as the underlying stream storage</param>
        public MemoryStream(byte[] Buffer)
            :this(Buffer, true)
        {
        }

        /// <summary>Creates a new memory stream using the provided buffer for storage</summary>
        /// <param name="Buffer">Buffer to use for storage</param>
        /// <param name="Writable">true if the stream is writeable; false if it should be read-only</param>
        public MemoryStream(byte[] Buffer, bool Writable)
        {
            if(Buffer == null)
                throw new ArgumentNullException("Buffer");

            this._CanWrite = Writable;
            this.Buffer = Buffer;
            this._Length = Buffer.Length;
        }

        /// <summary>Creates a new MemoryStream based on a byte array</summary>
        /// <param name="Buffer">Byte array to use for the </param>
        /// <param name="Index">index in the array to where the stream should start</param>
        /// <param name="Count">Count of bytes in Buffer to use for the stream</param>
        public MemoryStream(byte[] Buffer, int Index, int Count)
            : this(Buffer, Index, Count, true, false)
        {
        }

        /// <summary>Creates a new MemoryStream based on a byte array</summary>
        /// <param name="Buffer">Byte array to use for the </param>
        /// <param name="Index">index in the array to where the stream should start</param>
        /// <param name="Count">Count of bytes in Buffer to use for the stream</param>
        /// <param name="Writable">true if the stream is writeable; false if it should be read-only</param>
        public MemoryStream(byte[] Buffer, int Index, int Count, bool Writable)
            : this(Buffer, Index, Count, Writable, false)
        {
        }

        /// <summary>Creates a new MemoryStream based on a byte array</summary>
        /// <param name="Buffer">Byte array to use for the </param>
        /// <param name="Index">index in the array to where the stream should start</param>
        /// <param name="Count">Count of bytes in Buffer to use for the stream</param>
        /// <param name="Writable">true if the stream is writeable; false if it should be read-only</param>
        /// <param name="AllowGetBuffer">true if GetBuffer is allowed; false if not</param>
        public MemoryStream(byte[] Buffer, int Index, int Count, bool Writable, bool AllowGetBuffer)
        {
            ValidateBufferIndexCount(Buffer, Index, Count);

            this.Buffer = Buffer;
            this.BufferOrigin = Index;
            this._Position = Index;
            this._Length = Index + Count;
            this._Capacity = this._Length;
            this._CanWrite = Writable;
            this.AllowGetBuffer = AllowGetBuffer;
            this.Expandable = false;
            this.IsDisposed = false;
        }

        private static void ValidateBufferIndexCount(byte[] Buffer, int Index, int Count)
        {
            if(Buffer == null)
                throw new ArgumentNullException("buffer");

            if(Index < 0)
                throw new ArgumentOutOfRangeException("index");

            if(Count < 0)
                throw new ArgumentOutOfRangeException("count");

            if((Buffer.Length - Index) < Count)
                throw new ArgumentException("invalid index and count combination for this buffer");
        }

        /// <summary>Standard IDisposable support</summary>
        /// <param name="Disposing">true if this is caused by a call to Dispose() false if from the finalizer</param>
        protected override void Dispose(bool Disposing)
        {
            try
            {
                if(Disposing)
                {
                    this.IsDisposed = true;
                    this._CanWrite = false;
                    this.Expandable = false;
                }
            }
            finally
            {
                base.Dispose(Disposing);
            }
        }

        /// <summary>Flush the stream</summary>
        public override void Flush()
        {
        }

        /// <summary>Gets a byte array for the underlying buffer of the stream</summary>
        ///<returns>The buffer used for the stream</returns>
        /// <exception cref="InvalidOperationException">GetBUffer is not allowed on this instance</exception>
        public virtual byte[] GetBuffer()
        {
            if(!this.AllowGetBuffer)
                throw new InvalidOperationException("GetBuffer not allowed on this instance");

            return this.Buffer;
        }

        /// <summary>Reads data from the stream</summary>
        /// <param name="Buffer">Buffer to read data into</param>
        /// <param name="Offset">Offset into the Buffer to where the read data is placed</param>
        /// <param name="Count">Count of bytes to read</param>
        /// <returns>Number of bytes actually read.</returns>
        public override int Read( byte[] Buffer, int Offset, int Count)
        {
            if(this.IsDisposed)
                throw new InvalidOperationException("Object Disposed");

            ValidateBufferIndexCount(Buffer, Offset, Count);

            int readCount = this._Length - this._Position;
            if(readCount > Count)
                readCount = Count;

            if(readCount <= 0)
                return 0;
            
            Array.Copy(this.Buffer, this._Position, Buffer, Offset, readCount);
            this._Position += readCount;
            return readCount;
        }

        /// <summary>Read a single byte from the stream</summary>
        /// <returns>Byte read from the stream</returns>
        public override int ReadByte()
        {
            ThrowIfDisposed();
            if( this._Position >= this._Length )
                return -1;

            return this.Buffer[this._Position++];
        }

        /// <summary>Moves the internal seek pointer to a new location</summary>
        /// <param name="Offset">Offest to move</param>
        /// <param name="Loc">origin to move the pointer relative to</param>
        /// <returns>The new position</returns>
        public override long Seek(long Offset, SeekOrigin Loc)
        {
            ThrowIfDisposed();

            if(Offset > System.Int32.MaxValue)
                throw new ArgumentOutOfRangeException("Offset");

            switch(Loc)
            {
            case SeekOrigin.Begin:
                if(Offset < 0)
                    throw new IOException("Seek before begin");

                this._Position = this.BufferOrigin + ((int)Offset);
                break;

            case SeekOrigin.Current:
                if((Offset + this._Position) < this.BufferOrigin)
                    throw new IOException("Seek before begin");

                this._Position += (int)Offset;
                break;

            case SeekOrigin.End:
                if((this._Length + Offset) < this.BufferOrigin)
                    throw new IOException("Seek before begin");

                this._Position = this._Length + ((int)Offset);
                break;

            default:
                throw new ArgumentException("Invalid SeekOrigin", "Loc");
            }
            return (long)this._Position;
        }

        /// <summary>Sets the length of the stream</summary>
        /// <param name="Value">New length</param>
        public override void SetLength(long Value)
        {
            ThrowIfDisposed();
            if(Value > System.Int32.MaxValue)
                throw new ArgumentOutOfRangeException("Value");

            if((Value < 0) || (Value > (System.Int32.MaxValue - this.BufferOrigin)))
                throw new ArgumentOutOfRangeException("Value");

            int newLength = this.BufferOrigin + ((int)Value);
            if(!this.ForceCapacity(newLength) && (newLength > this._Length))
            {
                Array.Clear(this.Buffer, this._Length, newLength - this._Length);
            }
            this._Length = newLength;
            if(this._Position > newLength)
            {
                this._Position = newLength;
            }
        }

        private bool ForceCapacity(int Value)
        {
            if(Value < 0)
                throw new IOException("Invalid Stream");

            if(Value <= this._Capacity)
                return false;
            
            // don't grow by tiny increments - give it some breathing room
            int newCapacity = Value;
            if(newCapacity < 0x100)
                newCapacity = 0x100;

            if(newCapacity < (this._Capacity * 2))
                newCapacity = this._Capacity * 2;
            
            // use the property set to allocate more space as needed
            this.Capacity = newCapacity;
            return true;
        }

        /// <summary>Creates a new array with the contents of the stream's internal array</summary>
        /// <returns>new array with the data from the stream</returns>
        public virtual byte[] ToArray()
        {
            return Microsoft.SPOT.Hardware.Utility.ExtractRangeFromArray(this.Buffer, this.BufferOrigin, this._Length - this.BufferOrigin);
        }

        /// <summary>Writes data to the stream</summary>
        /// <param name="Buffer">Buffer containing the data to write</param>
        /// <param name="Offset">Offset within the buffer to start writing data from</param>
        /// <param name="Count">Count of bytes to write</param>
        public override void Write(byte[] Buffer, int Offset, int Count)
        {
            ThrowIfDisposed();
            ThrowIfNotWriteable();
            ValidateBufferIndexCount(Buffer, Offset, Count);

            int newPos = AdjustBufSize(Count);

            Array.Copy(Buffer, Offset, this.Buffer, this._Position, Count);
            this._Position = newPos;
        }

        private int AdjustBufSize(int Count)
        {
            int newPos = this._Position + Count;
            if(newPos < 0)
                throw new IOException("Invalid Capacity");

            if(newPos > this._Length)
            {
                bool extending = this._Position > this._Length;
                if((newPos > this._Capacity) && this.ForceCapacity(newPos))
                    extending = false;

                if(extending)
                    Array.Clear(this.Buffer, this._Length, newPos - this._Length);

                this._Length = newPos;
            }

            return newPos;
        }

        /// <summary>Writes a single byte to the stream</summary>
        /// <param name="Value">byte to write</param>
        public override void WriteByte(byte Value)
        {
            ThrowIfDisposed();
            ThrowIfNotWriteable();

            AdjustBufSize(1);

            this.Buffer[this._Position++] = Value;
        }

        /// <summary>Writes the contents of the internal buffer for this stream to another one</summary>
        /// <param name="OutputStream">stream to write the buffer to</param>
        public virtual void WriteTo(Stream OutputStream)
        {
            ThrowIfDisposed();

            if(OutputStream == null)
                throw new ArgumentNullException("OutputStream");

            OutputStream.Write(this.Buffer, this.BufferOrigin, this._Length - this.BufferOrigin);
        }

        /// <summary>Indicates if the stream is readable</summary>
        /// <value>true if readable; false if not</value>
        public override bool CanRead  { get { return !this.IsDisposed; } }

        /// <summary>Indicates if the stream supports Seek</summary>
        /// <value>true if seek is supported; false if not</value>
        public override bool CanSeek { get { return !this.IsDisposed; } }

        /// <summary>Indicates if the stream is writeable</summary>
        /// <value>true if the stream supports writing; false if not</value>
        public override bool CanWrite { get { return this._CanWrite; } }
        private bool _CanWrite;

        /// <summary>Gets/Sets the overall capacity of the stream</summary>
        public virtual int Capacity
        {
            get
            {
                ThrowIfDisposed();
                return (this._Capacity - this.BufferOrigin);
            }
            set
            {
                ThrowIfDisposed();
                if(value != this._Capacity)
                {
                    if(!this.Expandable)
                        throw new IOException("Stream is not expandable");

                    if(value < this._Length)
                        throw new ArgumentOutOfRangeException("value");

                    if(value > 0)
                    {
                        byte[] newBuffer = new byte[value];
                        
                        // if there is data in the current buffer copy it to the new one
                        if(this._Length > 0)
                            Array.Copy(this.Buffer, 0, newBuffer, 0, this._Length);

                        this.Buffer = newBuffer;
                    }
                    else
                        this.Buffer = null;

                    this._Capacity = value;
                }
            }
        }
        private int _Capacity;

        /// <summary>Current length of the stream</summary>
        public override long Length 
        {
            get
            {
                if(this.IsDisposed)
                    throw new InvalidOperationException("Object Disposed");

                return (long)(this._Length - this.BufferOrigin);
            }
        }
        private int _Length;

        /// <summary>Current position of the stream</summary>
        public override long Position
        {
            get
            {
                ThrowIfDisposed();
                return (long)(this._Position - this.BufferOrigin);
            }
            set
            {
                ThrowIfDisposed();

                if(value < 0)
                    throw new ArgumentOutOfRangeException("value");

                if(value > System.Int32.MaxValue)
                    throw new ArgumentOutOfRangeException("value");

                this._Position = this.BufferOrigin + ((int)value);
            }
        }
        private int _Position;

        private void ThrowIfDisposed()
        {
            if(IsDisposed)
                throw new InvalidOperationException("Object Disposed");
        }

        private void ThrowIfNotWriteable()
        {
            if(!this._CanWrite)
                throw new InvalidOperationException("Stream is not writeable");
        }

        private byte[] Buffer;
        private int BufferOrigin;
        private bool AllowGetBuffer;
        bool Expandable;
        bool IsDisposed;
    }
}
