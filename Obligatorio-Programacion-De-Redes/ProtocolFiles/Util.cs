namespace Common
{
    public class Utils
    {
        public static long GetFileParts(long filesize)
        {
            var parts = filesize / ProtocolSpecification.MaxPacketSize;
            return parts * ProtocolSpecification.MaxPacketSize == filesize ? parts : parts + 1;
        }
    }
}