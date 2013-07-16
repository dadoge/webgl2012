////////////////////////////////////////////////
// DESCRIPTION:
//    BinaryReader class
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
using System.Text;
using Microsoft.SPOT;
using FusionWare.SPOT.Hardware;

namespace FusionWare.SPOT.IO
{
    /// <summary>Class to read binary data from a stream</summary>
    public class BinaryReader : DisposableObject
    {
        private byte[] Buffer;
        private Encoding Encoding;
        private Stream Stream;
        private const int MaxCharBytesSize = 0x80;
        int BytesPerChar;

        /// <summary>
        /// Exposes access to the underlying stream of the BinaryReader.
        /// </summary>
        public virtual Stream BaseStream
        {
            get { return this.Stream; }
        }

        /// <summary>Creates a new <see cref="BinaryReader"/> that uses the specified Stream for input</summary>
        /// <param name="input">Stream to read the data from</param>
        /// <remarks>Using this constructor the default UTF8 Encoding is used</remarks>
        public BinaryReader(Stream input) 
            : this(input, new UTF8Encoding())
        {
        }

        /// <summary>Creates a new <see cref="BinaryReader"/> that uses the specified Stream for input and specified encoding for strings</summary>
        /// <param name="Input">Stream to read the data from</param>
        /// <param name="EncodingMethod">Encoding to use when reading string data in binary form</param>
        public BinaryReader(Stream Input, Encoding EncodingMethod)
        {
            if (Input == null)
                throw new ArgumentNullException("input");

            if (EncodingMethod == null)
                throw new ArgumentNullException("EncodingMethod");

            if (!Input.CanRead)
                throw new ArgumentException("Stream Must be readable", "Input");

            this.Stream = Input;
            this.Encoding = EncodingMethod;
            
            // for now, always 1 since MF only has UTF8encoding
            this.BytesPerChar = 1;
            this.Buffer = new byte[ MaxCharBytesSize ];
        }

        /// <summary>Close the reader and the contained BaseStream</summary>
        public virtual void Close()
        {
            this.Dispose(true);
        }

        /// <summary>Handles cleanup on Dispose</summary>
        /// <param name="disposing">true if called from dispose; false if called from GC finalization</param>
        protected override void  Dispose(bool disposing)
        {
            if( disposing )
            {
                Stream stream = this.Stream;
                this.Stream = null;
                if( stream != null )
                    stream.Close();
            }
            this.Stream = null;
            this.Buffer = null;
        }

        /// <summary>Fills the internal buffer with the specified number of bytes read from the stream.</summary>
        /// <param name="numBytes">The number of bytes to be read. </param>
        protected virtual void FetchBytes(int numBytes)
        {
            int offset = 0;
            int num2 = 0;
            if (this.Stream == null)
                throw new IOException("File not open");

            if (numBytes == 1)
            {
                num2 = this.Stream.ReadByte();
                if (num2 == -1)
                    throw new IOException("End of file");

                this.Buffer[0] = (byte)num2;
            }
            else
            {
                do
                {
                    num2 = this.Stream.Read(this.Buffer, offset, numBytes - offset);
                    if (num2 == 0)
                        throw new IOException("End of file");

                    offset += num2;
                }while (offset < numBytes);
            }
        }

        private int InternalReadChars(char[] buffer, int index, int count)
        {
            byte[] byteBuf = new byte[count * this.BytesPerChar];
            this.Stream.Read(byteBuf, 0, count);
            char[] chars = this.Encoding.GetChars(byteBuf);
            Array.Copy(chars, 0, buffer, index, count);
            return chars.Length;
        }

        private int InternalReadOneChar()
        {
            byte[] byteBuf = new byte[ BytesPerChar ];
            int actualCount = this.Stream.Read( byteBuf, 0, BytesPerChar );
            if( actualCount < BytesPerChar )
                return -1;
            char[] chars = this.Encoding.GetChars(byteBuf);
            return chars[0];
        }

