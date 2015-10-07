using System.Collections.Generic;
using Windtalker.Domain;

namespace Windtalker.Plumbing
{
    public interface IQueryExecutor
    {
        IReadOnlyList<TResult> Execute<TResult>(IQuery<TResult> query) where TResult : EntityBase;
    }
}