using System.Collections.Generic;
using Autofac;
using Windtalker.Domain;

namespace Windtalker.Features.ManageRoom
{
    [InstancePerDependency]
    public class GetRooms : IGetRooms
    {
        private readonly IRepository<Room> _roomRepository;

        public GetRooms(IRepository<Room> roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public IEnumerable<Room> GetAllRooms()
        {
            return _roomRepository.All();
        }
    }
}