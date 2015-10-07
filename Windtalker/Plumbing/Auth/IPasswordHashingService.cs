namespace Windtalker.Plumbing.Auth
{
    public interface IPasswordHashingService
    {
        string SaltAndHash(string plainTextPassword);
        bool TryVerify(string password, string saltedAndHashedPassword);
    }
}