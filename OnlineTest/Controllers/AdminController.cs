using BusinessLogicLayer.Model;
using BusinessLogicLayer.Services;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace OnlineTest.Controllers
{
    public class AdminController : Controller
    {
        public AdminService adminService;
        public AdminController(AdminService adminService)
        {
            this.adminService = adminService;
        }

        public ActionResult AdminLogIn()
        {
            return View(new ForLogIn());
        }

        [HttpPost]
        public ActionResult AdminLogIn(ForLogIn user)
        {
            if (user.UserName == "admin" && user.UserPassword == "admin")
            {
                return View("AdminLoggedIn");
            }
            else
            {
                ModelState.AddModelError("", "Username or password is not correct, please try again");
                return View(user);
            }
        }

        public ActionResult Index(string searchString)
        {
            var users = adminService.Users;
            if (!string.IsNullOrEmpty(searchString))
            {
                users = adminService.Users.Where(i => i.UserName.ToUpper().Contains(searchString.ToUpper())).ToList<User>();
            }
            return View(users);
        }

        public ActionResult EditUser(int id)
        {
            try
            {
                var GetUser = adminService.UserExists(id);
                if (GetUser.Success == true)
                {
                    return View(GetUser.Value);
                }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult EditUser(User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userEdited = adminService.EditUser(user);
                    return userEdited.Success ? RedirectToAction("Index") : (ActionResult)View("EditUser", userEdited.Value);
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(user);
            }
        }


        public ActionResult Questions()
        {
            return View("SelectTestName");
        }

        [HttpPost]
        public ActionResult Questions(string testName, string testLevel)
        {
            var questions = adminService.Questions.Where(i => i.TestName == testName && i.DifficultyLevel == testLevel).ToList<Question>();

            return View(questions);
        }

        public ActionResult CreateQuestion()
        {
            return View(new Question());
        }

        [HttpPost]
        public ActionResult CreateQuestion(Question question, List<string> Answers)
        {
            try
            {
                var questionCreated = adminService.CreateQuestion(question, Answers);
                if (questionCreated.Success == true)
                {
                    return RedirectToAction("Questions");
                }
                else
                {
                    ModelState.AddModelError("", questionCreated.Message);
                    return View("CreateQuestion");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(question);
            }
        }

        public ActionResult EditQuestion(int id)
        {
            try
            {
                var GetQuestion=adminService.QuestionExists(id);
                if(GetQuestion.Success==true)
                {
                    return View(GetQuestion.Value);
                }
                else
                {
                    ModelState.AddModelError("", GetQuestion.Message);
                    return RedirectToAction("Questions");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return RedirectToAction("Questions");
            }
        }

        [HttpPost]
        public ActionResult EditQuestion(Question question, List<string> InputAnswers)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var questionEdited = adminService.EditQuestion(question, InputAnswers);
                    if (questionEdited.Success == true)
                    {
                        return RedirectToAction("Questions");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Question can not be edited");
                        return View(question);
                    }
                }
                else
                {
                    return View(question);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(question);
            }
        }

        public ActionResult RemoveQuestion(int id)
        {
            try
            {
                var GetQuestion = adminService.QuestionExists(id);
                if (GetQuestion.Success == true)
                {
                    return View(GetQuestion.Value);
                }
                else
                {
                    ModelState.AddModelError("", GetQuestion.Message);
                    return RedirectToAction("Questions");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return RedirectToAction("Questions");
            }
        }

        [HttpPost, ActionName("RemoveQuestion")]
        public ActionResult RemoveQuestionAccepted(int id)
        {
            try
            {
                var questionRemoved = adminService.RemoveQuestion(id);
                if(questionRemoved.Success)
                {
                    return RedirectToAction("Questions");
                }
                else
                {
                    ModelState.AddModelError("", "Question can not be removed");
                    return RedirectToAction("RemoveQuestion", new { id = id });
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return RedirectToAction("RemoveQuestion", new { id = id });
            }
        }
    }
}