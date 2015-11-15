using System;
using System.Collections.Generic;
using Autofac;
using Windtalker.Domain;
using Windtalker.Domain.Queries.Messages;
using Windtalker.Plumbing;

namespace Windtalker.Features.Messaging
{
    [InstancePerDependency]
    public class GetMessagesForRoom : IGetMessagesForRoom
    {
        private readonly IQueryExecutor _queryExecutor;

        public GetMessagesForRoom(IQueryExecutor queryExecutor)
        {
            _queryExecutor = queryExecutor;
        }

        public IReadOnlyList<Message> Get(Guid roomId)
        {
            var query = new GetMessagesForRoomQuery(roomId);
            return _queryExecutor.Execute(query);
        }
    }
}