using MSP.BetterCalm.DataAccess;

namespace DataAccess.Mappers
{
    public interface IMapper<D,T> where T: class 
    {
        T DomainToDto(D obj,ContextDb context);

        D DtoToDomain(T obj,ContextDb context);
      
        T UpdateDtoObject (T objToUpdate, D updatedObject,ContextDb context);
    }
}