using System.Threading.Tasks;

namespace ProtocolFiles
{
    public interface IFileStreamHandler
    {
        Task<byte[]> ReadAsync(string path, long offset, int length);
        Task WriteAsync(string fileName, byte[] data);
    }
}