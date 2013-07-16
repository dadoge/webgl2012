////////////////////////////////////////////////
// DESCRIPTION:
//    IDs for defined Virtual Serial ports
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

namespace FusionWare.SPOT.Native
{
    /// <summary>id mapping for virtual port interop devices</summary>
    /// <remarks>
    /// These IDs <b>MUST</b> be kept in sync between all three of the following:
    /// <list type="number">
    /// <item><description>FusionWare.SPOT.EmulatorComponents</description></item>
    /// <item><description>FusionWare.SPOT.dll</description></item>
    /// <item><description>HAL Implementation</description></item>
    /// </list>
    /// Unfortunately Team Foundation Server, used by codeplex, does not support
    /// sharing files between differnt subprojects in a source tree so this enum
    /// must be edited in manually in both places.
    /// 
    /// <para><b>THESE IDS ARE SUBJECT TO CHANGE AND OR REMOVAL AT ANY TIME.
    /// OEMS MUST NOT USE OR RELY ON THESE AS THEY WILL BE REMOVED IN FUTURE
    /// VERSIONS OF THE LIBRARIES WITHOUT ANY WARNING OR SUPPORT. THESE WILL
    /// BE REMOVED OR OBSOLETED IF AN OFFICIAL INTEROP INTEROP SOLUTION IS
    /// AVAILABLE FROM MICROSOFT.</b></para>
    /// 
    /// VPortIDs in the range 0xEF00 - 0xEFFF are reserved for definition by
    /// the FusionWare.SPOT libraries OEMs <b>MUST NOT</b> use values in this range
    /// </remarks>
    [Obsolete("Use .NET Micro Framework Interop support instead")]
    enum VPortIDs : int
    {
        VPORT_DEV_VERSION = 0xEF00,
    }
}