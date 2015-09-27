using BusinessLogicLayer;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        public static TestContext db = new TestContext();
        public TempClass TempClass;

        public IEnumerable<User> Users
        {
            get
            {
                return db.Users.ToList<User>();
            }
        }

        List<Question> AllQuestions = db.Questions.ToList<Question>();

        public Question NextQuetion(int i, string testName, string difficultyLevel, string userName)
        {
            List<Question> Questions = AllQuestions.Where(m => m.TestName == testName && m.DifficultyLevel == difficultyLevel).ToList<Question>();

            if (i >= Questions.Count)
                Questions = null;

            if (Questions != null)
            {
                var tempQuestion = Questions[i];
                tempQuestion.UserName = userName;
                return tempQuestion;
            }
            else
                return null;
        }

        public RepositoryResult<Question> GetNextQuestion(Question question, string id)
        {
            try
            {
                if (id == "Skipped")
                {
                    TempClass.NextQuestionNumber++;
                    TempClass.TotallyAnsweredQuestions++;
                }

                var Question = NextQuetion(question.ID, question.TestName, question.DifficultyLevel, question.UserName);
                if (Question != null)
                {
                    return RepositoryResult<Question>.SuccessFunc("", Question);
                }
                else
                {
                    return RepositoryResult<Question>.ErrorFunc();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return null;
        }

        public RepositoryResult<bool> CheckAnswer(Question question)
        {
            if (question.CorrectAnswer == question.SelectedAnswer)
            {
                TempClass.NextQuestionNumber++;
                TempClass.TotallyAnsweredQuestions++;
                TempClass.GradeForAnswer += question.Grade;

                var user = db.Users.FirstOrDefault(i => i.UserName == question.UserName);
                user.Points += question.Grade;
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();

                Thread.Sleep(2000);
                return RepositoryResult<bool>.SuccessFunc();
            }
            else
            {
                TempClass.NextQuestionNumber = question.ID;
                Thread.Sleep(2000);
                return RepositoryResult<bool>.ErrorFunc();
            }
        }

        public TestResult GetTestResult(Question question)
        {
            TestResult testResult = new TestResult();
            testResult.CategoryName = question.TestName;
            testResult.Date = DateTime.Now;
            testResult.CollectedPoints = TempClass.GradeForAnswer;
            testResult.TestMaxPoints = db.Questions.Where(i => i.TestName == question.TestName).Sum(i => i.Grade);
            testResult.AnsweredQuestions = TempClass.TotallyAnsweredQuestions;

            return testResult;
        }

        public RepositoryResult<bool> ClearTempClass()
        {
            try
            {
                TempClass.NextQuestionNumber = 0;
                TempClass.TotallyAnsweredQuestions = 0;
                TempClass.GradeForAnswer = 0;

                return RepositoryResult<bool>.SuccessFunc();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return RepositoryResult<bool>.ErrorFunc();
            }
        }
    }
}
