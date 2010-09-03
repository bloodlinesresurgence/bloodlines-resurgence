using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Ionic.Zlib;

namespace ResurgenceLib
{
    public class Compression
    {
        public static string StringCompress(string data)
        {
            byte[] compressed = ZlibCodecCompress(data);
            return ByteArrayToString(compressed);
        }

        public static string StringDecompress(string data)
        {
            byte[] decoded = StringToByteArray(data);
            return ZlibCodecDecompress(decoded);
        }

        private static string ByteArrayToString(byte[] buffer)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (byte b in buffer)
                sb.Append(b.ToString("X2"));

            return (sb.ToString());
        }

        private static byte[] StringToByteArray(string data)
        {
            byte[] result = new byte[data.Length / 2];
            for (int i = 0; i < data.Length; i += 2)
                result[i / 2] = Byte.Parse(data.Substring(i, 2), System.Globalization.NumberStyles.HexNumber);
            return result;
        }

        private static byte[] ZlibCodecCompress(string textToCompress)
        {
            int outputSize = 2048;
            byte[] output = new Byte[outputSize];
            byte[] uncompressed = UTF8Encoding.UTF8.GetBytes(textToCompress);
            int lengthToCompress = uncompressed.Length;

            // If you want a ZLIB stream, set this to true.  If you want
            // a bare DEFLATE stream, set this to false.
            bool wantRfc1950Header = false;

            using (MemoryStream ms = new MemoryStream())
            {
                ZlibCodec compressor = new ZlibCodec();
                compressor.InitializeDeflate(CompressionLevel.BestCompression, wantRfc1950Header);

                compressor.InputBuffer = uncompressed;
                compressor.AvailableBytesIn = lengthToCompress;
                compressor.NextIn = 0;
                compressor.OutputBuffer = output;

                foreach (var f in new FlushType[] { FlushType.None, FlushType.Finish })
                {
                    int bytesToWrite = 0;
                    do
                    {
                        compressor.AvailableBytesOut = outputSize;
                        compressor.NextOut = 0;
                        compressor.Deflate(f);

                        bytesToWrite = outputSize - compressor.AvailableBytesOut;
                        if (bytesToWrite > 0)
                            ms.Write(output, 0, bytesToWrite);
                    }
                    while ((f == FlushType.None && (compressor.AvailableBytesIn != 0 || compressor.AvailableBytesOut == 0)) ||
                           (f == FlushType.Finish && bytesToWrite != 0));
                }

                compressor.EndDeflate();

                ms.Flush();
                return ms.ToArray();
            }
        }


        private static string ZlibCodecDecompress(byte[] compressed)
        {
            int outputSize = 2048;
            byte[] output = new Byte[outputSize];

            // If you have a ZLIB stream, set this to true.  If you have
            // a bare DEFLATE stream, set this to false.
            bool expectRfc1950Header = false;

            using (MemoryStream ms = new MemoryStream())
            {
                ZlibCodec compressor = new ZlibCodec();
                compressor.InitializeInflate(expectRfc1950Header);

                compressor.InputBuffer = compressed;
                compressor.AvailableBytesIn = compressed.Length;
                compressor.NextIn = 0;
                compressor.OutputBuffer = output;

                foreach (var f in new FlushType[] { FlushType.None, FlushType.Finish })
                {
                    int bytesToWrite = 0;
                    do
                    {
                        compressor.AvailableBytesOut = outputSize;
                        compressor.NextOut = 0;
                        compressor.Inflate(f);

                        bytesToWrite = outputSize - compressor.AvailableBytesOut;
                        if (bytesToWrite > 0)
                            ms.Write(output, 0, bytesToWrite);
                    }
                    while ((f == FlushType.None && (compressor.AvailableBytesIn != 0 || compressor.AvailableBytesOut == 0)) ||
                           (f == FlushType.Finish && bytesToWrite != 0));
                }

                compressor.EndInflate();

                return UTF8Encoding.UTF8.GetString(ms.ToArray());
            }
        }
    }
}
