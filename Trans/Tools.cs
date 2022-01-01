using System;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;

namespace Trans
{
    public enum CompressionType
    {
        None,
        DeflateStream,
        LZMA
    }

    public class Tools
    {

        public enum Units
        {
            Speed,
            Data
        }

        private static readonly NumberFormatInfo format = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone();
        
        public static string ConvertBytesSpeed(double bytes)
        {
            format.NumberGroupSeparator = " ";
            format.NumberDecimalDigits = 2;
            string text = "#,#.00";
            string result;
            bytes *= 8;
            if (bytes == 0.0)
            {
                result = "0 Б";
            }
            else
            {
                if (Math.Abs(bytes) < 1000.0)
                {
                    result = bytes.ToString(text, format);
                    result += " бит/сек";
                }
                else
                {
                    double kbps = bytes / 1000.0;
                    if (Math.Abs(kbps) < 1000.0)
                    {
                        result = kbps.ToString(text, format);
                        result += " Кбит/сек";
                    }
                    else
                    {
                        double mbps = kbps / 1000.0;
                        if (Math.Abs(mbps) < 1000.0)
                        {
                            result = mbps.ToString(text, format);
                            result += " Мбит/сек";
                        }
                        else
                        {
                            double gbps = mbps / 1000.0;
                            if (Math.Abs(gbps) < 1000.0)
                            {
                                result = gbps.ToString(text, format);
                                result += " Гбит/сек";
                            }
                            else
                            {
                                result = (gbps / 1000.0).ToString(text, format);
                                result += " Тбит/сек";
                            }
                        }
                    }
                }
            }
            return result;
        }

        public static string ConvertBytesData(double bytes)
        {
            format.NumberGroupSeparator = " ";
            format.NumberDecimalDigits = 2;
            string text = "#,#.00";
            string result;
            if (bytes == 0.0)
            {
                result = "0 байт";
            }
            else
            {
                if (Math.Abs(bytes) < 1024.0)
                {
                    result = bytes.ToString(text, format);
                    result += " байт";
                }
                else
                {
                    double kbps = bytes / 1024.0;
                    if (Math.Abs(kbps) < 1024.0)
                    {
                        result = kbps.ToString(text, format);
                        result += " Кбайт";
                    }
                    else
                    {
                        double mbps = kbps / 1024.0;
                        if (Math.Abs(mbps) < 1024.0)
                        {
                            result = mbps.ToString(text, format);
                            result += " Мбайт";
                        }
                        else
                        {
                            double gbps = mbps / 1024.0;
                            if (Math.Abs(gbps) < 1024.0)
                            {
                                result = gbps.ToString(text, format);
                                result += " Гбайт";
                            }
                            else
                            {
                                result = (gbps / 1024.0).ToString(text, format);
                                result += " Тбайт";
                            }
                        }
                    }
                }
            }
            return result;
        }

        public static string ConvertBytes(double bytes, Units units)
        {
            switch (units)
            {
                case Units.Speed:
                    return ConvertBytesSpeed(bytes);
                case Units.Data:
                    return ConvertBytesData(bytes);
                default:
                    return "-";
            }
            /*format.NumberGroupSeparator = " ";
            format.NumberDecimalDigits = 2;
            string text = "#,#.00";
            string result = "";
            if (bytes == 0.0)
            {
                switch (units)
                {
                    case Units.Speed:
                        result = "0 Б/сек";
                        break;
                    case Units.Data:
                        result = "0 Б";
                        break;
                }
            }
            else
            {
                if (Math.Abs(bytes) < 1024.0)
                {
                    result = bytes.ToString(text, format);
                    switch (units)
                    {
                        case Units.Speed:
                            result += " Б/сек";
                            break;
                        case Units.Data:
                            result += " Б";
                            break;
                    }
                }
                else
                {
                    double kbps = bytes / 1024.0;
                    if (Math.Abs(kbps) < 1024.0)
                    {
                        result = kbps.ToString(text, format);
                        switch (units)
                        {
                            case Units.Speed:
                                result += " Кб/сек";
                                break;
                            case Units.Data:
                                result += " Кб";
                                break;
                        }
                    }
                    else
                    {
                        double mbps = kbps / 1024.0;
                        if (Math.Abs(mbps) < 1024.0)
                        {
                            result = mbps.ToString(text, format);
                            switch (units)
                            {
                                case Units.Speed:
                                    result += " Мб/сек";
                                    break;
                                case Units.Data:
                                    result += " Мб";
                                    break;
                            }
                        }
                        else
                        {
                            double gbps = mbps / 1024.0;
                            if (Math.Abs(gbps) < 1024.0)
                            {
                                result = gbps.ToString(text, format);
                                switch (units)
                                {
                                    case Units.Speed:
                                        result += " Гб/сек";
                                        break;
                                    case Units.Data:
                                        result += " Гб";
                                        break;
                                }
                            }
                            else
                            {
                                result = (gbps / 1024.0).ToString(text, format);
                                switch (units)
                                {
                                    case Units.Speed:
                                        result += " Тб/сек";
                                        break;
                                    case Units.Data:
                                        result += " Тб";
                                        break;
                                }
                            }
                        }
                    }
                }
            }
            return result;*/
        }

