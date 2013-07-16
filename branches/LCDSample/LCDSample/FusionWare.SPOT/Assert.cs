////////////////////////////////////////////////
// DESCRIPTION:
//    Debug assert support
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

namespace System.Diagnostics
{
    /// <summary>Provides support for missing functionality in the .NET Micro Framework</summary>
    /// <remarks>
    /// This class provides some standard functionality of the FULL framework that
    /// are not availalbe or implmented differently in a different namespace in the
    /// standard .NET Micro Framework classes. 
    /// </remarks>
    public sealed class Debug
    {
        internal Debug()
        {
        }

        /// <summary>Provides debug assert capabilities</summary>
        /// <remarks>For unknown reasons MS put the assert support into the SPOT namespace 
        /// instead of the normal location. This implementation tests the expression and
        /// triggers a debug break point if it is not true. Thus you will get a break point
        /// inside this function and you will have complete call stack info etc...
        /// 
        /// If DEBUG is not defined or there is no debugger attached this does nothing.
        /// </remarks>
        /// <param name="expression">boolean value that, if false, will trigger a debug
        /// breakpoint.</param>
        [Conditional("DEBUG")]
        public static void Assert(bool expression)
        {
            if(System.Diagnostics.Debugger.IsAttached && !expression)
                System.Diagnostics.Debugger.Break();
        }

        /// <summary>Prints data to the debug output stream</summary>
        /// <param name="Msg">text to print</param>
        /// <remarks>For unknown reasons MS put the debug print support into the SPOT namespace 
        /// instead of the normal location. This simply wraps the standard SPOT version for 
        /// closer compatability it does not add any additional support like formatted string output
        /// </remarks>
        [Conditional("DEBUG")]
        public static void Print(string Msg)
        {
            Microsoft.SPOT.Debug.Print(Msg);
        }
    }
}
