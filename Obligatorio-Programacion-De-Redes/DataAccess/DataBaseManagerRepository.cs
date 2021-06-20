using BusinessLogic;
using BusinessLogic.Managers;
using DataAccess.DtoOjects;
using DataAccess.Mappers;
using DomainObjects;

namespace DataAccess
{
    public class DataBaseManagerRepository:ManagerRepository
    {
        public DataBaseManagerRepository()
        {
            Files = new DataBaseRepository<File, FileDto>(new FileMapper());
            Clients = new DataBaseRepository<Client, ClientDto>(new ClientMapper());
       }
    }
}