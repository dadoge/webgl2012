////////////////////////////////////////////////
// DESCRIPTION:
//    Bit field manipulation support
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

namespace FusionWare.SPOT
{
    #region Single bit mask enumerations
    /// <summary>Bit flags for 8 bit wide fields</summary>
    /// <remarks>
    /// This allows casting of the bit values directly to and from a byte
    /// </remarks>
    [Flags]
    public enum Bits8 : byte
    {
        /// <summary>Bit mask value for Bit position 0</summary>
        BIT_0 = 0x01,
        /// <summary>Bit mask value for Bit position 1</summary>
        BIT_1 = 0x02,
        /// <summary>Bit mask value for Bit position 2</summary>
        BIT_2 = 0x04,
        /// <summary>Bit mask value for Bit position 3</summary>
        BIT_3 = 0x08,
        /// <summary>Bit mask value for Bit position 4</summary>
        BIT_4 = 0x10,
        /// <summary>Bit mask value for Bit position 5</summary>
        BIT_5 = 0x20,
        /// <summary>Bit mask value for Bit position 6</summary>
        BIT_6 = 0x40,
        /// <summary>Bit mask value for Bit position 7</summary>
        BIT_7 = 0x80
    }

    /// <summary>Bit flags for 16 bit wide fields</summary>
    /// <remarks>
    /// This allows casting of the bit values directly to and from ushort
    /// </remarks>
    [Flags]
    public enum Bits16 : ushort
    {
        /// <summary>Bit mask value for Bit position 0</summary>
        BIT_0 = 0x0001,
        /// <summary>Bit mask value for Bit position 1</summary>
        BIT_1 = 0x0002,
        /// <summary>Bit mask value for Bit position 2</summary>
        BIT_2 = 0x0004,
        /// <summary>Bit mask value for Bit position 3</summary>
        BIT_3 = 0x0008,
        /// <summary>Bit mask value for Bit position 4</summary>
        BIT_4 = 0x0010,
        /// <summary>Bit mask value for Bit position 5</summary>
        BIT_5 = 0x0020,
        /// <summary>Bit mask value for Bit position 6</summary>
        BIT_6 = 0x0040,
        /// <summary>Bit mask value for Bit position 7</summary>
        BIT_7 = 0x0080,

        /// <summary>Bit mask value for Bit position 8</summary>
        BIT_8 = 0x0100,
        /// <summary>Bit mask value for Bit position 9</summary>
        BIT_9 = 0x0200,
        /// <summary>Bit mask value for Bit position 10</summary>
        BIT_10 = 0x0400,
        /// <summary>Bit mask value for Bit position 11</summary>
        BIT_11 = 0x0800,
        /// <summary>Bit mask value for Bit position 12</summary>
        BIT_12 = 0x1000,
        /// <summary>Bit mask value for Bit position 13</summary>
        BIT_13 = 0x2000,
        /// <summary>Bit mask value for Bit position 14</summary>
        BIT_14 = 0x4000,
        /// <summary>Bit mask value for Bit position 15</summary>
        BIT_15 = 0x8000,
    }

    /// <summary>Bit flags for 32 bit wide fields</summary>
    /// <remarks>
    /// This allows casting of the bit values directly to and from uint
    /// </remarks>
    [Flags]
    public enum Bits32 : uint
    {
        /// <summary>Bit mask value for Bit position 0</summary>
        BIT_0 = 0x00000001,
        /// <summary>Bit mask value for Bit position 1</summary>
        BIT_1 = 0x00000002,
        /// <summary>Bit mask value for Bit position 2</summary>
        BIT_2 = 0x00000004,
        /// <summary>Bit mask value for Bit position 3</summary>
        BIT_3 = 0x00000008,
        /// <summary>Bit mask value for Bit position 4</summary>
        BIT_4 = 0x00000010,
        /// <summary>Bit mask value for Bit position 5</summary>
        BIT_5 = 0x00000020,
        /// <summary>Bit mask value for Bit position 6</summary>
        BIT_6 = 0x00000040,
        /// <summary>Bit mask value for Bit position 7</summary>
        BIT_7 = 0x00000080,

        /// <summary>Bit mask value for Bit position 8</summary>
        BIT_8 = 0x00000100,
        /// <summary>Bit mask value for Bit position 9</summary>
        BIT_9 = 0x00000200,
        /// <summary>Bit mask value for Bit position 10</summary>
        BIT_10 = 0x00000400,
        /// <summary>Bit mask value for Bit position 11</summary>
        BIT_11 = 0x00000800,
        /// <summary>Bit mask value for Bit position 12</summary>
        BIT_12 = 0x00001000,
        /// <summary>Bit mask value for Bit position 13</summary>
        BIT_13 = 0x00002000,
        /// <summary>Bit mask value for Bit position 14</summary>
        BIT_14 = 0x00004000,
        /// <summary>Bit mask value for Bit position 15</summary>
        BIT_15 = 0x00008000,