        private static bool TryParseFloat(string s, out float result)
        {
            double d;
            bool ret = TryParseDouble(s, out d);
            result = (float)d;
            return ret;
        }

        private static bool TryParseDouble(string s, out double result)
        {
            bool success = true;
            double decimalDiv = 0;
            
            result = 0;
            try
            {
                for( int i = 0; i < s.Length; i++ )
                {
                    char digit = s[i];
                    if(('.' == digit) && (0 == decimalDiv))
                        decimalDiv = 1;
                    else
                    {
                        if((digit < '0') || (digit > '9'))
                            success = false;
                        else
                        {
                            result = result * 10;
                            decimalDiv = decimalDiv * 10;
                            result += (int)(digit - '0');
                        }
                    }
                }

                if (decimalDiv != 0)
                    result = (double) result / decimalDiv;
            }
            catch
            {
                success = false;
            }
            return success;
        }

        /// <summary>Returns the next available character and does not advance the byte or character position.</summary>
        /// <returns>The next available character, or -1 if no more characters are available or the stream does not support seeking.</returns>
        public virtual int PeekChar()
        {
            if (this.Stream == null)
                throw new IOException("File not open");

            if (!this.Stream.CanSeek)
                return -1;

            long position = this.Stream.Position;
            int num2 = this.Read();
            this.Stream.Position = position;
            return num2;
        }

        /// <summary>Reads characters from the underlying stream and advances the current position of the stream in accordance with the Encoding used and the specific character being read from the stream.</summary>
        /// <returns>The next character from the input stream, or -1 if no characters are currently available.</returns>
        public virtual int Read()
        {
            if (this.Stream == null)
                throw new IOException("File not open");

            return this.InternalReadOneChar();
        }

        /// <summary>Reads <paramref name="count"/> bytes from the stream with <paramref name="index"/> as the starting point in the byte array.</summary>
        /// <param name="buffer">The buffer to read data into.</param>
        /// <param name="index">The starting point in the buffer at which to begin reading into the buffer.</param>
        /// <param name="count">The number of bytes to read.</param>
        /// <returns>The number of bytes read into buffer. This might be less than the number of bytes requested if that many bytes are not available, or it might be zero if the end of the stream is reached.</returns>
        public virtual int Read(byte[] buffer, int index, int count)
        {
            if (buffer == null)
                throw new ArgumentNullException("buffer");

            if (index < 0)
                throw new ArgumentOutOfRangeException("index");

            if (count < 0)
                throw new ArgumentOutOfRangeException("count");

            if ((buffer.Length - index) < count)
                throw new ArgumentException("Argument_InvalidOffLen");

            if (this.Stream == null)
                throw new IOException("File not open");

            return this.Stream.Read(buffer, index, count);
        }

        /// <summary>Reads <paramref name="count"/> characters from the stream with <paramref name="index"/> as the starting point in the character array.</summary>
        /// <param name="buffer">The buffer to read data into.</param>
        /// <param name="index">The starting point in the buffer at which to begin reading into the buffer.</param>
        /// <param name="count">The number of characters to read.</param>
        /// <returns>The number of characters read into buffer. This might be less than the number of characters requested if that many characters are not available, or it might be zero if the end of the stream is reached.</returns>
        public virtual int Read(char[] buffer, int index, int count)
        {
            if (buffer == null)
                throw new ArgumentNullException("buffer");

            if (index < 0)
                throw new ArgumentOutOfRangeException("index");

            if (count < 0)
                throw new ArgumentOutOfRangeException("count");

            if ((buffer.Length - index) < count)
                throw new ArgumentException("Argument_InvalidOffLen");

            if (this.Stream == null)
                throw new IOException("File not open");

            return this.InternalReadChars(buffer, index, count);
        }

