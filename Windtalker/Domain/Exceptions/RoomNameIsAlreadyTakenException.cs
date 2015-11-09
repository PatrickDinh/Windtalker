namespace Windtalker.Domain.Exceptions
{
    public class RoomNameIsAlreadyTakenException : DomainException
    {
        public RoomNameIsAlreadyTakenException(string roomName) : base(roomName + " is already taken")
        {
        }
    }
}