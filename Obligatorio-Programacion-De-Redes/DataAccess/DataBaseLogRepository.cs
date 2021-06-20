using BusinessLogic.Managers;
using DataAccess.DtoOjects;
using DataAccess.Mappers;
using DomainObjects;


namespace DataAccess
{
    public class DataBaseLogRepository:ManagerLogRepository
    {
        public DataBaseLogRepository()
        {
            Logs = new DataBaseRepository<Log, LogDto>(new LogMapper());
        }
    }
}