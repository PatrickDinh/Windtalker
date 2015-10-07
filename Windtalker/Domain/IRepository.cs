using System;
using System.Linq;

namespace Windtalker.Domain
{
    public interface IRepository<T> where T : EntityBase
    {
        void Add(T entity);
        T Get(string id);
        IQueryable<T> All();
        IQueryable<T> Filter(Func<T, bool> filter);
    }
}