////////////////////////////////////////////////
// DESCRIPTION:
//    Binary Data writer class
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

namespace FusionWare.SPOT.IO
{

    /// <summary>Provides binary data formatting support</summary>
    /// <remarks>
    /// To the maximum extent possible this class follows the interface 
    /// defined in the desktop version of the class.
    /// </remarks>
    public class BinaryWriter : DisposableObject
    {
        private byte[ ] Buffer;
        private Encoding CharEncoding;
        private const int LargeByteBufferSize = 0x100;

        /// <summary>Provides a Null reader object in support of the NULL Object pattern</summary>
        public static readonly BinaryWriter Null = new BinaryWriter( );

        /// <summary>Stream used for writing data to</summary>
        protected Stream OutStream;

        /// <summary>
        /// Initializes a new instance of the BinaryWriter class that writes to a stream. 
        /// </summary>
        protected BinaryWriter( )
        {
            this.OutStream = new FusionWare.SPOT.IO.NullStream( );
            this.Buffer = new byte[ 0x10 ];
            this.CharEncoding = new UTF8Encoding( );
        }
        /// <summary>
        /// Initializes a new instance of the BinaryWriter class based on the supplied
        /// stream and using UTF-8 as the encoding for strings. 
        /// </summary>
        /// <param name="output">The output stream.</param>
        public BinaryWriter( Stream output )
            : this( output, new UTF8Encoding( ) )
        {
        }

        /// <summary>
        /// Initializes a new instance of the BinaryWriter class based on the supplied
        /// stream and a specific character encoding. 
        /// </summary>
        /// <param name="output">The output stream.</param>
        /// <param name="encoding">The character encoding.</param>
        public BinaryWriter( Stream output, Encoding encoding )
        {
            if( output == null )
                throw new ArgumentNullException( "output" );

            if( encoding == null )
                throw new ArgumentNullException( "encoding" );

            if( !output.CanWrite )
                throw new ArgumentException( "Stream Not Writable" );

            this.OutStream = output;
            this.Buffer = new byte[ 0x10 ];
            this.CharEncoding = encoding;
        }

        /// <summary>
        /// Closes the current <see cref="BinaryWriter"/> and the underlying stream. 
        /// </summary>
        public virtual void Close( )
        {
            this.Dispose( true );
        }

        /// <summary>
        /// Releases the unmanaged resources used by the <see cref="BinaryWriter"/> and optionally releases the managed resources. 
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
        protected override void Dispose( bool disposing )
        {
            if( disposing )
                this.OutStream.Close( );
        }

        /// <summary>
        /// Clears all buffers for the current writer and causes any buffered data to be written to the underlying device. 
        /// </summary>
        public virtual void Flush( )
        {
            this.OutStream.Flush( );
        }

        /// <summary>
        /// Sets the position within the current stream. 
        /// </summary>
        /// <param name="offset">A byte offset relative to origin.</param>
        /// <param name="origin">A field of <see cref="SeekOrigin"/> indicating the reference point from which the new position is to be obtained.</param>
        /// <returns>The position with the current stream.</returns>
        public virtual long Seek( int offset, SeekOrigin origin )
        {
            return this.OutStream.Seek( ( long )offset, origin );
        }

        /// <summary>
        /// Writes a one-byte Boolean value to the current stream, with 0 representing false and 1 representing true. 
        /// </summary>
        /// <param name="value">The Boolean value to write (0 or 1).</param>
        public virtual void Write( bool value )
        {
            this.Buffer[ 0 ] = value ? ( ( byte )1 ) : ( ( byte )0 );
            this.OutStream.Write( this.Buffer, 0, 1 );
        }

        /// <summary>
        /// Writes an unsigned byte to the current stream and advances the stream position by one byte. 
        /// </summary>
        /// <param name="value">The unsigned byte to write.</param>
        public virtual void Write( byte value )
        {
            this.OutStream.WriteByte( value );
        }

