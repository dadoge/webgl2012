////////////////////////////////////////////////
// DESCRIPTION:
//    Bidirectional serial port
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
using Microsoft.SPOT.Hardware;

namespace FusionWare.SPOT.Hardware
{
    /// <summary>Provides read/Write support for a GPIO pin</summary>
    /// <remarks>
    /// <para>The .NET Micro framework class <see href="T:Microsoft.SPOT.Hardware.TristatePort">TristatePort</see>
    /// class is not well named and its <see href="P:Microsoft.SPOT.Hardware.TristatePort.Active">Active</see>
    /// property is rather confusing.</para>
    /// <para>Given these limitations we decided to create a simpler and more rational class wrapper 
    /// for accessing a GPIO port as either an input or an output.</para>
    /// <para> At first having a direction property seemed silly since the read and write functions know
    /// what direction things should be. However, the electrical characteristics of the system change 
    /// with the direction and that can cause problems if not controlled correctly. External hardware
    /// may require the output valid for a specified time  or until some other event.pin is set, etc...
    /// Thus, this class makes no attempt at fully automating the direction. Instead it only ensures 
    /// the mode is set to output when you write to the <see href="State">State</see> property as the
    /// requirement and intent is clear at that point. In all other cases it is up to the calling
    /// application or driver to manage the direction as appropriate for the hardware. 
    /// </para>
    /// </remarks>
    public class BidirectionalPort : DisposableObject
    {
        TristatePort Port;
        
        /// <summary>Creates a new port by wrapping an existing TristatePort</summary>
        /// <param name="Port">TristatePort to wrap</param>
        BidirectionalPort(TristatePort Port)
        {
            this.Port = Port;
        }

        /// <summary>Creates a new Bidirectional port</summary>
        /// <param name="PortId">Pin identifier for the GPIO pin</param>
        /// <param name="InitialState">Initial state of the pin</param>
        /// <param name="GlitchFilter">flag to indicate if glitch filtering should be used</param>
        /// <param name="Resistor">Resistor mode to use if supported by underlying hardware</param>
        BidirectionalPort(Cpu.Pin PortId, bool InitialState, bool GlitchFilter, Port.ResistorMode Resistor)
            :this( new TristatePort(PortId, InitialState, GlitchFilter, Resistor) )
        {
        }
        
        #region IDisposable Support
        /// <summary>Handles releasing the GPIO pins</summary>
        /// <param name="disposing">true if managed and unmanaged resources should be disposed; false if only unmanaged should be</param>
        override protected void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if(!base.IsDisposed)
            {
                // If disposing is true, dispose all managed
                // and unmanaged resources.
                if(disposing)
                {
                    // Dispose managed resources.
                    this.Port.Dispose();
                }
                // Dispose all unmanged resources here 
                // None - in this version
            }
        }
        #endregion

        /// <summary>Used to clearly identify the mode for the pin</summary>
        public enum Direction
        {
            /// <summary>The GPIO pin is configured for Input</summary>
            Input,
            /// <summary>The GPIO Pin is configured for Output</summary>
            Output,
        }

        /// <summary>Gets/Sets the direction for the pin</summary>
        /// <value>Direction of the pin</value>
        public Direction PinDirection
        {
            get { return Port.Active ? Direction.Output : Direction.Input; }
            set { Port.Active = value == Direction.Output; }
        }

        /// <summary>Gets/Sets the state of the pin</summary>
        /// <value>State of the pin</value>
        /// <remarks>
        /// Writing the state value will force the <see href="P:PinDirection">PinDirection</see>
        /// to <see href="F:Direction.Output">Direction.Ouptut</see> automatically. That is - after 
        /// setting the pin state the pin direction will always be Direction.Output. If the calling
        /// code needs any other behaviour it is up to the caller to provide it. 
        /// </remarks>
        public bool State
        {
            get { return this.Port.Read(); }

            set
            {
                this.PinDirection = Direction.Output;
                this.Port.Write(value);
            }
        }

        /// <summary>Cast operator to allow treating this type as if it was derived from Microsoft.SPOT.Hardware.TristatePort</summary>
        /// <param name="p">Port to convert</param>
        /// <returns>The internally wrapped port</returns>
        /// <remarks>
        /// Since the framework <see cref="Microsoft.SPOT.Hardware.TristatePort">TristatePort</see> class is marked sealed
        /// this allows applications to treat a BidirectionalPort as if it was derived from the standard one
        /// </remarks>
        public static implicit operator TristatePort(BidirectionalPort p)
        {
            return p.Port;
        }
    }
}
