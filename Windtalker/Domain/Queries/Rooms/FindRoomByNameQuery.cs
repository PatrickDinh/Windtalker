using System.Linq;

namespace Windtalker.Domain.Queries.Rooms
{
    public class FindRoomByNameQuery : IQuery<Room>
    {
        private readonly string _roomName;

        public FindRoomByNameQuery(string roomName)
        {
            _roomName = roomName;
        }

        public IQueryable<Room> Execute(IQueryable<Room> source)
        {
            return source.Where(r => r.Name.Equals(_roomName));
        }
    }
}