        /// <summary>
        /// Writes a byte array to the underlying stream. 
        /// </summary>
        /// <param name="buffer">A byte array containing the data to write.</param>
        public virtual void Write( byte[ ] buffer )
        {
            if( buffer == null )
                throw new ArgumentNullException( "buffer" );

            this.OutStream.Write( buffer, 0, buffer.Length );
        }

        /// <summary>
        /// Writes a Unicode character to the current stream and advances the current position of the stream in accordance with the Encoding used and the specific characters being written to the stream. 
        /// </summary>
        /// <param name="ch">The non-surrogate, Unicode character to write.</param>
        public virtual void Write( char ch )
        {
            byte[ ] buf = this.CharEncoding.GetBytes( new string( ch, 1 ) );
            this.OutStream.Write( buf, 0, buf.Length );
        }

        /// <summary>
        /// Writes a character array to the current stream and advances the current position of the stream in accordance with the Encoding used and the specific characters being written to the stream. 
        /// </summary>
        /// <param name="chars">A character array containing the data to write.</param>
        public virtual void Write( char[ ] chars )
        {
            if( chars == null )
                throw new ArgumentNullException( "chars" );

            byte[ ] buffer = this.CharEncoding.GetBytes( new string( chars, 0, chars.Length ) );
            this.OutStream.Write( buffer, 0, buffer.Length );
        }

        //public virtual void Write(decimal value)
        //{
        //}

        //public virtual unsafe void Write(double value)
        //{
        //}

        /// <summary>
        /// Writes a two-byte signed integer to the current stream and advances the stream position by two bytes. 
        /// </summary>
        /// <param name="value">The two-byte signed integer to write.</param>
        public virtual void Write( short value )
        {
            this.Buffer[ 0 ] = ( byte )value;
            this.Buffer[ 1 ] = ( byte )( value >> 8 );
            this.OutStream.Write( this.Buffer, 0, 2 );
        }

        /// <summary>
        /// Writes a four-byte signed integer to the current stream and advances the stream position by four bytes. 
        /// </summary>
        /// <param name="value">The four-byte signed integer to write.</param>
        public virtual void Write( int value )
        {
            this.Buffer[ 0 ] = ( byte )value;
            this.Buffer[ 1 ] = ( byte )( value >> 8 );
            this.Buffer[ 2 ] = ( byte )( value >> 0x10 );
            this.Buffer[ 3 ] = ( byte )( value >> 0x18 );
            this.OutStream.Write( this.Buffer, 0, 4 );
        }

        /// <summary>
        /// Writes an eight-byte signed integer to the current stream and advances the stream position by eight bytes.
        /// </summary>
        /// <param name="value">The eight-byte signed integer to write.</param>
        public virtual void Write( long value )
        {
            this.Buffer[ 0 ] = ( byte )value;
            this.Buffer[ 1 ] = ( byte )( value >> 8 );
            this.Buffer[ 2 ] = ( byte )( value >> 0x10 );
            this.Buffer[ 3 ] = ( byte )( value >> 0x18 );
            this.Buffer[ 4 ] = ( byte )( value >> 0x20 );
            this.Buffer[ 5 ] = ( byte )( value >> 40 );
            this.Buffer[ 6 ] = ( byte )( value >> 0x30 );
            this.Buffer[ 7 ] = ( byte )( value >> 0x38 );
            this.OutStream.Write( this.Buffer, 0, 8 );
        }

        /// <summary>
        /// Writes a signed byte to the current stream and advances the stream position by one byte. 
        /// </summary>
        /// <param name="value">The signed byte to write.</param>
        public virtual void Write( sbyte value )
        {
            this.OutStream.WriteByte( ( byte )value );
        }

        //public virtual unsafe void Write(float value)
        //{
        //}

