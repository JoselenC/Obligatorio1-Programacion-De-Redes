using System.Threading;

namespace Domain
{
    public class SemaphoreSlimPost
    {
        public int Id { get; set; }
        public SemaphoreSlim SemaphoreSlim { get; set; }
        public Post Post { get; set; }
    }
}