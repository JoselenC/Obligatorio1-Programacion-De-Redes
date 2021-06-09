using System.Threading;

namespace Domain
{
    public class SemaphoreSlimTheme
    {
        public int Id { get; set; }
        public SemaphoreSlim SemaphoreSlim { get; set; }
        public Theme Theme { get; set; }
    }
}