        /// <summary>
        /// Writes a length-prefixed string to this stream in the current encoding of the BinaryWriter,
        /// and advances the current position of the stream in accordance with the encoding used and the
        /// specific characters being written to the stream. 
        /// </summary>
        /// <param name="value">The string to write</param>
        public virtual void Write( string value )
        {
            byte[ ] buf = this.CharEncoding.GetBytes( value );
            this.Write7BitEncodedInt( buf.Length );
            this.OutStream.Write( buf, 0, buf.Length );
        }

        /// <summary>
        /// Writes a two-byte unsigned integer to the current stream and advances the stream position by two bytes. 
        /// </summary>
        /// <param name="value">The two-byte unsigned integer to write. </param>
        public virtual void Write( ushort value )
        {
            this.Buffer[ 0 ] = ( byte )value;
            this.Buffer[ 1 ] = ( byte )( value >> 8 );
            this.OutStream.Write( this.Buffer, 0, 2 );
        }

        /// <summary>
        /// Writes a four-byte unsigned integer to the current stream and advances the stream position by four bytes. 
        /// </summary>
        /// <param name="value">The four-byte unsigned integer to write.</param>
        public virtual void Write( uint value )
        {
            this.Buffer[ 0 ] = ( byte )value;
            this.Buffer[ 1 ] = ( byte )( value >> 8 );
            this.Buffer[ 2 ] = ( byte )( value >> 0x10 );
            this.Buffer[ 3 ] = ( byte )( value >> 0x18 );
            this.OutStream.Write( this.Buffer, 0, 4 );
        }

        /// <summary>
        /// Writes an eight-byte unsigned integer to the current stream and advances the stream position by eight bytes.
        /// </summary>
        /// <param name="value">The eight-byte unsigned integer to write.</param>
        public virtual void Write( ulong value )
        {
            this.Buffer[ 0 ] = ( byte )value;
            this.Buffer[ 1 ] = ( byte )( value >> 8 );
            this.Buffer[ 2 ] = ( byte )( value >> 0x10 );
            this.Buffer[ 3 ] = ( byte )( value >> 0x18 );
            this.Buffer[ 4 ] = ( byte )( value >> 0x20 );
            this.Buffer[ 5 ] = ( byte )( value >> 40 );
            this.Buffer[ 6 ] = ( byte )( value >> 0x30 );
            this.Buffer[ 7 ] = ( byte )( value >> 0x38 );
            this.OutStream.Write( this.Buffer, 0, 8 );
        }

        /// <summary>
        /// Writes a region of a byte array to the current stream. 
        /// </summary>
        /// <param name="buffer">A byte array containing the data to write.</param>
        /// <param name="index">The starting point in buffer at which to begin writing.</param>
        /// <param name="count">The number of bytes to write.</param>
        public virtual void Write( byte[ ] buffer, int index, int count )
        {
            this.OutStream.Write( buffer, index, count );
        }

        /// <summary>
        /// Writes a section of a character array to the current stream, and advances the current position of the stream in accordance with the Encoding used and perhaps the specific characters being written to the stream.
        /// </summary>
        /// <param name="chars">A character array containing the data to write. </param>
        /// <param name="index">The starting point in buffer from which to begin writing.</param>
        /// <param name="count">The number of characters to write. </param>
        public virtual void Write( char[ ] chars, int index, int count )
        {
            byte[ ] buffer = this.CharEncoding.GetBytes( new string( chars, index, count ) );
            this.OutStream.Write( buffer, 0, buffer.Length );
        }
        /// <summary>
        /// Writes a 32-bit integer in a compressed format. 
        /// </summary>
        /// <param name="value">The 32-bit integer to be written.</param>
        internal void Write7BitEncodedInt( int value )
        {
            uint num = ( uint )value;
            while( num >= 0x80 )
            {
                this.Write( ( byte )( num | 0x80 ) );
                num = num >> 7;
            }
            this.Write( ( byte )num );
        }

        /// <summary>
        /// Gets the underlying stream of the BinaryWriter. 
        /// </summary>
        public virtual Stream BaseStream
        {
            get
            {
                this.Flush( );
                return this.OutStream;
            }
        }
    }
}