        public static string ConvertTime(int sec)
        {
            int h = sec / 60 / 60;
            int m = sec / 60 % 60;
            int s = sec % 60;
            return string.Concat(new string[]
            {
                h.ToString(),
                " ч ",
                m.ToString(),
                " мин ",
                s.ToString(),
                " сек"
            });
        }

        public static double Map(double value, double low, double high, double low_2, double high_2)
        {
            double num = (value - low) / (high - low);
            return low_2 + (high_2 - low_2) * num;
        }

        public static double GetNearestUnitOver(double val)
        {
            int num = (int)Math.Floor(val);
            while (num % 5 != 0)
            {
                num++;
            }
            return num;
        }

        public static double RoundMaxSpeed(double bytes)
        {
            double result;
            if (bytes == 0.0)
            {
                result = 0.0;
            }
            else
            {
                if (bytes < 1000.0)
                {
                    result = GetNearestUnitOver(bytes);
                }
                else
                {
                    double kbps = bytes / 1000.0;
                    if (kbps < 1000.0)
                    {
                        result = GetNearestUnitOver(kbps) * 1000.0;
                    }
                    else
                    {
                        double mbps = kbps / 1000.0;
                        if (mbps < 1000.0)
                        {
                            result = GetNearestUnitOver(mbps) * 1000.0 * 1000.0;
                        }
                        else
                        {
                            double gbps = mbps / 1000.0;
                            if (gbps < 1000.0)
                            {
                                result = GetNearestUnitOver(gbps) * 1000.0 * 1000.0 * 1000.0;
                            }
                            else
                            {
                                result = GetNearestUnitOver(gbps / 1000.0) * 1000.0 * 1000.0 * 1000.0 * 1000.0;
                            }
                        }
                    }
                }
            }
            return result;
        }

        public static byte[] CalcSHA256(FileStream fileStream, bool closeStream, bool resetPosAfterCalc)
        {
            using (SHA256 mySHA256 = SHA256.Create())
            {
                try
                {
                    fileStream.Position = 0;
                    byte[] hashValue = mySHA256.ComputeHash(fileStream);
                    if (resetPosAfterCalc && !closeStream)
                        fileStream.Position = 0;
                    if (closeStream)
                        fileStream.Close();
                    return hashValue;
                }
                catch (IOException e)
                {
                    return null;
                }
                catch (UnauthorizedAccessException e)
                {
                    return null;
                }
            }
        }

        public static void ClearMemoryStream(MemoryStream source)
        {
            byte[] buffer = source.GetBuffer();
            Array.Clear(buffer, 0, buffer.Length);
            source.Position = 0;
            source.SetLength(0);
        }

        public static byte[] Compress(byte[] data, CompressionType compressionType)
        {
            switch (compressionType)
            {
                case CompressionType.None:
                default:
                    return data;
                case CompressionType.DeflateStream:
                    return CompressByDeflateStream(data);
                case CompressionType.LZMA:
                    return Compress7Zip(data);
            }
        }

        public static byte[] Decompress(byte[] data, CompressionType compressionType)
        {
            switch (compressionType)
            {
                case CompressionType.None:
                default:
                    return data;
                case CompressionType.DeflateStream:
                    return DecompressByDeflateStream(data);
                case CompressionType.LZMA:
                    return Decompress7Zip(data);
            }
        }

        public static byte[] CompressByDeflateStream(byte[] data)
        {
            MemoryStream output = new MemoryStream();
            using (DeflateStream dstream = new DeflateStream(output, CompressionLevel.Optimal))
            {
                dstream.Write(data, 0, data.Length);
            }
            return output.ToArray();
        }

        public static byte[] DecompressByDeflateStream(byte[] data)
        {
            MemoryStream input = new MemoryStream(data);
            MemoryStream output = new MemoryStream();
            using (DeflateStream dstream = new DeflateStream(input, CompressionMode.Decompress))
            {
                dstream.CopyTo(output);
            }
            return output.ToArray();
        }

        public static byte[] Compress7Zip(byte[] data)
        {
            return SevenZip.Compression.LZMA.SevenZipHelper.Compress(data);
        }

        public static byte[] Decompress7Zip(byte[] data)
        {
            return SevenZip.Compression.LZMA.SevenZipHelper.Decompress(data);
        }

    }
}