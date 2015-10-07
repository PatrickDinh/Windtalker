using System.Data.Entity;

namespace Windtalker.Domain
{
    public interface IWindtalkerDbContext
    {
        IDbSet<User> Users { get; set; }

        IDbSet<TResult> Set<TResult>() where TResult : EntityBase;
    }
}