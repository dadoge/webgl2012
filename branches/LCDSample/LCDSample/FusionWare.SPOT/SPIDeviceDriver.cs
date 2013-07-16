////////////////////////////////////////////////
// DESCRIPTION:
//    SPI Device Driver support
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
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;

namespace FusionWare.SPOT.Hardware
{
    /// <summary>Base class for SPI peripheral device drivers</summary>
    /// <remarks>
    /// <para>This class works in conjunction with the <see cref="SPI"/>
    /// class to simplify access to SPI Bus devices and implementation
    /// of drivers for specific peripherals.</para>
    /// 
    /// <para>Of particular importance is the support for IDisposable.
    /// This class takes care of cleaning up the unmanged resources for the
    /// underlying bus. Furhtermore, the virtual
    /// <see href="Dispose(System.Boolean)">Dispose(bool)</see> /> provides
    /// derived classes the option of cleaning up any additional resources
    /// they might allocate.</para>
    /// </remarks>
    public class SPIDeviceDriver : DisposableObject
    {

        #region IDisposable Support
        /// <summary>Internal implementation of disposed</summary>
        /// <param name="disposing"></param>
        ///<remarks>
        /// <para>This version of Dispose executes in two distinct scenarios.
        /// If the Disposing flag is true, the method has been called
        /// directly or indirectly by a user's code. In this case
        /// Managed and unmanaged resources can be disposed.</para>
        /// <para>If the Disposing flag is false, the method has been called
        /// by the runtime from inside the finalizer and this MUST not
        /// reference other objects. Only unmanaged resources can be disposed.</para>
        /// <para>Derived classes that implement this method MUST call the base class</para>
        /// </remarks>
        protected override void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if(!base.IsDisposed)
            {
                // If disposing equals true, dispose all managed
                if(disposing)
                {
                    // Dispose managed resources.
                    this.Bus.Dispose();
                }
                // in either case release all unmanaged resources, if any
                // In this case - there are none.
            }
        }
        #endregion

        /// <summary>Creates a new SPIDeviceDriver instance</summary>
        /// <remarks>
        /// For maximum isolation and encapsulation Drivers should only require the
        /// application provide the SPI bus and chip select pin information. The rest
        /// of the timing and clock information is specifc to the actual device
        /// capabilities and therefore should be provided internally in the managed
        /// driver rather than being supplied by the application. 
        /// 
        /// This version of the constructor does not instantiate the bus, instead that
        /// is left to the protected set for the <see href="Config" /> property. Typically
        /// a derived device driver class will set the Config property in it's constructor
        /// based on the needs of the particular device. 
        /// </remarks>
        protected SPIDeviceDriver()
        {
            this.Bus = null;
        }

        /// <summary>Configuration for the device</summary>
        /// <value>Device SPI bus configuration</value>
        public SPI.Configuration Config
        {
            get { return this.Bus.Config; }
            protected set
            {
                if( this.Bus == null )
                    this.Bus = new SPI( value );
                else
                    this.Bus.Config = value;
            }
        }

        /// <summary>Writes data to the device</summary>
        /// <param name="WriteBuffer">Data to write</param>
        protected void Write( byte[] WriteBuffer )
        {
            ThrowIfDisposed();
            this.Bus.Write( WriteBuffer );
        }

        /// <summary>Writes data to the device</summary>
        /// <param name="WriteBuffer">Data to write</param>
        protected void Write(ushort[] WriteBuffer)
        {
            ThrowIfDisposed();
            this.Bus.Write( WriteBuffer );
        }

        /// <summary>Writes and reads data to/from the device</summary>
        /// <param name="WriteBuffer">Data to write</param>
        /// <param name="ReadBuffer">Buffer to receive data read</param>
        protected void WriteRead( byte[] WriteBuffer, byte[] ReadBuffer )
        {
            ThrowIfDisposed();
            this.Bus.WriteRead( WriteBuffer, ReadBuffer );
        }

        /// <summary>Writes and reads data to/from the device</summary>
        /// <param name="WriteBuffer">Data to write</param>
        /// <param name="ReadBuffer">Buffer to receive data read</param>
        protected void WriteRead(ushort[] WriteBuffer, ushort[] ReadBuffer)
        {
            ThrowIfDisposed();
            this.Bus.WriteRead( WriteBuffer, ReadBuffer );
        }

