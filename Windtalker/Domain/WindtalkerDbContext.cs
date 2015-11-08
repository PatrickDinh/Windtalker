using System.Data.Entity;
using Autofac;
using Windtalker.Settings;

namespace Windtalker.Domain
{
    [InstancePerLifetimeScope]
    public class WindtalkerDbContext : DbContext, IWindtalkerDbContext
    {
        public WindtalkerDbContext(DbConnectionString dbConnectionString) : base(dbConnectionString)
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<WindtalkerDbContext>());
        }

        public IDbSet<User> Users { get; set; }
        public IDbSet<Room> Rooms { get; set; }

        public IDbSet<TResult> Set<TResult>() where TResult : EntityBase
        {
            return base.Set<TResult>();
        }
    }
}
