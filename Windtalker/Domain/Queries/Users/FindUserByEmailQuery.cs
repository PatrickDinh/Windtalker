using System;
using System.Linq;

namespace Windtalker.Domain.Queries.Users
{
    public class FindUserByEmailQuery : IQuery<User>
    {
        private readonly string _email;

        public FindUserByEmailQuery(string email)
        {
            _email = email;
        }

        public IQueryable<User> Execute(IQueryable<User> source)
        {
            return source.Where(u => u.Email.Equals(_email, StringComparison.OrdinalIgnoreCase));
        }
    }
}