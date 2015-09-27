using BusinessLogicLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IUserRepository
    {
        RepositoryResult<User> Register(User user);
        RepositoryResult<User> UserExists(User user);
        RepositoryResult<ForLogIn> UserExistsForLogIn(ForLogIn user);
    }
}
