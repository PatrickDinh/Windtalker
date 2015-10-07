using System.Linq;

namespace Windtalker.Domain
{
    public interface IQuery<TResult>
    {
        IQueryable<TResult> Execute(IQueryable<TResult> source);
    }
}