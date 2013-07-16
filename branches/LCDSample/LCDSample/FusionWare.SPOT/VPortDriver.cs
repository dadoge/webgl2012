///////////////////////////////////////////////////////////////////////////////////////////
// Description:
//   Virtual Serial port driver support
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
using System.Threading;
using FusionWare.SPOT.IO;
using Microsoft.SPOT.Hardware;
using FusionWare.SPOT.Hardware;
using FusionWare.SPOT.Runtime.Serialization;

namespace FusionWare.SPOT.Native
{
    /// <summary>Base class for a Virtual Port based driver</summary>
    /// <remarks>
    /// Standardizes and supports access to extended HAL
    /// native code implementations (e.g. interop) via virtual
    /// serial ports.
    /// 
    /// <para>All code using this <b>MUST</b> isolate access to as thin
    /// a layer as possible since future CLR revisions may have an
    /// official interop solution. If/When interop is available this
    /// class will be removed from the library and all code using it
    /// should be re-written to utilize the official interop mechanisms.</para>
    /// </remarks>
    [Obsolete("Use .NET MF Interop support instead")]
    public class VPortDriver : DisposableObject
    {
        SerialStream Port;

        /// <summary>Creates a new Virtual Serial port driver instance</summary>
        /// <param name="DeviceID">Port Number of the virtual serial port in the HAL</param>
        public VPortDriver(int DeviceID)
        {
            SerialPort.Serial portnum = (SerialPort.Serial)( DeviceID );
            this.Port = new SerialStream(new ComPort(portnum, 0, false), Timeout.Infinite, Timeout.Infinite);
        }

        /// <summary>Marshals an object to the virtual serial port stream</summary>
        /// <param name="o">Object to marshal</param>
        public void Write(object o)
        {
            // need to create a single array to prevent
            // multiple calls to the HAL write function
            byte[] buf = BinarySerializer.Serialize( o );
            this.Port.Write(buf, 0, buf.Length );   
        }

        /// <summary>unmarshals an object from the HAL virtual port stream</summary>
        /// <param name="o">Object to unmarshal</param>
        public void Read(object o)
        {
            BinarySerializer.DeSerialize( o, Port );
        }

        /// <summary>Standard Dispose support</summary>
        protected override void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if( !base.IsDisposed )
            {
                // If disposing is true, dispose managed resources.
                if( disposing )
                {
                    // Dispose managed resources.
                    this.Port.Dispose();
                }
                // in either case, dispose unmanaged resources
                // { None in this class }
            }
        }
    }
}
