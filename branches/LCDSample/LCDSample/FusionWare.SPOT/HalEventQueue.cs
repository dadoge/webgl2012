using System;
using Microsoft.SPOT;
using System.Threading;
using FusionWare.SPOT.Collections;

namespace FusionWare.SPOT.Native
{
    /// <summary>Data structure containing information about a native code event</summary>
    /// <remarks>
    /// This structure is used to store the data received by a native event handler
    /// delegate. It is normally stored in a waitable queue so that an application
    /// can retrieve the data at a later point
    /// </remarks>
    public class NativeEventData
    {
        /// <summary>Generic data item 1</summary>
        public uint data1;
        /// <summary>Generic data item 2</summary>
        public uint data2;
        /// <summary>Time stamp for when the event occured</summary>
        public DateTime TimeStamp;

        /// <summary>Creates a new structure</summary>
        /// <param name="data1">Generic data item 1</param>
        /// <param name="data2">Generic data item 2</param>
        /// <param name="TimeStamp">Time stamp for when the event occured</param>
        public NativeEventData( uint data1, uint data2, DateTime TimeStamp )
        {
            this.data1 = data1;
            this.data2 = data2;
            this.TimeStamp = TimeStamp;
        }
    }
   
    /// <summary>Thread safe Queue for HAL events</summary>
    /// <remarks>
    /// <para>The delegates registered with <see cref="Microsoft.SPOT.Hardware.NativeEventDispatcher"/>
    /// are all called from a single internal system thread. Thus, they should do as little as possible
    /// and avoid blocking in an uncontrolled fashion as that would prevent the proper functioning
    /// of other NativeEventDispatcher instances.</para>
    /// <para>This class encapsulates that by providing a queue of events and standard implementation
    /// for the event handler delegate. The event handler will store the parameters as a 
    /// <see cref="NativeEventData"/> structure in the queue and then return. Thus it keeps the time on
    /// the internal system thread to a minimum. </para>
    /// <para>There is a trade off using this approach. Using HalEventQueue requires an additional thread 
    /// in the system to read the queue. There is some overhead for each thread. Thus the tradeoff is between
    /// safety of a complete block and the additional resources of another thread.
    /// </para>
    /// </remarks>
    public sealed class HalEventQueue : WaitHandle, IDisposable
    {
        WaitableQueue Q = new WaitableQueue();
        Microsoft.SPOT.Hardware.NativeEventDispatcher Dispatcher;

        #region IDisposable Support
        /// <summary>Releases unmanaged resources for this object</summary>
        /// <remarks>
        /// This releases the underlying managed and unmanaged resources.
        /// The C# compiler will inject calls to Dispose() for the close
        /// of scope on "using" statements.
        /// </remarks>
        public void Dispose()
        {
            Dispose(true);
            // This object will be cleaned up by the Dispose method.
            // Therefore, GC.SupressFinalize is called to
            // take this object off the finalization queue
            // and prevent finalization code for this object
            // from executing a second time.
            GC.SuppressFinalize(this);
        }

        void Dispose( bool disposing )
        {
            if( !IsDisposed )
            {
                // If disposing is true, dispose managed resources.
                if( disposing )
                {
                    this.Dispatcher.Dispose();
                }
                // in either case, dispose unmanaged resources
                // [None needed in this class]
            }
        }

        /// <summary>Finalizer for this class</summary>
        /// <remarks>
        /// Use C# destructor syntax for finalization code.
        /// This destructor will run only if the Dispose method
        /// does not get called to suppress finalization.
        /// </remarks>
        ~HalEventQueue()
        {
            // Do not re-create Dispose clean-up code here.
            // Calling Dispose(false) is optimal in terms of
            // readability and maintainability.
            Dispose(false);
        }

        /// <summary>Indicates if this driver is disposed</summary>
        /// <value>true if the Driver is disposed already; false if not</value>
        public bool IsDisposed
        {
            get { return _IsDisposed; }
        }
        private bool _IsDisposed = false;

        /// <summary>Utility method to throw an excpetion if this instance is already disposed/closed</summary>
        void ThrowIfDisposed()
        {
            if(this._IsDisposed)
                throw new InvalidOperationException("Cannot use a disposed Object");
        }
        #endregion

        /// <summary>Creates a new HalEventQueue</summary>
        /// <param name="DriverName">Name of the HAL native driver event source</param>
        /// <param name="DrvData">Drver specific context data for this event</param>
        public HalEventQueue( string DriverName, ulong DrvData )
        {
            Dispatcher = new Microsoft.SPOT.Hardware.NativeEventDispatcher( DriverName, DrvData );
            Dispatcher.OnInterrupt += new Microsoft.SPOT.Hardware.NativeEventHandler( Dispatcher_OnInterrupt );
        }

        // WARNING: this method is called on single CLR internal SYSTEM event thread
        void Dispatcher_OnInterrupt( uint data1, uint data2, DateTime time )
        {
            if( !this.IsDisposed )
                this.Q.Enqueue( new NativeEventData( data1, data2, time ) );
        }

        #region Queueing methods
        /// <summary>Adds event data into the queue</summary>
        /// <param name="EventData">Data to add to the queue</param>
        /// <remarks>
        /// Normally event data is added to the queue internaly in response to 
        /// a native code event callback. This method allows an application to 
        /// "stuff" event data into a queue. This can be useful for unit testing
        /// and other such scenarios. 
        /// </remarks>
        public void Enqueue( NativeEventData EventData )
        {
            ThrowIfDisposed();
            this.Q.Enqueue( EventData );
        }

        /// <summary>Removes an item from the queue</summary>
        /// <returns>Item in the queue</returns>
        /// <remarks>
        /// If the queue is not empty this method will return the first
        /// item in the queue. If the queu is empty this method will wait
        /// indefinately for an item to become available.
        /// </remarks>
        public NativeEventData Dequeue()
        {
            ThrowIfDisposed(); 
            return ( NativeEventData )this.Q.Dequeue();
        }

        /// <summary>Removes an item from the queue</summary>
        /// <param name="Timeout">Timeout (in milliseconds) to wait for something in the queue</param>
        /// <returns>Item in the queue</returns>
        public NativeEventData Dequeue( int Timeout )
        {
            ThrowIfDisposed();
            return ( NativeEventData )this.Q.Dequeue( Timeout );
        }
        #endregion

        #region WaitHandle Overrides
        /// <summary>Wait for data availble in the queue</summary>
        /// <returns>true if data available event is set; does not return otherwise</returns>
        public override bool WaitOne()
        {
            ThrowIfDisposed();
            return this.Q.WaitOne();
        }

        /// <summary>Wait for data available in the queue</summary>
        /// <param name="millisecondsTimeout">Timeout (in milliseconds) to wait for data</param>
        /// <param name="exitContext">Set to true to exit the synchronization domain for the context before the wait (in a synchronized context) and then reacquire it; otherwise, set this parameter to false.</param>
        /// <returns>true if the internal data available event is set; false if not</returns>
        public override bool WaitOne( int millisecondsTimeout, bool exitContext )
        {
            ThrowIfDisposed();
            return this.Q.WaitOne( millisecondsTimeout, exitContext );
        }
        #endregion
    }
}
