using System;
using System.Text;

namespace ProtocolFiles
{
    public class ProtocolHelper
    {
        public int GetLength()
        {
            return ProtocolConstant.FileNameLength + ProtocolConstant.FileSizeLength;
        }

        public byte[] CreateHeader(string filename, long filesize)
        {
            var header = new byte[GetLength()];
            var fileNameLength = BitConverter.GetBytes(Encoding.UTF8.GetBytes(filename).Length);
            if (fileNameLength.Length != ProtocolConstant.FileNameLength)
                throw new Exception("Hay algo mal en esta especificacion");
            var fileSize = BitConverter.GetBytes(filesize);
            
            Array.Copy(fileNameLength,0,header,0,ProtocolConstant.FileNameLength);
            Array.Copy(fileSize,0,header,ProtocolConstant.FileNameLength,ProtocolConstant.FileSizeLength);
            return header;
        }
    }
}