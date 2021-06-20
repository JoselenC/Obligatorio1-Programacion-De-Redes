using BusinessLogic.Managers;
using DataAccess.DtoOjects;
using DataAccess.Mappers;
using DomainObjects;

namespace DataAccess
{
    public class DataBasePostRepository: ManagerPostRepository
    {
        public DataBasePostRepository()
        {
            Posts = new DataBaseRepository<Post, PostDto>(new PostMapper());
        }
    }
}