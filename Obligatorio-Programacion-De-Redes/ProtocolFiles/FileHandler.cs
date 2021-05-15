using System;
using System.IO;

namespace ProtocolFiles
{
    public class FileHandler
    {
        public bool FileExists(string path)
        {
            return File.Exists(path);
        }

        public string GetFileName(string path)
        {
            if (FileExists(path))
            {
                return new FileInfo(path).Name;
            }
            throw new Exception("File not exist");
        }

        public long GetFileSize(string path)
        {
            if (FileExists(path) && IsValidSize(path))
            {
                return new FileInfo(path).Length;
            }
            else if(!IsValidSize(path))
            {
                throw new Exception("Invalid size");
            }
            else{
                throw new Exception("File not exist");
            }
        }

        private bool IsValidSize(string path)
        {
            long length = path.Length;
            long fileSizeInKB = length / 1024;
            long fileSizeInMB = fileSizeInKB / 1024;
            return fileSizeInMB <= 100;
        }
    }
}