        /// <summary>Bit mask value for Bit position 16</summary>
        BIT_16 = 0x00010000,
        /// <summary>Bit mask value for Bit position 17</summary>
        BIT_17 = 0x00020000,
        /// <summary>Bit mask value for Bit position 18</summary>
        BIT_18 = 0x00040000,
        /// <summary>Bit mask value for Bit position 19</summary>
        BIT_19 = 0x00080000,
        /// <summary>Bit mask value for Bit position 20</summary>
        BIT_20 = 0x00100000,
        /// <summary>Bit mask value for Bit position 21</summary>
        BIT_21 = 0x00200000,
        /// <summary>Bit mask value for Bit position 22</summary>
        BIT_22 = 0x00400000,
        /// <summary>Bit mask value for Bit position 23</summary>
        BIT_23 = 0x00800000,

        /// <summary>Bit mask value for Bit position 24</summary>
        BIT_24 = 0x01000000,
        /// <summary>Bit mask value for Bit position 25</summary>
        BIT_25 = 0x02000000,
        /// <summary>Bit mask value for Bit position 26</summary>
        BIT_26 = 0x04000000,
        /// <summary>Bit mask value for Bit position 27</summary>
        BIT_27 = 0x08000000,
        /// <summary>Bit mask value for Bit position 28</summary>
        BIT_28 = 0x10000000,
        /// <summary>Bit mask value for Bit position 29</summary>
        BIT_29 = 0x20000000,
        /// <summary>Bit mask value for Bit position 30</summary>
        BIT_30 = 0x40000000,
        /// <summary>Bit mask value for Bit position 31</summary>
        BIT_31 = 0x80000000,
    }
    #endregion

    /// <summary>Stores information about a bit field for extraction from a value</summary>
    /// <remarks>
    /// <para>C# doesn't support bit fields on structs so this is used to provide access to 
    /// bit fields in integral primative types. Most SPI and I2C devices have a set of
    /// registers with a varety of bit fields. This class helps make accessing registers
    /// simpler.</para>
    /// <para>Typical code would use a variety of bit mask and shifting techniques scattered
    /// around hiding inside the code. The problem with this type of code is that it is
    /// error prone and hides the actual intent. While adding a comment in the code to
    /// explain the intent solves one part of that it actually makes the other worse. A
    /// comment about a basic operation like that leaves the reader accepting the code does
    /// what the comment says without question. Using this class in conjunction with a device
    /// specific managed driver keeps code clear, simpler and self documenting.</para> 
    /// </remarks>
    public struct BitFieldMask
    {
        /// <summary>Inclusive starting bit position for the Field</summary>
        public int StartBit;
        
        /// <summary>Inclusive ending bit position for the field</summary>
        public int EndBit;

        /// <summary>Creates a new BitFieldMask</summary>
        /// <param name="StartBit">Starting Bit position of the field</param>
        /// <param name="EndBit">Ending Bit position of the field</param>
        public BitFieldMask(int StartBit, int EndBit)
        {
            this.StartBit = StartBit;
            this.EndBit = EndBit;
        }

        /// <summary>Gets the Length, in bits, of the field</summary>
        /// <value>Length of the field (in bits)</value>
        public int BitLength
        {
            get { return EndBit - StartBit + 1; }
        }

        /// <summary>Extracts the fields bits from a value</summary>
        /// <param name="Value">Value to extract the bits from</param>
        /// <returns>Extracted bits right shifted</returns>
        /// <remarks>
        /// This method uses the StartBit and EndBit to mask
        /// and right shift the bits of the field and returns the result.
        /// </remarks>
        public byte GetBitField(byte Value)
        {
            return Bits.GetBitField(Value, this.StartBit, this.BitLength);
        }

        /// <summary>Sets the bits of the field in a value</summary>
        /// <param name="Value">Value to set the bit fields for</param>
        /// <param name="NewFieldValue">new value for the field</param>
        /// <remarks>
        /// This method will set the bits defined by the field
        /// (StartBit to EndBit) of Value. The relevant bits of Value
        /// are cleared and then set based on NewFieldValue
        /// </remarks>
        public void SetBitField(ref byte Value, byte NewFieldValue)
        {
            Bits.SetBitField(ref Value, this.StartBit, this.BitLength, NewFieldValue);
        }

