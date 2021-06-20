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
                Message = obj.Message,
                CreationDate = obj.CreationDate,
                ObjectName = obj.ObjectName,
                ObjectType = obj.ObjectType
            };
        }

        public Log DtoToDomain(LogDto obj, ContextDb context)
        {
            return new Log()
            {
                Message = obj.Message,
                CreationDate = obj.CreationDate,
                ObjectName = obj.ObjectName,
                ObjectType = obj.ObjectType
            };
        }

        public LogDto UpdateDtoObject(LogDto objToUpdate, Log updatedObject, ContextDb context)
        { 
            objToUpdate.Message = updatedObject.Message;
            objToUpdate.CreationDate = updatedObject.CreationDate;
            objToUpdate.ObjectName = updatedObject.ObjectName;
            objToUpdate.ObjectType = updatedObject.ObjectType;
            return objToUpdate;
        }
    }
}