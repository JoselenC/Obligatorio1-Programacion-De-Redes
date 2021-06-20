namespace Protocol
{
    public class Packet
    {
        public string Header { get; set; }
        public string Command { get; set; }
        public string Length { get; set; }
        public string Data { get; set; }

        public Packet() { }

        public Packet(string head, string command, string data )
        {
            Header = head;
            Command = command.PadLeft(2, '0');
            Data = data;
            Length = (data.Length+5).ToString().PadLeft(4, '0');
        }
        
    }
}