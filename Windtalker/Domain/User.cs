using System;
using Windtalker.Domain.Exceptions;

namespace Windtalker.Domain
{
    public class User : EntityBase
    {
        protected User()
        {
        }

        public User(Guid id, string email, string hashedPassword, DateTime dateRegistered)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new DomainException("Email cannot be null");
            }

            Id = id;
            Email = email;
            HashedPassword = hashedPassword;
            DateCreated = dateRegistered;
        }

        public string Email { get; set; }

        public string HashedPassword { get; set; }

        public static User Register(string email, string hashedPassword, DateTime dateRegistered)
        {
            return new User(Guid.NewGuid(), email, hashedPassword, dateRegistered);
        }
    }
}
