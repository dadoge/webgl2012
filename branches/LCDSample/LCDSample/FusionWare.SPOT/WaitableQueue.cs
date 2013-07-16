using System;
using Microsoft.SPOT;
using System.Threading;
using System.Collections;

namespace FusionWare.SPOT.Collections
{
    /// <summary>Thread safe Queue</summary>
    /// <remarks>
    /// Provides a Queue of objects that allows
    /// independent reader and writer threads. 
    /// Since the Queue derives from WaitHandle 
    /// a reader thread can wait on the Queue
    /// until data is present in the Queuue.
    /// </remarks>
    public class WaitableQueue : WaitHandle
    {
        ArrayList Items = new ArrayList();
        ManualResetEvent Event = new ManualResetEvent(false);

        /// <summary>Inserts object O into the queue at the end</summary>
        /// <param name="O">Object to add to the queue</param>
        public void Enqueue( object O )
        {
            lock( Items )
            {
                this.Items.Add( O );
                this.Event.Set();
            }
        }

        /// <summary>Removes an object from the front of the queue</summary>
        /// <returns>Object on the queue</returns>
        /// <remarks>
        /// Removes an item from the queue. If there is no data in 
        /// the queue, this method will wait indefinately for 
        /// data to become available.
        /// </remarks>
        public object Dequeue()
        {
            return this.Dequeue( Timeout.Infinite );
        }

        /// <summary>Removes and object from the front of the queue</summary>
        /// <param name="Timeout">Timeout (in milliseconds) to wait for data</param>
        /// <returns>Object removed from the queue</returns>
        /// <exception cref="System.IO.IOException">Timeout occured</exception>
        /// <exception cref="InvalidOperationException">Queue is empty</exception>
        public object Dequeue(int Timeout)
        {
            object retVal = null;
            if( !this.Event.WaitOne( Timeout, true ) )
                throw new System.IO.IOException( "Timeout" );

            lock( Items )
            {
                if( this.Items.Count == 0 )
                    throw new InvalidOperationException( "Cannot Dequeue on an Empty Queue" );

                retVal = this.Items[ 0 ];
                this.Items.RemoveAt( 0 );
                if( this.Items.Count == 0 )
                    this.Event.Reset();
            }

            return retVal;
        }

        /// <summary>Wait for the Queue to have at least one item in it</summary>
        /// <returns>true if the internal event indicating an item is the queue is set</returns>
        public override bool WaitOne()
        {
            return this.Event.WaitOne();
        }

        /// <summary>Wait for the Queue to have at least one item in it</summary>
        /// <param name="millisecondsTimeout">Timeout (in milliseconds to wait)</param>
        /// <param name="exitContext">set to true to exit the synchronization domain for the context before the wait (in a synchronized context) and then reacquire it; otherwise, set this parameter to false.</param>
        /// <returns>true if the internal event indicating an item is the queue is set; false if not</returns>
        public override bool WaitOne( int millisecondsTimeout, bool exitContext )
        {
            return this.Event.WaitOne( millisecondsTimeout, exitContext );
        }
    }
}
