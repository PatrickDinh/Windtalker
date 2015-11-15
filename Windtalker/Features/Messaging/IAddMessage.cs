using System;
using Windtalker.Domain;

namespace Windtalker.Features.Messaging
{
    public interface IAddMessage
    {
        Message Add(string createdBy, Guid roomId, string body);
    }
}