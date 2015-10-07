using Autofac;

namespace Windtalker.Domain
{
    public interface IUnitOfWork
    {
        void SaveChanges();
    }

    [InstancePerLifetimeScope]
    public class UnitOfWork : IUnitOfWork
    {
        private readonly WindtalkerDbContext _dbContext;

        public UnitOfWork(WindtalkerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}
