using Windtalker.Domain;

namespace Windtalker.Features.ManageRoom
{
    public interface ICreateRoom
    {
        Room Create(string name);
    }
}