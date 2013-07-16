////////////////////////////////////////////////
// DESCRIPTION:
//    NullStream implementation
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
using FusionWare.SPOT;
using FusionWare.SPOT.IO;
using Microsoft.SPOT.Hardware;
using FusionWare.SPOT.Hardware;
using System.Runtime.InteropServices;
using FusionWare.SPOT.Runtime.Serialization;

namespace FusionWare.SPOT.Native
{
    /// <summary>Provides system specific information from the HAL</summary>
    /// <remarks>
    /// Not all HAL implementations will support the "virtual serial port
    /// pseudo interop" required by this class. The
    /// <see cref="T:FusionWare.SPOT.EmulatorComponents.VersionInfo"/> class implements 
    /// version support for the emulator. Real devices require support from the
    /// HAL. 
    /// </remarks>
    [Obsolete("Use Microsoft.SPOT.SystemInfo directly")]
    public static class SystemInfo
    {
        /// <summary>Retrieves a <see cref="VersionInfo"/> instance for the current firmware</summary>
        /// <remarks>
        /// This method retrieves a <see cref="VersionInfo"/> object instance representing
        /// the version information for the firmware currently on the device. If the
        /// firmware does not support retrieving the version information the result is version
        /// 0.0.0.0
        /// </remarks>
        public static Version VersionNumber
        {
            get
            {
                return Microsoft.SPOT.Hardware.SystemInfo.Version;
            }
        }

        /// <summary>Get the Version number of the device Firmware (CLR+HAL+PAL)</summary>
        /// <value>Version number string of the form "VM.mm.BBBBB.bbbbb"</value>
        public static string Version
        {
            get
            {
                return "V" + Microsoft.SPOT.Hardware.SystemInfo.Version.ToString();
            }
        }
    }
}
