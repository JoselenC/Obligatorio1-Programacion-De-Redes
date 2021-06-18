using DataAccess.DtoOjects;
using DomainObjects;
using MSP.BetterCalm.DataAccess;

namespace DataAccess.Mappers
{
    public class LogMapper : IMapper<Log, LogDto>
    {
        public LogDto DomainToDto(Log obj, ContextDb context)
        {
            return new LogDto()
            {
                Message = obj.Message
            };
        }

        public Log DtoToDomain(LogDto obj, ContextDb context)
        {
            return new Log()
            {
                Message = obj.Message
            };
        }

        public LogDto UpdateDtoObject(LogDto objToUpdate, Log updatedObject, ContextDb context)
        { 
            objToUpdate.Message = updatedObject.Message;
            return objToUpdate;
        }
    }
}