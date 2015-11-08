using System;

namespace Windtalker.Domain
{
    public class Room : EntityBase
    {
        protected Room()
        {
        }

        public Room(Guid id, string name, DateTime dateCreated)
        {
            Id = id;
            Name = name;
            DateCreated = dateCreated;
        }

        public string Name { get; set; }

        public static Room CreateRoom(string name, DateTime dateCreated)
        {
            return new Room(Guid.NewGuid(), name, dateCreated);
        }
    }
}