namespace Protocol
{
    public class Packet
    {
        public string Header { get; set; }
        public string Command { get; set; }
        public string Length { get; set; }
        public string Data { get; set; }

        public Packet(string head, string cmd, string data )
        {
            this.Header = head;
            this.Command = cmd;
            this.Data = data;
            this.Length = (data.Length + 105).ToString().PadLeft(100, '0');
        }
        public Packet() { }
    }
}