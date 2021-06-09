using System.Linq;
using DataAccess.DtoOjects;
using Domain;
using MSP.BetterCalm.DataAccess;

namespace DataAccess.Mappers
{
    public class SemaphorePostMapper:IMapper<SemaphoreSlimPost, SemaphoreSlimPostDto>
    {
        public SemaphoreSlimPostDto DomainToDto(SemaphoreSlimPost obj, ContextDb context)
        {
            SemaphoreSlimPostDto semaphoreSlimPostDto = context.SemaphoresSlimPostDto
                .FirstOrDefault(x => x.Id == obj.Id);
            if (semaphoreSlimPostDto is null)
            {
                semaphoreSlimPostDto = new SemaphoreSlimPostDto()
                {
                    Id = obj.Id,
                    Post = new PostMapper().DomainToDto(obj.Post, context)
                };
            }

            return semaphoreSlimPostDto;
        }

        public SemaphoreSlimPost DtoToDomain(SemaphoreSlimPostDto obj, ContextDb context)
        {
            return new SemaphoreSlimPost()
            {
               Id = obj.Id,
               Post = new PostMapper().DtoToDomain(obj.Post, context)
            };
        }

        public SemaphoreSlimPostDto UpdateDtoObject(SemaphoreSlimPostDto objToUpdate, SemaphoreSlimPost updatedObject,
            ContextDb context)
        {
            throw new System.NotImplementedException();
        }
    }
}