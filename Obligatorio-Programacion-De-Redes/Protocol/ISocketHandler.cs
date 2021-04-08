namespace Protocol
{
    public interface ISocketHandler
    {
      void Send(byte[] data);
      byte[] Receive(int size);
    }
}