using System;
using System.Text;

namespace ProtocolFiles
{
    public class ProtocolHelper
    {
        public static int GetLength()
        {
            return ProtocolSpecification.FileNameLength + ProtocolSpecification.FileSizeLength;
        }

        public static byte[] CreateHeader(string filename, long filesize)
        {
            var header = new byte[GetLength()];
            var fileNameLength = BitConverter.GetBytes(Encoding.UTF8.GetBytes(filename).Length);
            if (fileNameLength.Length != ProtocolSpecification.FileNameLength)
                throw new Exception("Hay algo mal en esta especificacion");
            var fileSize = BitConverter.GetBytes(filesize);
            
            Array.Copy(fileNameLength,0,header,0,ProtocolSpecification.FileNameLength);
            Array.Copy(fileSize,0,header,ProtocolSpecification.FileNameLength,ProtocolSpecification.FileSizeLength);
            return header;
        }
    }
}