        /// <summary>Reads a 7bit encoded integer value</summary>
        /// <remarks>
        /// This is useful for length prefixed values to reduce the
        /// amount of data used for the length. In particular it is
        /// used for strings but can be used for other purposes.
        /// </remarks>
        /// <returns>integer value from the 7 bit encoded data in the stream</returns>
        internal int Read7BitEncodedInt()
        {
            byte num3;
            int num = 0;
            int num2 = 0;
            do
            {
                if (num2 == 0x23)
                    throw new IOException("Format_Bad7BitInt32");

                num3 = this.ReadByte();
                num |= (num3 & 0x7f) << num2;
                num2 += 7;
            }while ((num3 & MaxCharBytesSize) != 0);
            return num;
        }

        /// <summary>Reads a Boolean value from the current stream and advances the current position of the stream by one byte.</summary>
        /// <returns>true if the byte is nonzero; otherwise, false.</returns>
        public virtual bool ReadBoolean()
        {
            this.FetchBytes(1);
            return (this.Buffer[0] != 0);
        }

        /// <summary>Reads the next byte from the current stream and advances the current position of the stream by one byte.</summary>
        /// <returns>The next byte read from the current stream.</returns>
        public virtual byte ReadByte()
        {
            if (this.Stream == null)
                throw new IOException("File not open");

            int num = this.Stream.ReadByte();
            if (num == -1)
                throw new IOException("End of file");

            return (byte)num;
        }

        /// <summary>Reads <paramref name="count"/> bytes from the current stream into a byte array and advances the current position by <paramref name="count"/> bytes.</summary>
        /// <param name="count">The number of bytes to read.</param>
        /// <returns>A byte array containing data read from the underlying stream. This might be less than the number of bytes requested if the end of the stream is reached.</returns>
        public virtual byte[] ReadBytes(int count)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException("count");

            if (this.Stream == null)
                throw new IOException("File not open");

            byte[] buffer = new byte[count];
            int offset = 0;
            do
            {
                int num2 = this.Stream.Read(buffer, offset, count);
                if (num2 == 0)
                    break;

                offset += num2;
                count -= num2;
            }while (count > 0);

            if (offset != buffer.Length)
            {
                byte[] dst = new byte[offset];
                Array.Copy(buffer, dst, offset);
                buffer = dst;
            }
            return buffer;
        }

        /// <summary>Reads the next character from the current stream and advances the current position of the stream in accordance with the Encoding used and the specific character being read from the stream.</summary>
        /// <returns>A character read from the current stream.</returns>
        public virtual char ReadChar()
        {
            int num = this.Read();
            if (num == -1)
                throw new IOException("End of file");

            return (char)num;
        }
        /// <summary>Reads <paramref name="count"/> characters from the current stream, returns the data in a character array, and advances the current position in accordance with the Encoding used and the specific character being read from the stream.</summary>
        /// <param name="count">The number of characters to read.</param>
        /// <returns>A character array containing data read from the underlying stream. This might be less than the number of characters requested if the end of the stream is reached.</returns>
        public virtual char[] ReadChars(int count)
        {
            if (this.Stream == null)
                throw new IOException("File not open");

            if (count < 0)
                throw new ArgumentOutOfRangeException("count");

            char[] buffer = new char[count];
            int num = this.InternalReadChars(buffer, 0, count);
            if (num != count)
            {
                char[] dst = new char[num];
                Array.Copy(buffer, dst, num);
                buffer = dst;
            }
            return buffer;
        }
/* NOTIMPL!
        public virtual decimal ReadDecimal()
        {
        }

        public virtual unsafe double ReadDouble()
        {
        }
*/
        /// <summary>
        /// Reads a 2-byte signed integer from the current stream and advances the current position of the stream by two bytes. 
        /// </summary>
        /// <returns>A 2-byte signed integer read from the current stream.</returns>
        public virtual short ReadInt16()
        {
            return ReadInt16(false);
        }

