using System;
using Microsoft.SPOT;
using System.Threading;
using System.Collections;
using Microsoft.SPOT.Hardware;
using FusionWare.SPOT.Collections;

namespace FusionWare.SPOT.Native
{
    /// <summary>Thread safe queueing and dispatching of events from NativeCode</summary>
    /// <remarks>
    /// <para>The delegates registered with <see cref="Microsoft.SPOT.Hardware.NativeEventDispatcher"/>
    /// are all called from a single internal system thread. Thus they should do as little as possible
    /// and avoid blocking in an uncontrolled fashion as that would prevent the proper functioning
    /// of other NativeEventDispatcher instances.</para>
    /// This class encapsulates a <see cref="HalEventQueue"/> to capture the native event data in a
    /// queue. The data is extracted from the queue by an internal thread that triggers callbacks
    /// to registered delegates through the <see cref="OnHalEvent"/> event. This ensures that the
    /// CLRs internal event thread is blocked for the smallest amount of time possible.
    /// </remarks>
    class HalEventDispatcher : DisposableObject
    {
        HalEventQueue EventQueue;
        Thread DispatcherThreadObj;

        #region IDisposable Support
        /// <summary>Internal implementation of disposed</summary>
        /// <param name="disposing">Flag to indicate context for dispose (See remarks)</param>
        /// <remarks>
        /// <para>This version of Dispose executes in two distinct scenarios.
        /// If the Disposing flag is true, the method has been called
        /// directly or indirectly by a user's code. In this case
        /// Managed and unmanaged resources can be disposed.</para>
        /// <para>If the Disposing flag is false, the method has been called
        /// by the runtime from inside the finalizer and this MUST not
        /// reference other objects. Only unmanaged resources can be disposed.</para>
        /// </remarks>
        protected override void Dispose( bool disposing )
        {
            // Check to see if Dispose has already been called.
            if(!IsDisposed)
            {
                // If disposing is true, dispose managed resources.
                if(disposing)
                {
                    this.EventQueue.Dispose();
                    this.DispatcherThreadObj.Abort();
                }
                // in either case, dispose unmanaged resources
                // [ None needed for this class]
            }
        }
        #endregion

        /// <summary>Event that is fired when a native event is triggered</summary>
        public event NativeEventHandler OnHalEvent;

        /// <summary>Creates a new <see cref="HalEventDispatcher"/></summary>
        /// <param name="DriverName">Name of the HAL driver providing the event data</param>
        /// <param name="DrvData">Driver specific context data for this event queue</param>
        public HalEventDispatcher( string DriverName, ulong DrvData )
        {
            this.EventQueue = new HalEventQueue( DriverName, DrvData );
            this.DispatcherThreadObj = new Thread( DispatcherThread );
            this.DispatcherThreadObj.Start();
        }

        private void DispatcherThread()
        {
            // infinite loop until the thread is aborted
            while( true )
            {
                // blocking Dequeue with inifinite wait
                NativeEventData data = this.EventQueue.Dequeue();
                if( this.OnHalEvent != null )
                {
                    try
                    {
                        OnHalEvent( data.data1, data.data2, data.TimeStamp );
                    }
                    catch(Exception ex)
                    {
                        System.Diagnostics.Debug.Print( "Exception in OnHalEvent Handler: " + ex.ToString() );
                    }
                }
            }
        }
    }
}
