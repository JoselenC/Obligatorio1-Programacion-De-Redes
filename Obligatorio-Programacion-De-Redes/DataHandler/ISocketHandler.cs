﻿namespace DataHandler
{
    public interface ISocketHandler
    {
      void Send(byte[] data);
      byte[] Receive(int size);
    }
}