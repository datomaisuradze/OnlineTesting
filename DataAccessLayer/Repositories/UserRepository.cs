using BusinessLogicLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer;
using System.Diagnostics;
using BusinessLogicLayer.Interfaces;

namespace DataAccessLayer.Repositories
{
    public class UserRepository : IUserRepository
    {
        public RepositoryResult<User> Register(User user)
        {
            try
            {
                using (var db = new TestContext())
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                }

                return RepositoryResult<User>.SuccessFunc("User " + user.UserName + " registered successfully", user);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return RepositoryResult<User>.ErrorFunc("User " + user.UserName + " can not be resigtered", user);
            }
        }

        public RepositoryResult<User> UserExists(User user)
        {
            try
            {
                using (var db = new TestContext())
                {
                    var userExists = db.Users.FirstOrDefault(i => i.UserName == user.UserName);
                    if (userExists != null)
                    {
                        return RepositoryResult<User>.SuccessFunc("User " + user.UserName + " exists", user);
                    }
                    else
                    {
                        return RepositoryResult<User>.ErrorFunc("User " + user.UserName + " doesn't exist", user);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return null;
        }

        public RepositoryResult<ForLogIn> UserExistsForLogIn(ForLogIn user)
        {
            try
            {
                using(var db=new TestContext())
                {
                    var userExists = db.Users.FirstOrDefault(i => i.UserName == user.UserName && i.UserPassword == user.UserPassword);
                    if(userExists!=null)
                    {
                        return RepositoryResult<ForLogIn>.SuccessFunc("User " + user.UserName + " exists", user);
                    }
                    else
                    {
                        return RepositoryResult<ForLogIn>.ErrorFunc("User " + user.UserName + " doesn't exist", user);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return null;
        }
    }
}