        /// <summary>Extracts the fields bits from a value</summary>
        /// <param name="Value">Value to extract the bits from</param>
        /// <returns>Extracted bits right shifted</returns>
        /// <remarks>
        /// This method uses the StartBit and EndBit to mask
        /// and right shift the bits of the field and returns the result.
        /// </remarks>
        public ushort GetBitField(ushort Value)
        {
            return Bits.GetBitField( Value, this.StartBit, this.BitLength );
        }

        /// <summary>Sets the bits of the field in a value</summary>
        /// <param name="Value">Value to set the bit fields for</param>
        /// <param name="NewFieldValue">new value for the field</param>
        /// <remarks>
        /// This method will set the bits defined by the field
        /// (StartBit to EndBit) of Value. The relevant bits of Value
        /// are cleared and then set based on NewFieldValue
        /// </remarks>
        public void SetBitField(ref ushort Value, ushort NewFieldValue)
        {
            Bits.SetBitField(ref Value, this.StartBit, this.BitLength, NewFieldValue);
        }

        /// <summary>Extracts the fields bits from a value</summary>
        /// <param name="Value">Value to extract the bits from</param>
        /// <returns>Extracted bits right shifted</returns>
        /// <remarks>
        /// This method uses the StartBit and EndBit to mask
        /// and right shift the bits of the field and returns the result.
        /// </remarks>
        public uint GetBitField(uint Value)
        {
            return Bits.GetBitField( Value, this.StartBit, this.BitLength );
        }

        /// <summary>Sets the bits of the field in a value</summary>
        /// <param name="Value">Value to set the bit fields for</param>
        /// <param name="NewFieldValue">new value for the field</param>
        /// <remarks>
        /// This method will set the bits defined by the field
        /// (StartBit to EndBit) of Value. The relevant bits of Value
        /// are cleared and then set based on NewFieldValue
        /// </remarks>
        public void SetBitField(ref uint Value, uint NewFieldValue)
        {
            Bits.SetBitField(ref Value, this.StartBit, this.BitLength, NewFieldValue);
        }
    }
    
    /// <summary>Static class providing utility methods for manipulating bits in a bit field</summary>
    /// <remarks>
    /// <para>This class provides utility functions for manipulating and testing bits in 
    /// a bit field that is a primitive integer type.</para>
    /// <para>Typical code would use a variety of bit mask and shifting techniques scattered
    /// around hiding inside the code. The problem with this type of code is that it is
    /// error prone and hides the actual intent. While adding a comment in the code to
    /// explain the intent solves one part of that it actually makes the other worse. A
    /// comment about a basic operation like that leaves the reader accepting the code does
    /// what the comment says without question. Using this class in conjunction with a device
    /// specific managed driver keeps code clear, simpler and self documenting.</para> 
    /// </remarks>
    public static class Bits
    {
        #region 8Bit Methods
        
        /// <summary>Modifies bits in a byte value</summary>
        /// <param name="Value">value to modify</param>
        /// <param name="ClearBits">Bit mask of bits in value to clear</param>
        /// <param name="SetBits">Bit mask of bits in value to set</param>
        public static void ModifyBits(ref byte Value, Bits8 ClearBits, Bits8 SetBits)
        {
            Value = ( byte )( ( Value & ( byte )( ~ClearBits ) ) | ( byte )SetBits );
        }

        /// <summary>Tests if all the specified bits are set in a Value</summary>
        /// <param name="Value">Value to test</param>
        /// <param name="TestBits">Bit mask of bits to test in Value</param>
        /// <returns>true, if ALL the bits specified in TestBits are set</returns>
        public static bool AllBitsSet(byte Value, Bits8 TestBits)
        {
            return (byte)TestBits == ((byte)TestBits & Value);
        }

        /// <summary>Tests if any of the bits specified are set in a value</summary>
        /// <param name="Value">Value to test bits in</param>
        /// <param name="TestBits">Bit mask of bits to test in Value</param>
        /// <returns>true if any of the bits specified in TestBits is set in Value</returns>
        public static bool AnyBitsSet(byte Value, Bits8 TestBits)
        {
            return 0 != ((byte)TestBits & Value);
        }

        /// <summary>Extracts a bit field from a byte</summary>
        /// <param name="Value">byte containing the bit field</param>
        /// <param name="StartBit">Starting bit position of the field</param>
        /// <param name="BitLength">Length of the bit field</param>
        /// <returns>Bit field extracted from Value and right shifted</returns>
        /// <remarks>
        /// This method will extract the value of a bit field from within a byte.
        /// This is useful for extracting bit fields from register values 
        /// in managed drivers. Although it is by no means limited to that context.
        /// </remarks>
        public static byte GetBitField(byte Value, int StartBit, int BitLength)
        {
            if(StartBit > 7 || ((StartBit + BitLength - 1) > 7))
                throw new ArgumentException();

            byte valMask = (byte)((1 << BitLength) - 1);
            return (byte)((Value >> StartBit) & valMask);
        }

