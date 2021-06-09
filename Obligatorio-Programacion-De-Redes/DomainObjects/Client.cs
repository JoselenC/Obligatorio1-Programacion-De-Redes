namespace Domain
{
    public class Client
    {
        public int Id { get; set; }
        public string TimeOfConnection { get; set; }
        public string LocalEndPoint { get; set; }
        public string Ip { get; set; }
        
        public override bool Equals(object? obj)
        {
            return ((Client) obj).Ip == Ip;
        }
    }
}