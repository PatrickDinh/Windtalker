using System;
using System.Linq;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;
using Windtalker.Domain;
using Windtalker.Features.Messaging;

namespace Windtalker.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly IAddMessage _addMessage;
        private readonly IGetMessagesForRoom _getMessagesForRoom;
        private readonly IUnitOfWork _unitOfWork;

        public ChatHub(IAddMessage addMessage,
                       IUnitOfWork unitOfWork,
                       IGetMessagesForRoom getMessagesForRoom)
        {
            _addMessage = addMessage;
            _unitOfWork = unitOfWork;
            _getMessagesForRoom = getMessagesForRoom;
        }

        public void SendMessage(MessageDto message)
        {
            var username = Context.User.Identity.Name;
            var newMessage = _addMessage.Add(username, message.RoomId, message.Body);

            _unitOfWork.SaveChanges();

            var dto = new MessageInRoomDto
            {
                Username = newMessage.CreatedBy,
                DateCreated = newMessage.DateCreated,
                Body = newMessage.Body,
                RoomId = newMessage.RoomId
            };
            Clients.All.displayMessage(dto);
        }

        public MessageInRoomDto[] GetMessages(Guid roomId)
        {
            var messages = _getMessagesForRoom.Get(roomId);

            return messages.Select(m => new MessageInRoomDto
            {
                Username = m.CreatedBy,
                DateCreated = m.DateCreated,
                Body = m.Body,
                RoomId = m.RoomId
            }).ToArray();
        }
    }

    public class MessageDto
    {
        [JsonProperty("roomId")]
        public Guid RoomId { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }
    }

    public class MessageInRoomDto
    {
        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("dateCreated")]
        public DateTime DateCreated { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }

        [JsonProperty("roomId")]
        public Guid RoomId { get; set; }
    }
}