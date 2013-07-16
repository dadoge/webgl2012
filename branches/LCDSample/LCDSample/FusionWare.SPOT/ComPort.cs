////////////////////////////////////////////////
// DESCRIPTION:
//    Extended Serial port support
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
using System.Text;
using System.IO.Ports;
using Microsoft.SPOT.Hardware;

namespace FusionWare.SPOT.IO.Ports
{
    /// <summary>Extends <see cref="System.IO.Ports.SerialPort"/> to provide some useful missing functionality</summary>
    /// <remarks>
    /// Simple extension of the <see cref="System.IO.Ports.SerialPort"/> class 
    /// The key extensions are:
    /// <list type="bullet">
    /// <item><description >Writing of complete byte arrays without the need for offset and count params</description></item>
    /// <item><description >Writing of string data using <see cref="System.Text.Encoding">Text Encoding</see></description></item>
    /// </list>
    /// </remarks>
    [Obsolete("Use SerialStream and TextReader/TextWriter instead")]
    public class ComPort : SerialPort
    {
        /// <summary>Creates new ComPort instance</summary>
        /// <param name="portName">Name of the port (e.g. COM1)</param>
        public ComPort( string portName )
            : base( portName )
        {
        }

        /// <summary>Creates new ComPort instance</summary>
        /// <param name="portName">Name of the port (e.g. COM1)</param>
        /// <param name="baudRate">baudrate for the port</param>
        public ComPort( string portName, int baudRate )
            : base( portName, baudRate )
        {
        }

        /// <summary>Creates new ComPort instance</summary>
        /// <param name="portName">Name of the port (e.g. COM1)</param>
        /// <param name="baudRate">baudrate for the port</param>
        /// <param name="parity">parity for the port</param>
        public ComPort( string portName, int baudRate, Parity parity )
            : base( portName, baudRate, parity )
        {
        }

        /// <summary>Creates new ComPort instance</summary>
        /// <param name="portName">Name of the port (e.g. COM1)</param>
        /// <param name="baudRate">baudrate for the port</param>
        /// <param name="parity">parity for the port</param>
        /// <param name="dataBits">number of data bits</param>
        public ComPort( string portName, int baudRate, Parity parity, int dataBits )
            : base( portName, baudRate, parity, dataBits )
        {
        }

        /// <summary>Creates new ComPort instance</summary>
        /// <param name="portName">Name of the port (e.g. COM1)</param>
        /// <param name="baudRate">baudrate for the port</param>
        /// <param name="parity">parity for the port</param>
        /// <param name="dataBits">number of data bits</param>
        /// <param name="stopBits">number of stop bits</param>
        public ComPort( string portName, int baudRate, Parity parity, int dataBits, StopBits stopBits )
            : base( portName, baudRate, parity, dataBits, stopBits )
        {
        }
      
        /// <summary>Writes a complete byte array to the port</summary>
        /// <param name="buf">byte array of data to write</param>
        /// <returns>number of bytes succesfully written</returns>
        public int Write( byte[] buf )
        {
            return base.Write( buf, 0, buf.Length );
        }

        /// <summary>Writes a string to the serial port using the current text encoding</summary>
        /// <param name="Txt">String to send to the port</param>
        /// <returns>Number of bytes successfully written to the stream</returns>
        public int Write( string Txt)
        {
            return Write(this._Encoding.GetBytes(Txt));
        }

        /// <summary>Get/Set the text encoding for the port</summary>
        /// <value>The text encoding for the port</value>
        /// <remarks>
        /// Default encoding is UTF8. You can override that by creating 
        /// a new text encoding instance and setting this property.
        /// Changes to the encoding take place for the next string
        /// written. (E.g. the change does not impact data already
        /// in buffers and FIFOs but not yet flushed.)
        /// </remarks>
        public System.Text.Encoding Encoding
        {
            get { return _Encoding; }
            set { _Encoding = value; }
        }
        private System.Text.Encoding _Encoding = new System.Text.UTF8Encoding();
    }
}
