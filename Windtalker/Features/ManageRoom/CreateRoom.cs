using Autofac;
using Windtalker.Domain;
using Windtalker.Plumbing;

namespace Windtalker.Features.ManageRoom
{
    [InstancePerDependency]
    public class CreateRoom : ICreateRoom
    {
        private readonly IClock _clock;
        private readonly IRepository<Room> _roomRepository;

        public CreateRoom(IClock clock, IRepository<Room> roomRepository)
        {
            _clock = clock;
            _roomRepository = roomRepository;
        }

        public Room Create(string name)
        {
            var newRoom = Room.CreateRoom(name, _clock.UtcNow);
            _roomRepository.Add(newRoom);

            return newRoom;
        }
    }
}