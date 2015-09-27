using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class UserService
    {
        private static IUserRepository UserRepository;

        public UserService(IUserRepository userRepository)
        {
            UserRepository = userRepository;
        }

        public ServiceResult<User> Register(User user)
        {
            try
            {
                var existedUser = UserRepository.UserExists(user);
                if (existedUser.Success == false)
                {
                    var registerUser = UserRepository.Register(user);
                    if (registerUser.Success == true)
                    {
                        return ServiceResult<User>.SuccessFunc(registerUser.Message, user);
                    }
                    else
                    {
                        return ServiceResult<User>.ErrorFunc(registerUser.Message, user);
                    }
                }
                else
                {
                    return ServiceResult<User>.ErrorFunc(existedUser.Message, user);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return null;
        }

        public ServiceResult<ForLogIn> LogIn(ForLogIn user)
        {
            try
            {
                var existedUser = UserRepository.UserExistsForLogIn(user);
                if(existedUser.Success==true)
                {
                    return ServiceResult<ForLogIn>.SuccessFunc("Welcome " + user.UserName + ", you're logged in", user);
                }
                else
                {
                    return ServiceResult<ForLogIn>.ErrorFunc("Sorry, " + user.UserName + " was not found.", user);
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
