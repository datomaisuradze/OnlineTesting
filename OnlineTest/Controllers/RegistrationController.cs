using BusinessLogicLayer.Model;
using BusinessLogicLayer.Services;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace OnlineTest.Controllers
{
    public class RegistrationController : Controller
    {
        public UserService userService;
        public RegistrationController(UserService UserService)
        {
            this.userService = UserService;
        }


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Registration()
        {
            return View(new User());
        }

        [HttpPost]
        public ActionResult Registration(User user)
        {
            try
            {
                var RegisterUser = userService.Register(user);
                return View("Registered", RegisterUser);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return null;
        }

        public ActionResult LogIn()
        {
            return View(new ForLogIn());
        }

        [HttpPost]
        public ActionResult LogIn(ForLogIn user)
        {
            try
            {
                var userLogIn = userService.LogIn(user);
                if(userLogIn.Success==true)
                {
                    FormsAuthentication.SetAuthCookie(user.UserName, false);
                }
                return View("LoggedIn", userLogIn);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(user);
            }
        }
    }
}