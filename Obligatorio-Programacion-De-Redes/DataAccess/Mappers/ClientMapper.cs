using System.Linq;
using DataAccess.DtoOjects;
using Domain;
using MSP.BetterCalm.DataAccess;

namespace DataAccess.Mappers
{
    public class ClientMapper: IMapper<Client, ClientDto>
    {
        public ClientDto DomainToDto(Client obj, ContextDb context)
        {
            ClientDto clientDto = context.Clients.FirstOrDefault(x => x.Ip == obj.Ip);
            if (clientDto is null)
                clientDto = new ClientDto()
                {
                    LocalEndPoint = obj.LocalEndPoint,
                    TimeOfConnection = obj.TimeOfConnection
                };
            return clientDto;
        }

        public Client DtoToDomain(ClientDto obj, ContextDb context)
        {
            return new Client()
            {
                Id = obj.Id, 
                Ip = obj.Ip,
                LocalEndPoint = obj.LocalEndPoint,
                TimeOfConnection = obj.TimeOfConnection
            };
        }

        public ClientDto UpdateDtoObject(ClientDto objToUpdate, Client updatedObject, ContextDb context)
        {
            objToUpdate.Ip = updatedObject.Ip;
            objToUpdate.LocalEndPoint = updatedObject.LocalEndPoint;
            objToUpdate.TimeOfConnection = updatedObject.TimeOfConnection;
            return objToUpdate;
        }
    }
}