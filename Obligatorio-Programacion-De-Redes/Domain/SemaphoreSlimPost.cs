using System.Threading;

namespace Domain
{
    public class SemaphoreSlimPost
    {
        public SemaphoreSlim SemaphoreSlim { get; set; }
        public Post Post { get; set; }
    }
}