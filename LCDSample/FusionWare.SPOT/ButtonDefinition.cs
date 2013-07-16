////////////////////////////////////////////////
// DESCRIPTION:
//    Button input handling support
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
using Microsoft.SPOT.Input;
using Microsoft.SPOT.Hardware;

namespace FusionWare.SPOT.Hardware
{
    /// <summary>This class provides information about a button connected to a GPIO pin</summary>
    /// <remarks>
    /// The .NET Micro framework requires configuration of each GPIO pin used as a button
    /// for InputEvent processing. This class provides all the information needed for that
    /// purpose. Typically an application derived from
    /// <see cref="FusionWare.SPOT.Application">FusionWare.Application</see>
    /// supplies the button mapping to the base constructor. This allows the greatest
    /// flexibility as different applications on the same hardware platform may choose to
    /// use the buttons in different ways.
    /// </remarks>
    public class ButtonDefinition
    {
        /// <summary>Constructs a new button definition</summary>
        /// <remarks>
        /// The resistor mode can influence the detection of an up/down press.
        /// Setting it incorrectly can cause unexpected results. Check with the
        /// hardware design schematics or documentation for best results. 
        /// </remarks>
        /// <param name="Button">The button value to assign to the GPIO pin.</param>
        /// <param name="AutoRepeat">Boolean flag to indicate if this button should auto-repeat</param>
        /// <param name="ResistorMode">
        /// Resistor mode to configure for the GPIO pin [may be ignored on hardware that
        /// does not support dynamic configuration ]
        /// </param>
        /// <param name="InterruptMode">
        /// Interrupt mode to configure for the GPIO pin [may be ignored on hardware that
        /// does not support dynamic configuration ]
        /// </param>
        public ButtonDefinition( Microsoft.SPOT.Hardware.Button Button
                               , bool AutoRepeat
                               , Port.ResistorMode ResistorMode
                               , Port.InterruptMode InterruptMode
                               )
        {
            this._Button = Button;
            this._Pin = Pin;
            this._AutoRepeat = AutoRepeat;
            this._ResistorMode = ResistorMode;
            this._InterruptMode = InterruptMode;
        }

        /// <summary>Constructs a new button definition</summary>
        /// <remarks>This version of the constructor defaults the ResistorMode to PullUp</remarks>
        /// <param name="Button">The button value to assign to the GPIO pin.</param>
        /// <param name="AutoRepeat">Boolean flag to indicate if this button should auto-repeat</param>
        public ButtonDefinition(Microsoft.SPOT.Hardware.Button Button
                               , bool AutoRepeat
                               )
        {
            this._Button = Button;
            this._Pin = HardwareProvider.HwProvider.GetButtonPins(Button);
            this._AutoRepeat = AutoRepeat;
        }

        /// <summary>Indicates if auto-repeat is enabled</summary>
        /// <remarks>
        /// If auto-repeat is enabled the system will automatically repeat the ButtonDown
        /// action after the button is held down for a specified delay and then periodically send
        /// new ButtonDown actions (repeat). The Delay and repeat period are configured on an 
        /// application wide basis using the
        /// <see href="FusionWare.Application.SetButtonRepeatRate">FusionWare.Application.SetButtonRepeatRate()</see>
        /// method.
        /// </remarks>
        /// <value>Boolean indicating if Auto repeat is enabled for this button</value>
        public bool AutoRepeat
        {
            get { return _AutoRepeat; }
        }
        private bool _AutoRepeat;

        /// <summary>Button id to associate with the GPIO pin</summary>
        /// <value>Standard button ID to associate with a specific hardware button</value>
        /// <remarks>
        /// Each button must have an ID associated with it from the standard MS
        /// definitions
        /// </remarks>
        /// <seealso cref="Microsoft.SPOT.Hardware.Button" cat=".NET Micro Framework" />
        public Microsoft.SPOT.Hardware.Button Button
        {
            get { return _Button; }
        }
        private Microsoft.SPOT.Hardware.Button _Button;

        /// <summary>GPIO Pin for this button</summary>
        /// <value>GPIO Pin for this particular button</value>
        /// <remarks>
        /// This property holds the GPIO Pin for this particular button, typically it is a
        /// value from an enumeration specific to a particular hardware platform.
        /// </remarks>
        /// <seealso cref="Microsoft.SPOT.Hardware.Cpu.Pin" cat=".NET Micro Framework" />
        public Microsoft.SPOT.Hardware.Cpu.Pin Pin
        {
            get { return _Pin; }
        }
        private Microsoft.SPOT.Hardware.Cpu.Pin _Pin;

        /// <summary>Resistor mode for this GPIO pin</summary>
        /// <remarks>
        /// The resistor mode can influence the detection of an up/down press.
        /// Setting it incorrectly can cause unexpected results. Check with the
        /// hardware design schematics or documentation for best results. 
        /// </remarks>
        /// <value>The resistor mode for this pin. (Disabled, PullDown, PullUp)</value>
        /// <seealso cref="Microsoft.SPOT.Hardware.Port.ResistorMode" cat=".NET Micro Framework" />
        public Microsoft.SPOT.Hardware.InterruptPort.ResistorMode ResistorMode
        {
            get { return _ResistorMode; }
        }
        private  Microsoft.SPOT.Hardware.InterruptPort.ResistorMode _ResistorMode = Port.ResistorMode.PullUp;

        /// <summary>Interupt mode for this GPIO pin</summary>
        /// <remarks>
        /// The interrupt mode can influence the detection of an up/down press.
        /// Setting it incorrectly can cause unexpected results. Check with the
        /// hardware design schematics or documentation for best results. 
        /// </remarks>
        /// <value>The Interrupt mode for this pin. </value>
        /// <seealso cref="Microsoft.SPOT.Hardware.Port.InterruptMode" cat=".NET Micro Framework" />
        public Microsoft.SPOT.Hardware.InterruptPort.InterruptMode InterruptMode
        {
            get { return _InterruptMode; }
        }
        private Microsoft.SPOT.Hardware.InterruptPort.InterruptMode _InterruptMode = InterruptPort.InterruptMode.InterruptEdgeHigh;
    }
}

