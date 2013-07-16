////////////////////////////////////////////////
// DESCRIPTION:
//   General Bitmap utility support 
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
using FusionWare.SPOT.IO;
using Microsoft.SPOT.Presentation.Media;
using System.Runtime.InteropServices;

namespace FusionWare.SPOT
{
    /// <summary>
    /// This static class provides a number of useful bitmap related support functions
    /// that are otherwise missing from the basic Microsoft .NET Micro Framework
    /// libraries.
    /// </summary>
    /// <remarks>
    /// The .NET Micro Framework provides only minimal support for bitmaps. This class
    /// provides some additional functionality needed for a modern UI.
    /// </remarks>
    public static class BitmapHelper
    {
        /// <summary>Selects a random image form an array of bitmaps</summary>
        /// <param name="Images">Array of bitmaps to choose from</param>
        /// <returns>Randomly selected bitmap from the Images array</returns>
        public static Bitmap RandomImage(Bitmap[] Images)
        {
            Random r = new Random();
            return Images[ r.Next( Images.Length -1 ) ];
        }
        
        [StructLayout(LayoutKind.Sequential, Pack=1, Size=40)]
        private struct BITMAPINFOHEADER
        {
            public uint   biSize;
            public int    biWidth;
            public int    biHeight;
            public ushort biPlanes;
            public ushort biBitCount;
            public uint   biCompression;
            public uint   biSizeImage;
            public int    biXPelsPerMeter;
            public int    biYPelsPerMeter;
            public uint   biClrUsed;
            public uint   biClrImportant;

            public bool IsTransparent
            {
                get
                {
                    return (biPlanes == 1) && (biCompression == 0) && (biBitCount == 32);
                }
            }

            public bool Read(BinaryReader Reader)
            {
                this.biSize = Reader.ReadUInt32();
                if (this.biSize != BITMAPINFOHEADER.ByteSize)
                    return false;

                this.biWidth = Reader.ReadInt32();
                this.biHeight = Reader.ReadInt32();
                this.biPlanes = Reader.ReadUInt16();
                this.biBitCount = Reader.ReadUInt16();
                this.biCompression = Reader.ReadUInt32();
                this.biSizeImage = Reader.ReadUInt32();
                this.biXPelsPerMeter = Reader.ReadInt32();
                this.biYPelsPerMeter = Reader.ReadInt32();
                this.biClrUsed = Reader.ReadUInt32();
                this.biClrImportant = Reader.ReadUInt32();
                return true;
            }

            public const int ByteSize = 40;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 14)]
        private class BITMAPFILEHEADER
        { 
            public ushort bfType; 
            public uint   bfSize; 
            public ushort bfReserved1; 
            public ushort bfReserved2; 
            public uint   bfOffBits;

            public bool Read(BinaryReader Reader)
            {
                this.bfType = Reader.ReadUInt16();
                if (this.bfType != bfTypeBM)
                    return false;

                this.bfSize = Reader.ReadUInt32();
                this.bfReserved1 = Reader.ReadUInt16();
                this.bfReserved2 = Reader.ReadUInt16();
                this.bfOffBits = Reader.ReadUInt32();
                return true;
            }
            public const int ByteSize = 14;
            private const short bfTypeBM = 0x4d42; //'BM'
        }

        /// <summary>Converts an array of bytes to a Bitmap object</summary>
        /// <param name="bytes">
        /// Bytes for the bitmap, typically read from a resource. This is the complete bitmap
        /// structure starting with the BITMAPFILEHEADER, etc... as appropriate for the image
        /// file type.
        /// </param>
        /// <remarks>
        /// Converts an array of bytes, typically retrieved from a resource, into a Bitmap
        /// instance. The bytes are scanned to determine the correct type of bitmap to 
        /// create (Windows Bitmap, GIF, JPEG)
        /// </remarks>
        /// <returns>Converted Bitmap</returns>
        public static Bitmap ByteArrayToBitmap(byte[] bytes)
        {
            Bitmap.BitmapImageType imageType = Bitmap.BitmapImageType.Bmp;

            if (bytes == null)
            {
                throw new ArgumentNullException("bytes");
            }
            int len = bytes.Length;

            // test for BMP file signature ('BM')
            if(((len < 2) || (bytes[0] != 0x42)) || (bytes[1] != 0x4d))
            {
                // Check for GIF signature
                if(((len < 3) || (bytes[0] != 0x47)) || ((bytes[1] != 0x49) || (bytes[2] != 70)))
                {
                    // Check for JPG signature
                    if((((len < 10) || (bytes[6] != 0x4a)) || ((bytes[7] != 70) || (bytes[8] != 0x49))) || (bytes[9] != 70))
                    {
                        throw new ArgumentException("Invalid image type");
                    }
                    imageType = Bitmap.BitmapImageType.Jpeg;
                }
                imageType = Bitmap.BitmapImageType.Gif;
            }
            return new Bitmap(bytes, imageType);
        }

