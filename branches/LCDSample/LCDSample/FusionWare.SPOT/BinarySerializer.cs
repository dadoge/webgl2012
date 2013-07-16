////////////////////////////////////////////////
// DESCRIPTION:
//    Data serialization
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
using System.IO;
using Microsoft.SPOT;
using System.Reflection;

#if !FULL_FRAMEWORK
// BinaryReader and BinaryWriter don't exist in Micro Framework
// so these provide equivalent substitutes.
using FusionWare.SPOT;
using FusionWare.SPOT.IO;
#endif

namespace FusionWare.SPOT.Runtime.Serialization
{
    /// <summary>Data marshalling for communications</summary>
    /// <remarks>
    /// This class is used to Marshal the data for an object into a byte array
    /// or stream. It is conceptually identical to the <see cref="Microsoft.SPOT.Reflection"/>
    /// class with following exceptions:
    /// <list type="bullet">
    /// <item>BinarySerializer supports both streams and byte arrays</item>
    /// <item>BinarySerializer stores data in little-endian format instead of big-endian</item>
    /// <item>BinarySerializer stores object base first followed by subsequant derived classes with the most derived last</item>
    /// <item>BinarySerializer floating point members (single or double) are not supported at this time</item>
    /// <item>BinarySerializer deserialization requires a default contructor that takes no parameters</item>
    /// <item>BinarySerializer is included in <see cref="T:FusionWare.EmulatorComponents"/>
    ///       for use on the desktop. This allows emulation and desktop code to use a single implementation
    ///       for the serialization/deserialization code. 
    /// </item>
    /// </list> 
    /// </remarks>
    /// <stereotype>Utility</stereotype>
    public static class BinarySerializer
    {
        /// <summary>Serializes data fileds of an object to an array</summary>
        /// <param name="o">Object to marshal</param>
        /// <returns>byte array containing marshalled data</returns>
        static public byte[] Serialize(object o)
        {
            MemoryStream stream = new MemoryStream();
            BinarySerializer.Serialize(o, stream);
            return stream.ToArray();
        }

        /// <summary>Marshals data to a stream</summary>
        /// <param name="o">object to marshal</param>
        /// <param name="stream">stream to marshal the data to</param>
        static public void Serialize(object o, Stream stream)
        {
            BinaryWriter writer = new BinaryWriter(stream);
            BinarySerializer.SerializeFields(writer, o);
        }
       
        /// <summary>Unmarshals data from the given stream</summary>
        /// <param name="o">object to unmarshal into</param>
        /// <param name="stream">stream to unmarshal data from</param>
        static public void DeSerialize( object o, Stream stream )
        {
            BinaryReader reader = new BinaryReader( stream );
            BinarySerializer.DeserializeFields( reader, o );
        }

        /// <summary>Unmarshals a data object from a byte array</summary>
        /// <param name="o">Object to unmarshal the byte array into</param>
        /// <param name="buf">byte array containing Marshalled data to unmarshal</param>
        static public void DeSerialize( object o, byte[] buf )
        {
            MemoryStream stream = new MemoryStream( ( buf != null ) ? buf : new byte[ 1 ] );
            BinarySerializer.DeSerialize( o, stream );
        }

        /// <summary>Deserializes an object from a byte array</summary>
        /// <param name="buf">byte array containing serialized data for the object</param>
        /// <param name="T">type of the object to deserialize</param>
        /// <returns>new instance of the object deserialized from the byte array</returns>
        static public object DeSerialize(byte[] buf, Type T)
        {
            ConstructorInfo ci = T.GetConstructor(new Type[0]);
            if(ci == null)
                throw new ArgumentException("Type must have public default constructor to DeSerialize", T.Name);

            object o = ci.Invoke(null);
            DeSerialize(o, buf);
            return o;
        }

        /// <summary>DeSerializes an object from a stream</summary>
        /// <param name="S">Stream to deserialize from</param>
        /// <param name="T">type of object to deserializ</param>
        /// <returns>new instance of the object initialized with values from the stream</returns>
        static public object DeSerialize(Stream S, Type T)
        {
            ConstructorInfo ci = T.GetConstructor(new Type[0]);
            if(ci == null)
                throw new ArgumentException("Type must have public default constructor to DeSerialize", T.Name);

            object o = ci.Invoke(null);
            DeSerialize(o, S);
            return o;
        }

        #region Internal Deserialization support
        static private void DeserializeFields( BinaryReader reader, object o )
        {
            Type t = o.GetType( ); 
            if( t.IsArray )
                BinarySerializer.InternalDeserializeInstance( reader, o, t );
            else
                BinarySerializer.InternalDeserializeFields( reader, o, t );
        }

        static private void InternalDeserializeFields( BinaryReader reader, object o, Type t )
        {
            // if this is not a root move up to the root 
            // so that marshaling includes complete
            // inheritance chain
            if( t.BaseType != null )
                BinarySerializer.InternalDeserializeFields( reader, o, t.BaseType );

            // marshal individual fields
            foreach( FieldInfo info in t.GetFields( BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly ) )
            {
                if( info.FieldType.IsSerializable )
                {
                    Type fieldType = info.FieldType;
                    object obj2 = info.GetValue( o );
                    obj2 = BinarySerializer.InternalDeserializeInstance( reader, obj2, fieldType );
                    info.SetValue( o, obj2 );
                }
            }
        }

