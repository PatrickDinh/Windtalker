using Autofac;
using Windtalker.Domain;
using Windtalker.Domain.Exceptions;
using Windtalker.Domain.Queries.Users;
using Windtalker.Plumbing;
using Windtalker.Plumbing.Auth;

namespace Windtalker.Features.Register
{
    [InstancePerDependency]
    public class CreateUser : ICreateUser
    {
        private readonly IPasswordHashingService _passwordHashingService;
        private readonly IQueryExecutor _queryExecutor;
        private readonly IRepository<User> _userRepository;

        public CreateUser(IRepository<User> userRepository,
                          IQueryExecutor queryExecutor,
                          IPasswordHashingService passwordHashingService)
        {
            _userRepository = userRepository;
            _queryExecutor = queryExecutor;
            _passwordHashingService = passwordHashingService;
        }

        public User Create(string email, string plainTextPassword)
        {
            var query = new FindUserByEmailQuery(email);
            var existingUser = _queryExecutor.Execute(query);
            if (existingUser.Count > 0)
            {
                throw new EmailAddressAlreadyTakenException();
            }

            var hashedPassword = _passwordHashingService.SaltAndHash(plainTextPassword);
            var user = User.Register(email, hashedPassword);
            _userRepository.Add(user);

            return user;
        }
    }
}