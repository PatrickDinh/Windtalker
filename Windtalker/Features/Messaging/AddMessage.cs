using System;
using Autofac;
using Windtalker.Domain;
using Windtalker.Plumbing;

namespace Windtalker.Features.Messaging
{
    [InstancePerDependency]
    public class AddMessage : IAddMessage
    {
        private readonly IClock _clock;
        private readonly IRepository<Message> _messageRepositry;

        public AddMessage(IClock clock,
                          IRepository<Message> messageRepositry)
        {
            _clock = clock;
            _messageRepositry = messageRepositry;
        }


        public Message Add(string createdBy, Guid roomId, string body)
        {
            var newMessage = Message.CreateMessage(_clock.UtcNow, createdBy, roomId, body);
            _messageRepositry.Add(newMessage);
            return newMessage;
        }
    }
}