        /// <summary>Set the bits of a bit field in a byte</summary>
        /// <param name="Value">Value to set the filed in</param>
        /// <param name="StartBit">Starting bit position of the field</param>
        /// <param name="BitLength">Length of the field</param>
        /// <param name="NewFieldValue">New value for the field</param>
        /// <remarks>
        /// THis method will set the bits of a bit field in the specified value. 
        /// This is useful for manipulating bit fields for register values 
        /// in managed drivers. Although it is by no means limited to that context.
        /// </remarks>
        public static void SetBitField(ref byte Value, int StartBit, int BitLength, byte NewFieldValue)
        {
            if(StartBit > 7 || ((StartBit + BitLength -1 ) > 7))
                throw new ArgumentException();

            byte valMask = (byte)(((1 << BitLength) - 1) << StartBit);
            Bits.ModifyBits(ref Value, (Bits8)valMask, (Bits8)(valMask & (NewFieldValue << StartBit)));
        }

        #endregion

        #region 16bit Methods
        /// <summary>Modifies bits in a 16bit value</summary>
        /// <param name="Value">value to modify</param>
        /// <param name="ClearBits">Bit mask of bits in value to clear</param>
        /// <param name="SetBits">Bit mask of bits in value to set</param>
        public static void ModifyBits(ref ushort Value, Bits16 ClearBits, Bits16 SetBits)
        {
            Value = ( ushort )( ( Value & ( ushort )( ~ClearBits ) ) | ( ushort )SetBits );
        }

        /// <summary>Tests if all the specified bits are set in a Value</summary>
        /// <param name="Value">Value to test</param>
        /// <param name="TestBits">Bit mask of bits to test in Value</param>
        /// <returns>true, if ALL the bits specified in TestBits are set</returns>
        public static bool AllBitsSet(ushort Value, Bits16 TestBits)
        {
            return (ushort)TestBits == ((ushort)TestBits & Value);
        }

        /// <summary>Tests if any of the bits specified are set in a value</summary>
        /// <param name="Value">Value to test bits in</param>
        /// <param name="TestBits">Bit mask of bits to test in Value</param>
        /// <returns>true if any of the bits specified in TestBits is set in Value</returns>
        public static bool AnyBitsSet(ushort Value, Bits16 TestBits)
        {
            return 0 != ((ushort)TestBits & Value);
        }

        /// <summary>Extracts a bit field from a ushort value</summary>
        /// <param name="Value">ushort value containing the bit field</param>
        /// <param name="StartBit">Starting bit position of the field</param>
        /// <param name="BitLength">Length of the bit field</param>
        /// <returns>Bit field extracted from Value and right shifted</returns>
        /// <remarks>
        /// This method will extract the value of a bit field from within a byte.
        /// This is useful for extracting bit fields from register values 
        /// in managed drivers. Although it is by no means limited to that context.
        /// </remarks>
        public static ushort GetBitField(ushort Value, int StartBit, int BitLength)
        {
            if(StartBit > 15 || ((StartBit + BitLength -1) > 15))
                throw new ArgumentException();

            ushort valMask = ( ushort )( ( 1 << BitLength ) - 1 );
            return (ushort)((Value >> StartBit) & valMask);
        }

        /// <summary>Set the bits of a bit field in a ushort value</summary>
        /// <param name="Value">Value to set the filed in</param>
        /// <param name="StartBit">Starting bit position of the field</param>
        /// <param name="BitLength">Length of the field</param>
        /// <param name="NewFieldValue">New value for the field</param>
        /// <remarks>
        /// This method will set the bits of a bit field in the specified value. 
        /// This is useful for manipulating bit fields for register values 
        /// in managed drivers. Although it is by no means limited to that context.
        /// </remarks>
        public static void SetBitField(ref ushort Value, int StartBit, int BitLength, ushort NewFieldValue)
        {
            if(StartBit > 15 || ((StartBit + BitLength - 1) > 15))
                throw new ArgumentException();

            ushort valMask = (ushort)(((1 << BitLength) - 1) << StartBit);
            ModifyBits(ref Value, (Bits16)valMask, (Bits16)(valMask & (NewFieldValue << StartBit)));
        }

        #endregion

