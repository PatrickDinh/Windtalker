using System;

namespace Windtalker.Domain
{
    public class Message : EntityBase
    {
        public Guid RoomId { get; set; }

        public Room Room { get; set; }

        public string Body { get; set; }

        protected Message() { }

        public Message(Guid id, DateTime dateCreated, string createdBy, Guid roomId, string body)
        {
            Id = id;
            DateCreated = dateCreated;
            RoomId = roomId;
            Body = body;
            CreatedBy = createdBy;
        }

        public static Message CreateMessage(DateTime dateCreated, string createdBy, Guid roomId, string body)
        {
            return new Message(Guid.NewGuid(), dateCreated, createdBy, roomId, body);
        }
    }
}