using System;
using System.Collections.Generic;
using Windtalker.Domain;

namespace Windtalker.Features.Messaging
{
    public interface IGetMessagesForRoom
    {
        IReadOnlyList<Message> Get(Guid roomId);
    }
}