using System.Data.Entity;

namespace Windtalker.Domain
{
    public interface IWindtalkerDbContext
    {
        IDbSet<User> Users { get; set; }

        IDbSet<Room> Rooms { get; set; }

        IDbSet<Message> Messages { get; set; }

        IDbSet<TResult> Set<TResult>() where TResult : EntityBase;
    }
}