        static private object InternalDeserializeInstance( BinaryReader reader, object o, Type t )
        {
            if( o != null )
                t = o.GetType( );

            if( o != null && ( o.GetType( ) != t ) )
                throw new ArgumentException( "type 't' doesn't match provided object 'o'" );

            if( t == typeof( Boolean ) )
                return reader.ReadBoolean( );

            if( t == typeof( Char ) )
                return reader.ReadChar( );

            if( t == typeof( SByte ) )
                return reader.ReadSByte( );

            if( t == typeof( Byte ) )
                return reader.ReadByte( );

            if( t == typeof( Int16 ) )
                return reader.ReadInt16( );

            if( t == typeof( UInt16 ) )
                return reader.ReadUInt16( );

            if( t == typeof( Int32 ) )
                return reader.ReadInt32( );

            if( t == typeof( UInt32 ) )
                return reader.ReadUInt32( );

            if( t == typeof( Int64 ) )
                return reader.ReadInt64( );

            if( t == typeof( UInt64 ) )
                return reader.ReadUInt64( );
            //
            //        if(t==typeof(Single))
            //            return reader.ReadSingle();
            //
            //        if(t==typeof(Double))
            //            return reader.ReadDouble();

            if( t == typeof( String ) )
                return reader.ReadString( );

            // arrays introduce too many complications to support at this time
            // far to many border or special case considerations need to be accounted
            // for; the entire implementation of this class needs a re-design to 
            // do that adequately.
            if( t.IsArray )
                throw new ArgumentException( "Arrays are not supported for serialization" );

            if( !t.IsValueType && !t.IsClass )
                throw new ArgumentException( "unsupported type for parameter 'o'" );

            BinarySerializer.DeserializeFields( reader, o );
            return o;
        }
        #endregion

        #region Internal Serialization support
        static private void SerializeFields(BinaryWriter reader, object o)
        {
            Type t = o.GetType();
            if(t.IsArray)
                BinarySerializer.InternalSerializeInstance(reader, o, t);
            else
                BinarySerializer.InternalSerializeFields(reader, o, t);
        }

        static private void InternalSerializeFields( BinaryWriter writer, object o, Type T )
        {
            // if this is not a root move up to the root 
            // so that marshaling includes complete
            // inheritance chain
            if(T.BaseType != null)
                BinarySerializer.InternalSerializeInstance(writer, o, T.BaseType);

            if(T == typeof(string))
                BinarySerializer.InternalSerializeInstance(writer, o, T);
            else
            {
                // unmarshal individual fields
                foreach(FieldInfo info in T.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))
                {
                    if(info.FieldType.IsSerializable)
                        BinarySerializer.InternalSerializeInstance(writer, info.GetValue(o), info.FieldType);
                }
            }
        }

        static private void InternalSerializeInstance( BinaryWriter writer, object o, Type t )
        {
            if( t == typeof( Boolean ) )
            {
                writer.Write( ( Boolean )o );
                return;
            }

            if( t == typeof( Char ) )
            {
                writer.Write( ( Char )o );
                return;
            }

            if( t == typeof( SByte ) )
            {
                writer.Write( ( SByte )o );
                return;
            }

            if( t == typeof( Byte ) )
            {
                writer.Write( ( Byte )o );
                return;
            }

            if( t == typeof( Int16 ) )
            {
                writer.Write( ( Int16 )o );
                return;
            }

            if( t == typeof( UInt16 ) )
            {
                writer.Write( ( UInt16 )o );
                return;
            }

            if( t == typeof( Int32 ) )
            {
                writer.Write( ( Int32 )o );
                return;
            }

            if( t == typeof( UInt32 ) )
            {
                writer.Write( ( UInt32 )o );
                return;
            }

            if( t == typeof( Int64 ) )
            {
                writer.Write( ( Int64 )o );
                return;
            }

            if( t == typeof( UInt64 ) )
            {
                writer.Write( ( UInt64 )o );
                return;
            }

            //if (t == typeof(Single))
            //{
            //    writer.Write((Single)o);
            //    return;
            //}

            //if (t == typeof(Double))
            //{
            //    writer.Writer((Double)o);
            //    return;
            //}

            if( t == typeof( String ) )
            {
                if( o == null )
                    writer.Write( string.Empty );
                else
                    writer.Write( ( String )o );

                return;
            }

            // arrays introduce too many complications to support at this time
            // far to many border or special case considerations need to be accounted
            // for; the entire implementation of this class needs a re-design to 
            // do that adequately.
            if( t.IsArray )
                throw new ArgumentException( "Arrays are not supported for serialization" );

            if( !t.IsValueType && !t.IsClass )
                throw new ArgumentException( "unsupported type","o" );

            BinarySerializer.InternalSerializeFields(writer, o, t);
        }
        #endregion
    }
}