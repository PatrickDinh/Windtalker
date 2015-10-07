using System;
using System.Linq;

namespace Windtalker.Domain
{
    public class Repository<T> : IRepository<T> where T : EntityBase
    {
        private readonly WindtalkerDbContext _db;

        public Repository(WindtalkerDbContext db)
        {
            _db = db;
        }

        public IQueryable<T> All()
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> Filter(Func<T, bool> filter)
        {
            throw new NotImplementedException();
        }

        public void Add(T entity)
        {
            var set = _db.Set<T>();
            set.Add(entity);
        }

        public T Get(string id)
        {
            throw new NotImplementedException();
        }
    }
}