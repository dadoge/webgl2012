////////////////////////////////////////////////
// DESCRIPTION:
//    Button input with support for Auto-repeat
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
using System.Threading;

using Microsoft.SPOT;
using Microsoft.SPOT.Input;
using Microsoft.SPOT.Hardware;
using Microsoft.SPOT.Presentation;

namespace FusionWare.SPOT.Hardware
{
    
    internal class GPIOButtonInputProvider
    {
        private GpioButtonHandler[] Buttons;
        private DispatcherOperationCallback ReportInputFunc;
        private int RepeatDelay;
        private int RepeatPeriod;

        private InputProviderSite InputSite;
        private PresentationSource PresentationSource;
        
        private readonly Microsoft.SPOT.Dispatcher Dispatcher;


        public GPIOButtonInputProvider(PresentationSource source, ButtonDefinition[] ButtonDefinitions)
        {
            this.RepeatDelay = 500;
            this.RepeatPeriod = 150;
            this.PresentationSource = source;
            this.InputSite = InputManager.CurrentInputManager.RegisterInputProvider(this);
            this.ReportInputFunc = new DispatcherOperationCallback( ReportInputCallback );

            this.Dispatcher = Microsoft.SPOT.Dispatcher.CurrentDispatcher;
            this.Buttons = new GpioButtonHandler[ButtonDefinitions.Length];
            for( int i = 0; i < ButtonDefinitions.Length; i++ )
            {
                ButtonDefinition btnDef = ButtonDefinitions[i];
                this.Buttons[i] = new GpioButtonHandler( this, btnDef);
            }
        }

        private object ReportInputCallback( object report )
        {
            InputReportArgs args = ( InputReportArgs )report;
            return InputSite.ReportInput( args.Device, args.Report );
        }

        public void GetButtonRepeatRate(out int delay, out int period)
        {
            delay = this.RepeatDelay;
            period = this.RepeatPeriod;
        }

        public void SetButtonRepeatRate(int delay, int period)
        {
            if ((delay != 0) && (((delay < 40) || (delay > 1000)) || ((period < 40) || (period > 1000))))
            {
                throw new Exception("Invalid button repeat rate specified");
            }
            this.RepeatDelay = delay;
            this.RepeatPeriod = period;
        }

        void ReportInput( DateTime TimeStamp, Button TheButton, RawButtonActions Actions )
        {
            RawButtonInputReport report = new RawButtonInputReport( this.PresentationSource, TimeStamp, TheButton, Actions );
            this.Dispatcher.BeginInvoke( this.ReportInputFunc, new object[ ] { report } );
        }

        internal class GpioButtonHandler
        {
            public GpioButtonHandler(GPIOButtonInputProvider Provider, ButtonDefinition ButtonDef)
            {
                this.Provider = Provider;
                this.ButtonDef = ButtonDef;
                this.State = true;
                this.Port = new InterruptPort(ButtonDef.Pin, true, ButtonDef.ResistorMode, InterruptPort.InterruptMode.InterruptEdgeBoth);
                this.Port.OnInterrupt += new NativeEventHandler(this.Interrupt);
            }

            private void Interrupt(uint Pin, uint PinState, DateTime TimeStamp)
            {
                this.State = ( PinState == 0 ? false : true );
                this.Provider.ReportInput( TimeStamp
                                         , this.ButtonDef.Button
                                         , this.State ? RawButtonActions.ButtonUp : RawButtonActions.ButtonDown
                                         );

                if(this.ButtonDef.AutoRepeat)
                {
                    // If the button is down make sure the auto repeat timer is running
                    if(!this.State)
                    {
                        if ((this.Timer == null) && (this.Provider.RepeatDelay > 0))
                        {
                            this.Timer = new ExtendedTimer( new TimerCallback(this.OnTimer)
                                                          , null
                                                          , this.Provider.RepeatDelay
                                                          , this.Provider.RepeatPeriod
                                                          );
                        }
                    }
                    else
                        if(this.Timer != null)
                        {
                            // shut down the auto repeat timer on button up
                            this.Timer.Change(-1, -1);
                            this.Timer.Dispose();
                            this.Timer = null;
                        }
                }
            }

            protected void OnTimer(object stateObj)
            {
                // if the timer ticked and the button is down
                // send a new button down event report.
                if(!this.State)
                {
                    this.Provider.ReportInput( DateTime.Now
                                             , this.ButtonDef.Button
                                             , RawButtonActions.ButtonDown
                                             );
                }
            }

            ButtonDefinition ButtonDef;
            private InterruptPort Port;
            private GPIOButtonInputProvider Provider;
            private bool State;
            private ExtendedTimer Timer;
        }
    }
}

