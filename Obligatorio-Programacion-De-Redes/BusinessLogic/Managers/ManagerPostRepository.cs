using Domain;

namespace BusinessLogic.Managers
{
    public class ManagerPostRepository
    {
        public IRepository<Post> Posts { get; set; }
    }
}