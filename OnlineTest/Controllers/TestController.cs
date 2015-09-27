using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer;
using BusinessLogicLayer.Model;
using BusinessLogicLayer.Services;
using System.Data.Entity;
using OnlineTest.Infrastructure;
using System.Diagnostics;
using System.Threading;


namespace OnlineTest.Controllers
{

    [Authorize]
    public class TestController : Controller
    {
        public QuestionService questionService;
        public TestController(QuestionService questionService)
        {
            this.questionService = questionService;
        }
        //public IQuestionService questionService;
        //public IUserService userService;

        //public TestController(IQuestionService questionService, IUserService userService)
        //{
        //    this.questionService = questionService;
        //    this.userService = userService;
        //}


        public ActionResult Start()
        {
            return View(new Question());
        }

        [TimeElapsedFilter]
        public ActionResult Index(Question question, string id)
        {
            var GetNextQuestion = questionService.GetNextQuestion(question, id);
            if(GetNextQuestion.Success)
            {
                return View(GetNextQuestion.Value);
            }
            else
            {
                var testResult = questionService.GetTestResult(question);
                Thread.Sleep(2000);
                return View("TestCompleted", testResult);
            }             
        }

        [CheckTime]
        [HttpPost]
        public ActionResult IndexApplied(Question question)
        {
            var checkAnswer = questionService.CheckAnswer(question);
            ModelState.Clear();
            return RedirectToAction("Index", "Test", question);
        }

        public ActionResult Completed()
        {
            var Complete = questionService.ClearTempClass();
            if(Complete.Success)
                return RedirectToAction("Index", "Home");
            return null;
        }

        public ActionResult Statistics(string searchString)
        {
            var users = questionService.Users.OrderByDescending(i => i.Points);
            if (searchString != null)
            {
                users = questionService.Users.Where(i => i.UserName.ToUpper().Contains(searchString.ToUpper())).OrderByDescending(i => i.Points);
            }

            return View(users);
        }
    }
}