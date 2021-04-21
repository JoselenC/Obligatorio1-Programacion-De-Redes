using System;

namespace ProtocolString
{
    public class HeaderHandler
    {
        public static byte[] EncodeHeader(short command, int dataLength)
        {
            var header = new byte[HeaderConstants.CommandLength + HeaderConstants.DataLength];
            Array.Copy(BitConverter.GetBytes(command), 0, header, 0, HeaderConstants.CommandLength);
            Array.Copy(BitConverter.GetBytes(dataLength), 0, header, HeaderConstants.CommandLength, HeaderConstants.DataLength);
            return header;
        }

        public Tuple<short, int> DecodeHeader(byte[] data)
        {
            try
            {
                short command = BitConverter.ToInt16(data, 0);
                int dataLength = BitConverter.ToInt32(data, HeaderConstants.CommandLength);
                return new Tuple<short, int>(command, dataLength);

            }
            catch (Exception e)
            {
                Console.WriteLine("Error decoding data: " + e.Message);
                return null;
            }
        }
    }
}