        #region 32bit Methods
        /// <summary>Modifies bits in a 32bit value</summary>
        /// <param name="Value">value to modify</param>
        /// <param name="ClearBits">Bit mask of bits in value to clear</param>
        /// <param name="SetBits">Bit mask of bits in value to set</param>
        public static void ModifyBits(ref uint Value, Bits32 ClearBits, Bits32 SetBits)
        {
            Value = ( Value & ( uint )( ~ClearBits ) ) | ( uint )SetBits;
        }

        /// <summary>Modifies bits in a 32bit value</summary>
        /// <param name="Value">value to modify</param>
        /// <param name="ClearBits">Bit mask of bits in value to clear</param>
        /// <param name="SetBits">Bit mask of bits in value to set</param>
        public static void ModifyBits(ref int Value, Bits32 ClearBits, Bits32 SetBits)
        {
            Value = ( Value & ( int )( ~ClearBits ) ) | ( int )SetBits;
        }

        /// <summary>Tests if all the specified bits are set in a Value</summary>
        /// <param name="Value">Value to test</param>
        /// <param name="TestBits">Bit mask of bits to test in Value</param>
        /// <returns>true, if ALL the bits specified in TestBits are set</returns>
        public static bool AllBitsSet(uint Value, Bits32 TestBits)
        {
            return (uint)TestBits == ((uint)TestBits & Value);
        }

        /// <summary>Tests if any of the bits specified are set in a value</summary>
        /// <param name="Value">Value to test bits in</param>
        /// <param name="TestBits">Bit mask of bits to test in Value</param>
        /// <returns>true if any of the bits specified in TestBits is set in Value</returns>
        public static bool AnyBitsSet(uint Value, Bits32 TestBits)
        {
            return 0 != ((uint)TestBits & Value);
        }

        /// <summary>Extracts a bit field from a 32 bit value</summary>
        /// <param name="Value">ushort value containing the bit field</param>
        /// <param name="StartBit">Starting bit position of the field</param>
        /// <param name="BitLength">Length of the bit field</param>
        /// <returns>Bit field extracted from Value and right shifted</returns>
        /// <remarks>
        /// This method will extract the value of a bit field from within a byte.
        /// This is useful for extracting bit fields from register values 
        /// in managed drivers. Although it is by no means limited to that context.
        /// </remarks>
        public static uint GetBitField(uint Value, int StartBit, int BitLength)
        {
            if(StartBit > 31 || ((StartBit + BitLength - 1) > 31))
                throw new ArgumentException();

            uint valMask = (uint)((1 << BitLength) - 1);
            return (Value >> StartBit) & valMask;
        }

        /// <summary>Sets a bit field in a 32 bit value</summary>
        /// <param name="Value">value containing the bit field</param>
        /// <param name="StartBit">Starting bit position of the field</param>
        /// <param name="BitLength">Length of the bit field</param>
        /// <param name="NewFieldValue">New value for the bit field</param>
        /// <remarks>
        /// This method will set the value of a bit field within a 32 bit value.
        /// You do not need to mask or shift NewFieldValue as that is done
        /// internally in this function. This is useful for setting bit fields
        /// from register values in managed drivers. Although it is by no means
        /// limited to that context.
        /// </remarks>
        public static void SetBitField(ref uint Value, int StartBit, int BitLength, uint NewFieldValue)
        {
            if(StartBit > 31 || ((StartBit + BitLength - 1) > 31))
                throw new ArgumentException();

            uint valMask = (uint)((1 << BitLength) - 1) << StartBit;
            ModifyBits(ref Value, (Bits32)valMask, (Bits32)(valMask & (NewFieldValue << StartBit)));
        }

        ///<summary>Performs an Atomic modification of bit flags</summary>
        /// <param name="Value">Value to modify</param>
        /// <param name="ClearBits">Bits to clear in Value</param>
        /// <param name="SetBits">Bits to Set in Value</param>
        /// <returns>previous value</returns>
        /// <remarks>
        /// This method performs a thread safe interlocked atomic modification of the
        /// specified value.
        /// </remarks>
        static int InterlockedModifyBits(ref int Value, Bits32 ClearBits, Bits32 SetBits)
        {
            int original, newVal;
            do
            {
                newVal = original = Value;
                Bits.ModifyBits(ref newVal, ClearBits, SetBits);
                // if the correct bits are already set/cleared then do nothing
                if(newVal == original)
                    break;

                // use InterlockedCopmareExchange to make a thread safe assignment or try again
            } while(System.Threading.Interlocked.CompareExchange(ref Value, newVal, original) != original);
            return original;
        }

        #endregion
    }
}
