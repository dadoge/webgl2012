////////////////////////////////////////////////
// DESCRIPTION:
//   Extensions to the Framework Application class
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
using Microsoft.SPOT.Input;
using Microsoft.SPOT.Hardware;
using Microsoft.SPOT.Presentation;
using FusionWare.SPOT.Hardware;

namespace FusionWare.SPOT
{
    /// <summary>
    /// Extension of the <see cref="Microsoft.SPOT.Application" /> to add support for GPIO button input
    /// event routing.
    /// </summary>
    /// <remarks>
    /// <para>This class extends Microsoft.SPOT.Application to include support
    /// for setting the button input focus and button input capability in
    /// general.</para> 
    ///  
    /// <para>To use this class you must derive your application class from this
    /// one and call one if its constructors from your base class constructor
    /// to provide a ButtonDefinition array for the application.</para>
    /// 
    /// <para>The ButtonDefinition array provides information about the the buttons
    /// used by the application. Some, but not necessarily all of those buttons can
    /// support auto-repeat where multiple button down events are sent automatically
    /// at preset periods until the button is released.</para> 
    /// </remarks>
    /// <example>
    /// Typical application usage:
    /// <code source=".\Samples\BouncyApp\main.cs" region="Application" lang="cs" title="C#"/>
    /// </example>
    public abstract class Application : Microsoft.SPOT.Application
    {
        /// <summary>Initializes a new application instance</summary>
        /// <param name="ButtonDefs">ButtonDefinition array to map GPIO pins to framework button ids for this application</param>
        /// <remarks>
        /// <para>This constructor is protected as it is an abstract class and cannot
        /// be created directly.</para>
        /// <para>
        /// This will create the input provider for the GPIO buttons based on the
        /// definitions provided in the ButtonDefs parameter. The ButtonDefs array 
        /// maps GPI pin values to button IDs and specifies if the button uses auto-repeat
        /// By keeping that information at the top application level it allows different
        /// applications on the same hardware platform to use the available physical buttons
        /// in different ways.
        /// </para>
        /// </remarks>
        protected Application(ButtonDefinition[] ButtonDefs)
        {
            this.InputProvider = new GPIOButtonInputProvider(null, ButtonDefs);
        }

        /// <summary>Initializes a new application instance</summary>
        /// <param name="ButtonDefs">ButtonDefinition array to map GPIO pins to framework button ids for this application</param>
        /// <param name="Delay">Minimum time (in milliseconds) button must be down before auto repeat begins</param>
        /// <param name="Period">Period of time (in milliseconds) between auto repeated ButtonDown actions are sent</param>
        /// <remarks>
        /// <para>This constructor is protected as it is an abstract class and cannot
        /// be created directly.</para>
        /// <para>
        /// This will create the input provider for the GPIO buttons based on the
        /// definitions provided in the ButtonDefs parameter. The ButtonDefs array 
        /// maps GPIO pin values to button IDs and specifies if the button uses auto-repeat.
        /// By keeping that information at the top application level it allows different
        /// applications on the same hardware platform to use the available physical buttons
        /// in different ways.
        /// </para>
        /// </remarks>
        /// <seealso cref="M:FusionWare.Application.GetButtonRepeatRate(System.Int32@,System.Int32@)"/>
        /// <seealso cref="M:FusionWare.Application.SetButtonRepeatRate(System.Int32,System.Int32)"/>
        protected Application(ButtonDefinition[] ButtonDefs, int Delay, int Period)
            : this(ButtonDefs)
        {
            this.InputProvider.SetButtonRepeatRate(Delay, Period);
        }

        /// <summary>Get the current button repeat rate</summary>
        /// <param name="Delay">Minimum time button must be down before auto repeat begins</param>
        /// <param name="Period">Period of time between auto repeated ButtonDown actions are sent</param>
        /// <remarks>Retrieves the current button repeat rate information for the application.</remarks>
        public void GetButtonRepeatRate(out int Delay, out int Period)
        {
            this.InputProvider.GetButtonRepeatRate(out Delay, out Period);
        }

        /// <summary>Sets the button press repeat rate for the application</summary>
        /// <param name="Delay">Minimum time (in milliseconds) button must be down before auto repeat begins</param>
        /// <param name="Period">Period of time (in milliseconds) between auto repeated ButtonDown actions are sent</param>
        /// <remarks>
        /// <para>Sets the current button repeat rate information for the application. When a
        /// button is pressed and held down for the minimum delay time additional button
        /// down events are sent at a rate determined by the Period parameter. Changes to
        /// the repeat rate take place immediately.</para>
        /// <para>The allowed range for the Delay and Period is 40ms-1S. Values outside that range generate
        /// an exception</para>
        /// </remarks>
        public void SetButtonRepeatRate(int Delay, int Period)
        {
            this.InputProvider.SetButtonRepeatRate(Delay, Period);
        }

        /// <summary>Handles startup event after the dispatcher starts running</summary>
        /// <remarks>
        /// <para>
        /// This handler makes the main window visible and sets the input focus to
        /// the main window or the main window's child if there is one. The standard framework
        /// method Buttons.Focus() will fail unless the current thread has an active
        /// dispatcher. Doing this in the Startup event handler guarantees that the dispatcher
        /// already exists while relieving the developer from having to deal with the issue.
        /// </para>
        /// </remarks>
        /// <seealso cref="M:Microsoft.SPOT.Application.OnStartup(Microsoft.SPOT.EventArgs)">Microsoft.SPOT.Application.OnStartup</seealso>
        /// <param name="e">Standard Event arguments</param>
        protected override void OnStartup(EventArgs e)
        {
            base.OnStartup(e);

            // Make the main window visible (Needed for it to get the focus)
            // windows are created with Collapsed visibility in the constructor
            this.MainWindow.Visibility = Visibility.Visible;
            
            // If the main window has a child set the focus
            // to the child; otherwise set it to the main window
            if (this.MainWindow.Child != null)
                Buttons.Focus(this.MainWindow.Child);
            else
                Buttons.Focus(this.MainWindow);
        }

        // maintains a reference to the Input provider so it won't be GC'd
        GPIOButtonInputProvider InputProvider = null;
    }
}
