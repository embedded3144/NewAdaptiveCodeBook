using System;
using System.Collections.Generic;
using System.Linq;

namespace Chapter03._3_11
{
    internal class UserRepository : IUserRepository
    {
        private ICollection<User> users;

        public UserRepository()
        {
            users = new List<User>
            {
                new User(Guid.NewGuid()),
                new User(Guid.NewGuid()),
                new User(Guid.NewGuid()),
                new User(Guid.NewGuid())
            };
        }

        public IUser GetById(Guid userId)
        {
            IUser userFound = users.SingleOrDefault(user => user.Id == userId);
            if(userFound == null)
            {
                userFound = new NullUser();
            }
            return userFound;
        }

        User IUserRepository.GetById(Guid guid)
        {
            throw new NotImplementedException();
        }
    }
}