using System;
using System.Diagnostics;
using System.IO;

namespace Assignment4.Image
{
    public sealed class Bitmap
    {
        // CONSTS
        private const int BI_BITFIELDS = 0x03;
        private const uint PIXELS_PER_METER = 2834; // 72 DPI
        private const uint LCS_GM_IMAGES = 4;
        private const uint PIXELDATA_START_OFFSET = 14 + 124;
        private const uint DIB_HEADER_SIZE = 124;
        private const uint RED_MASK = 0x00FF0000;
        private const uint GREEN_MASK = 0x0000FF00;
        private const uint BLUE_MASK = 0x000000FF;
        private const uint ALPHA_MASK = 0xFF000000;
        private const uint COLOR_SPACE = 0x73524742;    // sRGB

        // PROPERTIES
        public int Width { get; private set; }

        public int Height { get; private set; }

        // MEMBER VARIABLES
        private byte[] mPixelData;  // rgbx

        public Bitmap(string filePath)
        {
            byte[] data = File.ReadAllBytes(filePath);

            using (BinaryReader reader = new BinaryReader(new MemoryStream(data)))
            {
                // 1. Bitmap file header
                // BM (2 bytes)
                char c = reader.ReadChar();
                Debug.Assert(c == 'B');
                c = reader.ReadChar();
                Debug.Assert(c == 'M');

                // file size (4 bytes)
                uint fileSize = reader.ReadUInt32();
                Debug.Assert(fileSize == data.Length);

                // reserved (4 bytes)
                reader.ReadUInt32();

                // pixel start offset (4 bytes)
                uint startOffset = reader.ReadUInt32();
                Debug.Assert(startOffset == PIXELDATA_START_OFFSET);

                // 2. DIB header (only supports BITMAPV5HEADER)
                // header size (4 bytes)
                uint dibHeaderSize = reader.ReadUInt32();
                Debug.Assert(dibHeaderSize == BitmapHeader.DibHeaderSize);

                // width (4 bytes)
                Width = reader.ReadInt32();
                Debug.Assert(Width > 0);

                // height (4 bytes)
                Height = reader.ReadInt32();
                Debug.Assert(Height > 0);

                // # planes (2 bytes), must be 1
                ushort numPlanes = reader.ReadUInt16();
                Debug.Assert(numPlanes == 1);

                // bit count (2 bytes), we only support 32
                ushort bitCount = reader.ReadUInt16();
                Debug.Assert(bitCount == 32);

                // compression (4 bytes), must be BI_BITFIELDS
                uint compression = reader.ReadUInt32();
                Debug.Assert(compression == BI_BITFIELDS);

                // image size (4 bytes)
                uint imageSize = reader.ReadUInt32();
                Debug.Assert(Width * Height * 4 == imageSize);

                // horizontal pixel resolution (4 bytes)
                uint xPixelsPerMeter = reader.ReadUInt32();

                // vertical pixel resolution (4 bytes)
                uint yPixelsPerMeter = reader.ReadUInt32();

                // 4 bytes always 0
                uint colorIndexCount = reader.ReadUInt32();
                Debug.Assert(colorIndexCount == 0);

                // 4 bytes always 0
                uint reuqiredColorIndexCount = reader.ReadUInt32();
                Debug.Assert(reuqiredColorIndexCount == 0);

                // 4 bytes, red channel position  (only support XRGB)
                uint redMask = reader.ReadUInt32();
                Debug.Assert(redMask == RED_MASK);

                // 4 bytes
                uint greenMask = reader.ReadUInt32();
                Debug.Assert(greenMask == GREEN_MASK);

                // 4 bytes
                uint blueMask = reader.ReadUInt32();
                Debug.Assert(blueMask == BLUE_MASK);

                // 4 bytes
                uint alphaMask = reader.ReadUInt32();
                Debug.Assert(alphaMask == ALPHA_MASK);

                // 4 bytes colorspace (sRGB only)
                uint colorSpace = reader.ReadUInt32();
                Debug.Assert(colorSpace == COLOR_SPACE);

                // 36 bytes. ciexyztriple
                reader.ReadBytes(36);

                // red gamma (4 bytes)
                uint redGamma = reader.ReadUInt32();
                Debug.Assert(redGamma == 0);

                // green gamma (4 bytes)
                uint greenGamma = reader.ReadUInt32();
                Debug.Assert(greenGamma == 0);

                // blue gamma (4 bytes)
                uint blueGamma = reader.ReadUInt32();
                Debug.Assert(blueGamma == 0);

                // intent (4 bytes)
                uint intent = reader.ReadUInt32();
                Debug.Assert(intent == LCS_GM_IMAGES);

                // profile data (4 bytes)
                uint profileData = reader.ReadUInt32();
                Debug.Assert(profileData == 0);

                // profile size (4 bytes)
                uint profileSize = reader.ReadUInt32();
                Debug.Assert(profileSize == 0);

                // reserved (4 bytes)
                reader.ReadUInt32();

                mPixelData = new byte[Width * Height * 4];
                Array.Copy(data, startOffset, mPixelData, 0, mPixelData.Length);
            }
        }