        /// <summary>
        /// Composites a foreground image on top of a background image applying any alpha
        /// channel data from the foreground image.
        /// </summary>
        /// <returns>A new Bitmap containing the composition</returns>
        /// <remarks>
        /// This method combines the 2 images into a single composition. if the foreground
        /// image contains alpha channel information (32Bpp) it is used to create a blended
        /// composite image.
        /// </remarks>
        /// <param name="bitmapData">bitmap file data for the foreground image</param>
        /// <param name="background">bitmap data for the background image</param>
        public static Bitmap CompositeOnBackground(byte[] bitmapData, byte[] background)
        {
            uint bitsOffset;
            uint imageSize;
            bool isTransparent;

            BitmapHelper.GetBitmapInfo(bitmapData, out bitsOffset, out imageSize, out isTransparent);
            System.Diagnostics.Debug.Assert(bitsOffset == (BITMAPINFOHEADER.ByteSize + BITMAPFILEHEADER.ByteSize));
            if(!isTransparent)
                return ByteArrayToBitmap(bitmapData);

            int srcPixelIndex = (int)bitsOffset;
            int endPixelIndex = srcPixelIndex + (int)(imageSize * 4);
            for(int dstPixelIndex = (int)bitsOffset; srcPixelIndex < endPixelIndex; dstPixelIndex += 3)
            {
                int alphaChannel = bitmapData[srcPixelIndex + 3];
                if (alphaChannel == 0xff)
                {
                    Array.Copy(bitmapData, srcPixelIndex, background, dstPixelIndex, 3);
                }
                else
                    if (alphaChannel > 0)
                    {
                        int blend = 0xff - alphaChannel;
                        background[dstPixelIndex] = (byte) (((background[dstPixelIndex] * blend) + (bitmapData[srcPixelIndex] * alphaChannel)) >> 8);
                        background[dstPixelIndex + 1] = (byte) (((background[dstPixelIndex + 1] * blend) + (bitmapData[srcPixelIndex + 1] * alphaChannel)) >> 8);
                        background[dstPixelIndex + 2] = (byte) (((background[dstPixelIndex + 2] * blend) + (bitmapData[srcPixelIndex + 2] * alphaChannel)) >> 8);
                    }
                srcPixelIndex += 4;
            }
            return ByteArrayToBitmap(background);
        }

        /// <summary>
        /// Performs a composition with a solid color background applying alpha channel data
        /// from the image as appropriate.
        /// </summary>
        /// <remarks>
        /// This method will composite the foreground image on to the background. If the
        /// foreground image contains alpha channel information it is used to create the blended
        /// image.
        /// </remarks>
        /// <returns>Composite image Bitmap</returns>
        /// <keywords>Bitmap|Alpha Blend</keywords>
        /// <param name="bitmapData">
        /// bitmap file data for the foreground image. If the bitmap contains alpha channel
        /// data (32bpp) then that information is used to create an alpha blended composite against
        /// the solid color background.
        /// </param>
        /// <param name="color">Color for the background</param>
        public static Bitmap CompositeOnColor(byte[] bitmapData, Color color)
        {
            uint bitsOffset;
            uint imageSize;
            bool isTransparent;

            BitmapHelper.GetBitmapInfo(bitmapData, out bitsOffset, out imageSize, out isTransparent);
            System.Diagnostics.Debug.Assert(bitsOffset == BITMAPFILEHEADER.ByteSize + BITMAPINFOHEADER.ByteSize);
            if(!isTransparent)
                return ByteArrayToBitmap(bitmapData);

            byte[] imgData = new byte[bitsOffset + (imageSize * 4)];
            Array.Copy(bitmapData, 0, imgData, 0, (int)bitsOffset);
            
            // set the image data as 24bpp
            imgData[BitCountOffset] = 24;

            byte bkgRedVal = ColorUtility.GetRValue(color);
            byte bkgGreenVal = ColorUtility.GetGValue(color);
            byte bkgBlueVal = ColorUtility.GetBValue(color);

            int srcPixelIndex = (int)bitsOffset;
            int endPixelIndex = srcPixelIndex + (int)(imageSize * 4);
            for(int dstPixelIndex = (int)bitsOffset; srcPixelIndex < endPixelIndex; dstPixelIndex += 3)
            {
                int alphaVal = bitmapData[srcPixelIndex + 3];
                switch (alphaVal)
                {
                    case 0:
                        imgData[dstPixelIndex] = bkgRedVal;
                        imgData[dstPixelIndex + 1] = bkgGreenVal;
                        imgData[dstPixelIndex + 2] = bkgBlueVal;
                        break;

                    case 0xff:
                        Array.Copy(bitmapData, srcPixelIndex, imgData, dstPixelIndex, 3);
                        break;

                    default:
                    {
                        int blend = 0xff - alphaVal;
                        imgData[dstPixelIndex] = (byte) (((bkgRedVal * blend) + (bitmapData[srcPixelIndex] * alphaVal)) >> 8);
                        imgData[dstPixelIndex + 1] = (byte) (((bkgGreenVal * blend) + (bitmapData[srcPixelIndex + 1] * alphaVal)) >> 8);
                        imgData[dstPixelIndex + 2] = (byte) (((bkgBlueVal * blend) + (bitmapData[srcPixelIndex + 2] * alphaVal)) >> 8);
                        break;
                    }
                }
                srcPixelIndex += 4;
            }
            return ByteArrayToBitmap(imgData);
        }

        // parses a BITMAPFILEHEADER and BITMAPINFOHEADER to get
        // the offset of the image bits, the size of the image and
        // if it's a transparent bitmap.
        private static void GetBitmapInfo(byte[] data, out uint BitsOffset, out uint ImageSize, out bool IsTransparent)
        {
            BitsOffset = 0;
            ImageSize = 0;
            IsTransparent = false;

            using(BinaryReader reader = new BinaryReader(new MemoryStream(data)))
            {
                BITMAPFILEHEADER fileHeader = new BITMAPFILEHEADER();
                BITMAPINFOHEADER infoHeader = new BITMAPINFOHEADER();
                if(!fileHeader.Read(reader))
                    return;

                if(!infoHeader.Read(reader))
                    return;

                BitsOffset = fileHeader.bfOffBits;
                ImageSize = (uint)(System.Math.Abs(infoHeader.biWidth) * System.Math.Abs(infoHeader.biHeight));
                IsTransparent = infoHeader.IsTransparent;
            }
        }

        private const int BitCountOffset = 0x1c;
        private const int FileHeaderSize = 14;
        private const int HeaderSize = 40;
    }
}