        /// <summary>Writes and reads byte data to/from the device</summary>
        /// <param name="WriteBuffer">Data to write</param>
        /// <param name="ReadBuffer">Buffer to receive data read</param>
        /// <param name="ReadClockIndex">Number of 8 bit clock cycles to wait before reading in data</param>
        /// <remarks>
        /// This method writes and reads asymetrically sized arrays of data. The SPI bus is FULL duplex
        /// and technically read data can occur at any point in the data stream. This method is used
        /// when the size of the read and write buffer are not the same or when the read does not begin
        /// on the first clock edge. The ReadClockIndex parameter is used to indicate the number of 
        /// clock cycles to wait before starting the read. To keep things simpler and more intuitive
        /// the clock cycles are specified in terms of bytes rather than actual clock edge transitions.
        /// </remarks>
        /// <example>This demonstrates writing a byte and reading back 2 bytes where the read begins
        /// on the first clock edge following the write.
        ///<code>
        /// byte[] writeBuffer = new byte[] { 0x12 };
        /// byte[] readBuffer = new byte[2];
        /// dev.WriteRead(writeBuffer, readBuffer, 1);
        /// </code>
        /// </example>
        /// <example>This demonstrates writing a 4 byte array and reading back 2 bytes where the read begins
        /// on the first clock edge following the write.
        ///<code>
        /// byte[] writeBuffer = new byte[] { 0x12, 0x34, 0x56, 0x78 };
        /// byte[] readBuffer = new byte[2];
        /// dev.WriteRead(writeBuffer, readBuffer, 4);
        /// </code>
        /// </example>
        /// <example>The Previous examples indicate a common pattern where the read begins
        /// on the first clock edge following the write. Instead of hardcoding the ReadClockIndex
        /// a better option, in such cases, is to use the length of the write buffer.
        ///<code>
        /// byte[] writeBuffer = new byte[] { 0x12, 0x34, 0x56, 0x78 };
        /// byte[] readBuffer = new byte[2];
        /// dev.WriteRead(writeBuffer, readBuffer, writeBuffer.Length);
        /// </code>
        /// </example>
        /// <example>The Previous examples indicate a common pattern where the read begins
        /// on the first clock edge following the write. However it is possible that the
        /// read beginsat some point in the middle while additional data is being written,
        /// since the SPI bus is FULL DUPLEX. This example shows the read beggining after the first
        /// 2 bytes are written but before all the bytes are sent. 
        ///<code>
        /// byte[] writeBuffer = new byte[] { 0x12, 0x34, 0x56, 0x78 };
        /// byte[] readBuffer = new byte[2];
        /// dev.WriteRead(writeBuffer, readBuffer, 2);
        /// </code>
        /// </example>
        protected void WriteRead(byte[] WriteBuffer, byte[] ReadBuffer, int ReadClockIndex)
        {
            ThrowIfDisposed();
            this.Bus.WriteRead(WriteBuffer, ReadBuffer, ReadClockIndex);
        }

        /// <summary>Writes and reads 16bit data to/from the device</summary>
        /// <param name="WriteBuffer">Data to write</param>
        /// <param name="ReadBuffer">Buffer to receive data read</param>
        /// <param name="ReadClockIndex">Number of 16 bit clock cycles to wait before reading in data</param>
        /// <remarks>
        /// This method writes and reads data arrays. The SPI bus is FULL duplex and technically valid
        /// read data can occur at any point in the data stream. This method is used when the size of
        /// the read and write buffer are not the same or when the read does not begin on the first
        /// clock edge. The ReadClockIndex parameter is used to indicate the number of clock cycles
        /// to wait before starting the read. To keep things simpler and more intuitive the clock cycles
        /// are specified in terms of a single 16bit value rather than actual clock edge transitions.
        /// </remarks>
        /// <example>This demonstrates writing a ushort and reading back 2 more where the read begins
        /// on the first clock edge following the write. 
        ///<code>
        /// ushort[] writeBuffer = new ushort[] { 0x12 };
        /// ushort[] readBuffer = new ushort[2];
        /// dev.WriteRead(writeBuffer, readBuffer, 1);
        /// </code>
        /// </example>
        /// <example>This demonstrates writing a 4 ushort array and reading back 2 ushorts where the read begins
        /// on the first clock edge following the write.
        ///<code>
        /// ushort[] writeBuffer = new ushort[] { 0x12, 0x34, 0x56, 0x78 };
        /// ushort[] readBuffer = new ushort[2];
        /// dev.WriteRead(writeBuffer, readBuffer, 4);
        /// </code>
        /// </example>
        /// <example>The Previous examples indicate a common pattern where the read begins
        /// on the first clock edge following the write. Instead of hardcoding the ReadClockIndex
        /// a better option, in such cases, is to use the length of the write buffer.
        ///<code>
        /// ushort[] writeBuffer = new ushort[] { 0x12, 0x34, 0x56, 0x78 };
        /// ushort[] readBuffer = new ushort[2];
        /// dev.WriteRead(writeBuffer, readBuffer, writeBuffer.Length);
        /// </code>
        /// </example>
        /// <example>The Previous examples indicate a common pattern where the read begins
        /// on the first clock edge following the write. However it is possible that the
        /// read beginsat some point in the middle while additional data is being written,
        /// since the SPI bus is FULL DUPLEX. This example shows the read beggining after the first
        /// 2 ushorts are written but before all the ushorts are sent. 
        ///<code>
        /// ushort[] writeBuffer = new ushort[] { 0x12, 0x34, 0x56, 0x78 };
        /// ushort[] readBuffer = new ushort[2];
        /// dev.WriteRead(writeBuffer, readBuffer, 2);
        /// </code>
        /// </example>
        protected void WriteRead(ushort[] WriteBuffer, ushort[] ReadBuffer, int ReadClockIndex)
        {
            ThrowIfDisposed();
            this.Bus.WriteRead(WriteBuffer, ReadBuffer, ReadClockIndex);
        }

