using System.Linq;
using Autofac;
using Windtalker.Domain;
using Windtalker.Domain.Exceptions;
using Windtalker.Domain.Queries.Rooms;
using Windtalker.Plumbing;

namespace Windtalker.Features.ManageRoom
{
    [InstancePerDependency]
    public class CreateRoom : ICreateRoom
    {
        private readonly IClock _clock;
        private readonly IRepository<Room> _roomRepository;
        private readonly IQueryExecutor _queryExecute;

        public CreateRoom(IClock clock, 
            IRepository<Room> roomRepository, IQueryExecutor queryExecute)
        {
            _clock = clock;
            _roomRepository = roomRepository;
            _queryExecute = queryExecute;
        }

        public Room Create(string name)
        {
            var query = new FindRoomByNameQuery(name);
            var roomNameIsAlreadyTaken = _queryExecute.Execute(query).Any();

            if (roomNameIsAlreadyTaken)
            {
                throw new RoomNameIsAlreadyTakenException(name);
            }

            var newRoom = Room.CreateRoom(name, _clock.UtcNow);
            _roomRepository.Add(newRoom);

            return newRoom;
        }
    }
}