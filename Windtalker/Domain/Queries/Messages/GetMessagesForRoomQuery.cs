using System;
using System.Linq;

namespace Windtalker.Domain.Queries.Messages
{
    public class GetMessagesForRoomQuery : IQuery<Message>
    {
        private readonly Guid _roomId;

        public GetMessagesForRoomQuery(Guid roomId)
        {
            _roomId = roomId;
        }

        public IQueryable<Message> Execute(IQueryable<Message> source)
        {
            return source.Where(m => m.RoomId == _roomId);
        }
    }
}