        public Bitmap(int width, int height)
        {
            Width = width;
            Height = height;

            mPixelData = new byte[width * height * 4];

            // opaque black
            for (int a = 3; a < width * height * 4; a += 4)
            {
                mPixelData[a] = 0xFF;
            }
        }

        public Color GetPixel(int x, int y)
        {
            Debug.Assert(x >= 0 && y >= 0);

            int offset = (y * Width + x) * 4;
            byte r = mPixelData[offset++];
            byte g = mPixelData[offset++];
            byte b = mPixelData[offset];

            return new Color(r, g, b);
        }

        public void SetPixel(int x, int y, Color pixel)
        {
            Debug.Assert(x >= 0 && y >= 0);

            int offset = (y * Width + x) * 4;
            mPixelData[offset++] = pixel.R;
            mPixelData[offset++] = pixel.G;
            mPixelData[offset] = pixel.B;
        }

        public void Save(string filePath)
        {
            byte[] data = new byte[mPixelData.Length + BitmapHeader.SIZE];

            using (BinaryWriter writer = new BinaryWriter(new MemoryStream(data)))
            {
                BitmapHeader.SaveHeader(writer, this);
                writer.Write(mPixelData);
            }

            File.WriteAllBytes(filePath, data);
        }

        /// <summary>
        /// Bitmap file header + BITMAPV5HEADER
        /// </summary>
        public static class BitmapHeader
        {
            public const uint SIZE = PIXELDATA_START_OFFSET;

            public static uint DibHeaderSize
            {
                get
                {
                    return readUint(14);
                }
                private set
                {
                    write(14, value);
                }
            }

            private static readonly byte[] mData = new byte[SIZE];

            static BitmapHeader()
            {
                // Bitmap file header
                mData[0] = (byte)'B';
                mData[1] = (byte)'M';

                write(10, PIXELDATA_START_OFFSET);

                // DIB header
                DibHeaderSize = DIB_HEADER_SIZE;

                mData[26] = 1;              // num planes
                mData[28] = 32;             // bit count
                write(30, BI_BITFIELDS);    // compression
                write(38, PIXELS_PER_METER);// x
                write(42, PIXELS_PER_METER);// y
                write(54, RED_MASK);
                write(58, GREEN_MASK);
                write(62, BLUE_MASK);
                write(66, ALPHA_MASK);
                write(70, COLOR_SPACE);
                write(122, LCS_GM_IMAGES); // intent
            }

            public static void SaveHeader(BinaryWriter writer, Bitmap bitmap)
            {
                int fileSize = mData.Length + bitmap.Width * bitmap.Height * 4;

                write(2, fileSize);
                write(18, bitmap.Width);
                write(22, bitmap.Height);
                write(34, bitmap.Width * bitmap.Height * 4);

                writer.Write(mData);
            }

            private static void write(int offset, int value)
            {
                for (int i = 0; i < 4; ++i)
                {
                    mData[offset++] = (byte)(value & 0xFF);
                    value >>= 8;
                }
            }

            private static void write(int offset, uint value)
            {
                for (int i = 0; i < 4; ++i)
                {
                    mData[offset++] = (byte)(value & 0xFF);
                    value >>= 8;
                }
            }

            private static uint readUint(int offset)
            {
                uint value = 0;

                for (int i = 0; i < 4; ++i)
                {
                    value = (uint)(mData[offset++] << i) | value;
                }

                return value;
            }
        }
    }
}
