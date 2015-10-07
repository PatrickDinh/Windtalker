using System.Collections.Generic;
using System.Linq;
using Autofac;
using Windtalker.Domain;

namespace Windtalker.Plumbing
{
    [InstancePerLifetimeScope]
    internal class QueryExecutor : IQueryExecutor
    {
        private readonly IWindtalkerDbContext _dbContext;

        public QueryExecutor(IWindtalkerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IReadOnlyList<TResult> Execute<TResult>(IQuery<TResult> query) where TResult : EntityBase
        {
            var set = _dbContext.Set<TResult>();
            var result = query.Execute(set).ToArray();
            return result;
        }
    }
}