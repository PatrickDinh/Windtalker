using Windtalker.Domain;

namespace Windtalker.Features.Register
{
    public interface ICreateUser
    {
        User Create(string email, string plainTextPassword);
    }
}