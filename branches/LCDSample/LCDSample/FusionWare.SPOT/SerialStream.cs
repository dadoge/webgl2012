////////////////////////////////////////////////
// DESCRIPTION:
//    Stream Wrapper for serial ports
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
using Microsoft.SPOT;
using System.IO.Ports;
using Microsoft.SPOT.Hardware;
using System.Runtime.InteropServices;

namespace FusionWare.SPOT.IO
{
    /// <summary>Stream wrapper for a serial port</summary>
    [Serializable]
    public sealed class SerialStream : Stream
    {
        SerialPort Port;
        byte[] OneByte = new byte[1];
        object PortReadSynch = new object();
        object PortWriteSynch = new object();

        /// <summary>Inter character read timeout</summary>
        [Obsolete("Use the SerialPort.ReadTimeout property to specify the port timeout")]
        public int ReadTimeOut
        {
            get { return this.Port.ReadTimeout; }
            set { this.Port.ReadTimeout = value; }
        }

        /// <summary>Total timeout allowed for each read operation</summary>
        public int ReadTotalTimeout
        {
            get { return (int)( _ReadTotalTimeout.Ticks / (long)TimeSpan.TicksPerMillisecond ); }
            set { _ReadTotalTimeout = new TimeSpan( TimeSpan.TicksPerMillisecond * value ); }
        }
        private TimeSpan _ReadTotalTimeout;

        /// <summary>Creates a new SerialStream instance</summary>
        /// <param name="Port">Serial port to use for the stream</param>
        /// <param name="ReadTimeOut">Inter character read timeout</param>
        /// <param name="ReadTotalTimeout">Read total timeout</param>
        public SerialStream(SerialPort Port, int ReadTimeOut, int ReadTotalTimeout)
        {
            this.Port = Port;
            this.Port.ReadTimeout = ReadTimeOut;
            this.ReadTotalTimeout = ReadTotalTimeout;
        }

        /// <summary>Creates a new SerialStream instance</summary>
        /// <param name="Port">Serial port to use for the stream</param>
        /// <param name="ReadTotalTimeout">Read total timeout</param>
        public SerialStream( SerialPort Port, int ReadTotalTimeout )
        {
            this.Port = Port;
            this.ReadTotalTimeout = ReadTotalTimeout;
        }

        /// <summary>Flush data buffer in the stream to the serial port hardware</summary>
        public override void Flush()
        {
            lock( this.PortWriteSynch )
            {
                this.Port.Flush();
            }
        }

        /// <summary>Reads data from the serial port stream</summary>
        /// <param name="buffer">Buffer to read data into</param>
        /// <param name="offset">Offset into buffer to start reading first bye into</param>
        /// <param name="count">Total count of bytes to read</param>
        /// <returns>number of bytes actually read</returns>
        public override int Read( byte[] buffer, int offset, int count )
        {
            lock( this.PortReadSynch )
            {
                DateTime stop = DateTime.UtcNow + this._ReadTotalTimeout;
                int totalBytesRead = 0;

                // loop until all bytes actually received
                // with read total timeout in place.
                do
                {
                    int bytesRead = this.Port.Read( buffer, offset, count );
                    count -= bytesRead;
                    offset += bytesRead;
                    totalBytesRead += bytesRead;
                } while( count > 0 && DateTime.UtcNow >= stop );
                return totalBytesRead;
            }
        }

        /// <summary>Reads one byte from the serial stream</summary>
        /// <returns>byte vale (cast to an int)</returns>
        public override int ReadByte()
        {
            lock( this.PortReadSynch )
            {
                if( Read( OneByte, 0, 1 ) > 0 )
                    return OneByte[ 0 ];
                else
                    return -1;
            }
        }

        /// <summary></summary>
        /// <param name="buffer">Buffer to write data from</param>
        /// <param name="offset">Offset into buffer to start writing from</param>
        /// <param name="count">Total count of bytes to write</param>
        public override void Write( byte[] buffer, int offset, int count )
        {
            lock( this.PortWriteSynch )
            {
                // loop on this until all bytes actually sent out. 
                do
                {
                    int bytesWritten = this.Port.Write( buffer, offset, count );
                    count -= bytesWritten;
                    offset += bytesWritten;
                } while( count > 0 );
            }
        }

        /// <summary>Writes a single byte out the serial port</summary>
        /// <param name="value"></param>
        public override void WriteByte( byte value )
        {
            lock( this.PortWriteSynch )
            {
                this.OneByte[ 0 ] = value;
                this.Port.Write( OneByte, 0, 1 );
            }
        }

        /// <summary>Not supported on SerialStreams</summary>
        /// <param name="offset"></param>
        /// <param name="origin"></param>
        /// <returns></returns>
        public override long Seek( long offset, SeekOrigin origin )
        {
            throw new NotSupportedException( "SerailStream Cannot Seek" );
        }

        /// <summary>Not supported on SerialStreams</summary>
        public override void SetLength( long length )
        {
            throw new NotSupportedException( "Cannot set Length on SerailStream" );
        }

        /// <summary>Indicates the stream is readable</summary>
        public override bool CanRead
        {
            get
            {
                return true;
            }
        }

        /// <summary>Indicates SerialStreams cannot seek</summary>
        public override bool CanSeek
        {
            get
            {
                return false;
            }
        }

        /// <summary>Indicates serial Streams are writeable</summary>
        public override bool CanWrite
        {
            get
            {
                return true;
            }
        }

        /// <summary>Indicates 0 length for serial streams</summary>
        public override long Length
        {
            get
            {
                return ( long )0;
            }
        }

        /// <summary>position information not supported on SerialStream</summary>
        public override long Position
        {
            get
            {
                return ( long )0;
            }
            set
            {
            }
        }
    }
}