        #region Common Register Access
        // pre-allocated data buffers to minimize heap allocation and fragmentation
        // since all functions using these buffers use the Bus as a lock object 
        // this is thread-safe. (the bus methods are threadsafe on their own)
        private byte[] Regx8 = new byte[ 1 ];
        private byte[] Buff1x8 = new byte[ 1 ];
        private byte[] Buff2x8 = new byte[ 2 ];
        private byte[] Buff3x8 = new byte[ 3 ];

        private ushort[] Regx16 = new ushort[ 1 ];
        private ushort[] Buff1x16 = new ushort[ 1 ];
        private ushort[] Buff2x16 = new ushort[ 2 ];

        /// <summary>Reads a 16 bit value from a register</summary>
        /// <param name="Reg">Register id to read from</param>
        /// <returns>16 bit value read from the register</returns>
        /// <remarks>
        /// Many SPI devices require a write of a register id/offset before
        /// any read or write of the data for the register. This function wraps
        /// that up into a single method call for easier implementation and
        /// readability of driver code. The register number is written
        /// to the device and then 16 bits of data are read back.
        /// </remarks>
        protected ushort ReadReg16( ushort Reg )
        {
            lock( this.Bus )
            {
                this.Regx16[ 0 ] = Reg;
                WriteRead(this.Regx16, Buff1x16, 1);
                return this.Buff1x16[0];
            }
        }

        /// <summary>Writes a 16 bit value to a register</summary>
        /// <param name="Reg">Register id to write to</param>
        /// <param name="Value">Value to write to the register</param>
        /// <returns>16 bit value read from the register</returns>
        /// <remarks>
        /// <para>Many SPI devices require a write of a register
        /// id/offset before any read or write of the data for
        /// the register. This function wraps that up into a 
        /// single method call for easier implementation and
        /// readability of driver code. The register number is
        /// written to the device and then 16 bits of data are
        /// written.</para>
        /// </remarks>
        protected void WriteReg16( ushort Reg, ushort Value)
        {
            lock( this.Bus )
            {
                this.Buff2x16[ 0 ] = Reg;
                this.Buff2x16[ 1 ] = Value;
                this.Write( Buff2x16 );
            }
        }

        /// <summary>Reads an 8 bit value from a register</summary>
        /// <param name="Reg">Register id to read from</param>
        /// <returns>8 bit value read from the register</returns>
        /// <remarks>
        /// Many SPI devices require a write of a register id/offset
        /// before any read or write of the data for the register.
        /// This function wraps that up into a single method call for
        /// easier implementation and readability of driver code. The
        /// register number is written to the SPI device and then 8
        /// bits of data are read back.
        /// </remarks>
        protected byte ReadReg8( byte Reg )
        {
            lock( this.Bus )
            {
                this.Regx8[ 0 ] = Reg;
                WriteRead( this.Regx8, this.Buff1x8, 1 );
                return this.Buff1x8[ 0 ];
            }
        }

        /// <summary>Writes an 8 bit value to a register</summary>
        /// <param name="Reg">Register id to write to</param>
        /// <param name="Value">Value to write to the register</param>
        /// <returns>8 bit value read from the register</returns>
        /// <remarks>
        /// <para>Many SPI devices require a write of a register
        /// id/offset before any read or write of the data for the
        /// register. This function wraps that up into a single
        /// method call for easier implementation and readability
        /// of driver code. The register number is written to the 
        /// device and then 8 bits of data are written.</para>
        /// </remarks>
        protected void WriteReg8( byte Reg, byte Value )
        {
            lock( this.Bus )
            {
                Buff2x8[ 0 ] = Reg;
                Buff2x8[ 1 ] = Value;
                Write( Buff2x8 );
            }
        }

        #endregion

        private SPI Bus;
    }
}