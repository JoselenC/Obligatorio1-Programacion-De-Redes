using System.IO;

namespace Common
{
    public class FileStreamHandler
    {
        public byte[] ReadFile(string path)
        {
            return File.ReadAllBytes(path);
        }

        public void WriteFile(string path, byte[] data)
        {
            File.WriteAllBytes(path,data);
        }
    }
}