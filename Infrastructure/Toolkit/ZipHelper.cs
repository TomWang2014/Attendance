// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ZipHelper.cs" company="">
//   
// </copyright>
// <author>李天赐</author>
// <date>2015-03-31 16:32:50</date>
// <summary>
//   Gzip压缩辅助类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Attendance.Infrastructure.Toolkit
{
    using System;
    using System.Data;
    using System.IO;
    using System.IO.Compression;

    /// <summary>
    /// Gzip压缩辅助类
    /// </summary>
    public class ZipHelper
    {
        /// <summary>
        /// 解压  
        /// </summary>
        /// <param name="Value">
        /// 需要解压的value
        /// </param>
        /// <returns>
        /// 返回DataSet
        /// </returns>
        public static DataSet GetDatasetByString(string Value)
        {
            var ds = new DataSet();
            var cc = GZipDecompressString(Value);
            var sr = new StringReader(cc);
            ds.ReadXml(sr);
            return ds;
        }

        /// <summary>
        /// 根据DATASET压缩字符串  
        /// </summary>
        /// <param name="ds">
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetStringByDataset(string ds)
        {
            return GZipCompressString(ds);
        }

        /// <summary>
        /// 将传入字符串以GZip算法压缩后，返回Base64编码字符  
        /// </summary>
        /// <param name="rawString">
        /// 需要压缩的字符串
        /// </param>
        /// <returns>
        /// 压缩后的Base64编码的字符串
        /// </returns>
        public static string GZipCompressString(string rawString)
        {
            if (string.IsNullOrEmpty(rawString) || rawString.Length == 0)
            {
                return string.Empty;
            }

            var rawData = System.Text.Encoding.UTF8.GetBytes(rawString);
            var zippedData = Compress(rawData);
            return Convert.ToBase64String(zippedData);
        }

        /// <summary>
        /// GZip压缩  
        /// </summary>
        /// <param name="rawData">
        /// </param>
        /// <returns>
        /// The <see cref="byte[]"/>.
        /// </returns>
        public static byte[] Compress(byte[] rawData)
        {
            var ms = new MemoryStream();
            var compressedzipStream = new GZipStream(ms, CompressionMode.Compress, true);
            compressedzipStream.Write(rawData, 0, rawData.Length);
            compressedzipStream.Close();
            return ms.ToArray();
        }

        /// <summary>
        /// 将传入的二进制字符串资料以GZip算法解压缩  
        /// </summary>
        /// <param name="zippedString">
        /// 经GZip压缩后的二进制字符串
        /// </param>
        /// <returns>
        /// 原始未压缩字符串
        /// </returns>
        public static string GZipDecompressString(string zippedString)
        {
            if (string.IsNullOrEmpty(zippedString) || zippedString.Length == 0)
            {
                return string.Empty;
            }

            var zippedData = Convert.FromBase64String(zippedString);
            return System.Text.Encoding.UTF8.GetString(Decompress(zippedData));
        }

        /// <summary>
        /// ZIP解压  
        /// </summary>
        /// <param name="zippedData">
        /// </param>
        /// <returns>
        /// The <see cref="byte[]"/>.
        /// </returns>
        public static byte[] Decompress(byte[] zippedData)
        {
            var ms = new MemoryStream(zippedData);
            var compressedzipStream = new GZipStream(ms, CompressionMode.Decompress);
            var outBuffer = new MemoryStream();
            var block = new byte[1024];
            while (true)
            {
                var bytesRead = compressedzipStream.Read(block, 0, block.Length);
                if (bytesRead <= 0)
                {
                    break;
                }

                outBuffer.Write(block, 0, bytesRead);
            }

            compressedzipStream.Close();
            return outBuffer.ToArray();
        }
    }  
}