        /// <summary>
        /// Reads a 2-byte signed integer from the current stream and advances the current position of the stream by two bytes. 
        /// </summary>
        /// <param name="BigEndian">Treat stream as Big Endian array (true) or Little Endian array (false)</param>
        /// <returns>A 2-byte signed integer read from the current stream.</returns>
        public virtual short ReadInt16(bool BigEndian)
        {
            this.FetchBytes(2);
            return BigEndian ?
                  (short)(this.Buffer[1] | (this.Buffer[0] << 8))
                : (short)(this.Buffer[0] | (this.Buffer[1] << 8));
        }

        /// <summary>
        /// Reads a 4-byte signed integer from the current stream and advances the current position of the stream by four bytes. 
        /// </summary>
        /// <returns>A 4-byte signed integer read from the current stream. </returns>
        public virtual int ReadInt32()
        {
            return ReadInt32(false);
        }

        /// <summary>
        /// Reads a 4-byte signed integer from the current stream and advances the current position of the stream by four bytes. 
        /// </summary>
        /// <param name="BigEndian">Treat stream as Big Endian array (true) or Little Endian array (false)</param>
        /// <returns>A 4-byte signed integer read from the current stream. </returns>
        public virtual int ReadInt32(bool BigEndian)
        {
            this.FetchBytes(4);
            return BigEndian ?
                  (((this.Buffer[3] | (this.Buffer[2] << 8)) | (this.Buffer[1] << 0x10)) | (this.Buffer[0] << 0x18))
                : (((this.Buffer[0] | (this.Buffer[1] << 8)) | (this.Buffer[2] << 0x10)) | (this.Buffer[3] << 0x18));
        }
        
        /// <summary>
        /// Reads an 8-byte signed integer from the current stream and advances the current position of the stream by eight bytes. 
        /// </summary>
        /// <returns>An 8-byte signed integer read from the current stream. </returns>
        public virtual long ReadInt64()
        {
            return ReadInt64(false);
        }

        /// <summary>
        /// Reads an 8-byte signed integer from the current stream and advances the current position of the stream by eight bytes. 
        /// </summary>
        /// <param name="BigEndian">Treat stream as Big Endian array (true) or Little Endian array (false)</param>
        /// <returns>An 8-byte signed integer read from the current stream. </returns>
        public virtual long ReadInt64(bool BigEndian)
        {
            this.FetchBytes(8);
            uint num = BigEndian ?
                          (uint)(((this.Buffer[3] | (this.Buffer[2] << 8)) | (this.Buffer[1] << 0x10)) | (this.Buffer[0] << 0x18))
                        : (uint)(((this.Buffer[0] | (this.Buffer[1] << 8)) | (this.Buffer[2] << 0x10)) | (this.Buffer[3] << 0x18));
            uint num2 = BigEndian ?
                          (uint)(((this.Buffer[7] | (this.Buffer[6] << 8)) | (this.Buffer[5] << 0x10)) | (this.Buffer[4] << 0x18))
                        : (uint)(((this.Buffer[4] | (this.Buffer[5] << 8)) | (this.Buffer[6] << 0x10)) | (this.Buffer[7] << 0x18));
            return BigEndian ?
                      (((long)num << 0x20) | num2)
                    : (((long)num2 << 0x20) | num);
        }

        /// <summary>
        /// Reads a signed byte from this stream and advances the current position of the stream by one byte. 
        /// </summary>
        /// <returns>A signed byte read from the current stream. </returns>
        public virtual sbyte ReadSByte()
        {
            this.FetchBytes(1);
            return (sbyte)this.Buffer[0];
        }
/* NOTIMPL!
        public virtual unsafe float ReadSingle()
        {
        }
*/
        /// <summary>
        /// Reads a string from the current stream. The string is prefixed with the length, encoded as
        /// an integer seven bits at a time. 
        /// </summary>
        /// <returns>The string being read.</returns>
        public virtual string ReadString()
        {
            if (this.Stream == null)
                throw new IOException("File not open");

            int capacity = this.Read7BitEncodedInt();
            if (capacity < 0)
                throw new IOException("IO.IO_InvalidStringLen_Len");

            if (capacity == 0)
                return string.Empty;

            int byteCount = capacity * this.BytesPerChar;
            byte[] rawBytes = new byte[byteCount];
            this.Stream.Read(rawBytes, 0, byteCount);
            return new string(this.Encoding.GetChars(rawBytes));
        }

