using System.Linq;
using DataAccess.DtoOjects;
using Domain;
using MSP.BetterCalm.DataAccess;

namespace DataAccess.Mappers
{
    public class SemaphoreThemeMapper:IMapper<SemaphoreSlimTheme, SemaphoreSlimThemeDto>
    {
        public SemaphoreSlimThemeDto DomainToDto(SemaphoreSlimTheme obj, ContextDb context)
        {
            SemaphoreSlimThemeDto semaphoreSlimThemeDto = context.SemaphoresSlimThemeDto
                .FirstOrDefault(x => x.Id == obj.Id);
            if (semaphoreSlimThemeDto is null)
            {
                semaphoreSlimThemeDto = new SemaphoreSlimThemeDto()
                {
                    Id = obj.Id,
                    Theme = new ThemeMapper().DomainToDto(obj.Theme, context)
                };
            }

            return semaphoreSlimThemeDto;
        }

        public SemaphoreSlimTheme DtoToDomain(SemaphoreSlimThemeDto obj, ContextDb context)
        {
            return new SemaphoreSlimTheme()
            {
                Id = obj.Id,
                Theme = new ThemeMapper().DtoToDomain(obj.Theme, context)
            };
        }

        public SemaphoreSlimThemeDto UpdateDtoObject(SemaphoreSlimThemeDto objToUpdate, SemaphoreSlimTheme updatedObject,
            ContextDb context)
        {
            throw new System.NotImplementedException();
        }
    }
}