        /// <summary>
        /// Reads a 2-byte unsigned integer from the current stream and advances the current position of the stream by two bytes. 
        /// </summary>
        /// <returns>A 2-byte unsigned integer read from the current stream.</returns>
        public virtual ushort ReadUInt16()
        {
            return ReadUInt16(false);
        }
        /// <summary>
        /// Reads a 2-byte unsigned integer from the current stream and advances
        /// the current position of the stream by two bytes. 
        /// </summary>
        /// <param name="BigEndian">Treat stream as Big Endian array (true) or Little Endian array (false)</param>
        /// <returns>A 2-byte unsigned integer read from the current stream.</returns>
        public virtual ushort ReadUInt16(bool BigEndian)
        {
            this.FetchBytes(2);
            return BigEndian ?
                      (ushort)(this.Buffer[1] | (this.Buffer[0] << 8))
                    : (ushort)(this.Buffer[0] | (this.Buffer[1] << 8));
        }

        /// <summary>
        /// Reads a 4-byte unsigned integer from the current stream and advances the current position of the stream by four bytes. 
        /// </summary>
        /// <returns>A 4-byte unsigned integer read from the current stream. </returns>
        public virtual uint ReadUInt32()
        {
            return ReadUInt32(false);
        }

        /// <summary>
        /// Reads a 4-byte unsigned integer from the current stream and advances the current position of the stream by four bytes. 
        /// </summary>
        /// <param name="BigEndian">Treat stream as Big Endian array (true) or Little Endian array (false)</param>
        /// <returns>A 4-byte unsigned integer read from the current stream. </returns>
        public virtual uint ReadUInt32(bool BigEndian)
        {
            this.FetchBytes(4);
            return BigEndian ?
                      (uint)(((this.Buffer[3] | (this.Buffer[2] << 8)) | (this.Buffer[1] << 0x10)) | (this.Buffer[0] << 0x18))
                    : (uint)(((this.Buffer[0] | (this.Buffer[1] << 8)) | (this.Buffer[2] << 0x10)) | (this.Buffer[3] << 0x18));
        }

        /// <summary>
        /// Reads an 8-byte unsigned integer from the current stream and advances the current position of the stream by eight bytes. 
        /// </summary>
        /// <returns>An 8-byte unsigned integer read from the current stream. </returns>
        public virtual ulong ReadUInt64()
        {
            return ReadUInt64(false);
        }

        /// <summary>
        /// Reads an 8-byte unsigned integer from the current stream and advances the current position of the stream by eight bytes. 
        /// </summary>
        /// <param name="BigEndian">Treat stream as Big Endian array (true) or Little Endian array (false)</param>
        /// <returns>An 8-byte unsigned integer read from the current stream. </returns>
        public virtual ulong ReadUInt64(bool BigEndian)
        {
            this.FetchBytes(8);
            uint num = BigEndian ?
                          (uint)(((this.Buffer[3] | (this.Buffer[2] << 8)) | (this.Buffer[1] << 0x10)) | (this.Buffer[0] << 0x18))
                        : (uint)(((this.Buffer[0] | (this.Buffer[1] << 8)) | (this.Buffer[2] << 0x10)) | (this.Buffer[3] << 0x18));
            uint num2 = BigEndian ?
                          (uint)(((this.Buffer[7] | (this.Buffer[6] << 8)) | (this.Buffer[5] << 0x10)) | (this.Buffer[4] << 0x18))
                        : (uint)(((this.Buffer[4] | (this.Buffer[5] << 8)) | (this.Buffer[6] << 0x10)) | (this.Buffer[7] << 0x18));
            return BigEndian ? 
                      (((ulong)num << 0x20) | num2)
                    : (((ulong)num2 << 0x20) | num);
        }
    }
}

