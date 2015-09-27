using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer;
using System.Diagnostics;
using System.Data.Entity;

namespace DataAccessLayer.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        TestContext db = new TestContext();


        public IEnumerable<User> Users
        {
            get
            {
                return db.Users.ToList<User>();
            }
        }

        public IEnumerable<Question> Questions
        {
            get
            {
                return db.Questions.ToList<Question>();
            }
        }

        public RepositoryResult<bool> CreateQuestion(Question question)
        {
            try
            {
                var questionExists = db.Questions.FirstOrDefault(i => i.TestQuestion == question.TestQuestion);
                if (questionExists == null)
                {
                    db.Questions.Add(question);
                    db.SaveChanges();
                    return RepositoryResult<bool>.SuccessFunc();
                }
                else
                {
                    return RepositoryResult<bool>.ErrorFunc();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return null;
        }
        public RepositoryResult<bool> CreateQuestionAnswer(List<string> Answers)
        {
            try
            {
                var MaxAnswers = db.Answers.Count() + 1;
                for (int i = 0; i < Answers.Count; i++)
                {
                    Answer NewAnswer = new Answer();
                    NewAnswer.AnswerID = MaxAnswers + i;
                    NewAnswer.QuestionAnswer = Answers[i];
                    db.Answers.Add(NewAnswer);

                    QuestionAnswer NewQuestionAnswer = new QuestionAnswer();
                    NewQuestionAnswer.QuestionID = db.Questions.Count();
                    NewQuestionAnswer.AnswerID = NewAnswer.AnswerID;
                    db.QuestionAnswers.Add(NewQuestionAnswer);
                }
                db.SaveChanges();

                return RepositoryResult<bool>.SuccessFunc();
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return RepositoryResult<bool>.ErrorFunc();
            }
        }

        public RepositoryResult<User> UserExists(int id)
        {
            try
            {
                var userExists = db.Users.FirstOrDefault(i => i.ID == id);
                if (userExists != null)
                {
                    return RepositoryResult<User>.SuccessFunc("", userExists);
                }
                else
                {
                    return RepositoryResult<User>.ErrorFunc();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return null;
        }

        public RepositoryResult<User> EditUser(User user)
        {
            try
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();

                return RepositoryResult<User>.SuccessFunc("", user);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return RepositoryResult<User>.ErrorFunc("", user);
            }
        }

        public RepositoryResult<Question> QuestionExists(int id)
        {
            try
            {
                var questionExists = db.Questions.FirstOrDefault(i => i.ID == id);
                if(questionExists!=null)
                {
                    return RepositoryResult<Question>.SuccessFunc("", questionExists);
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

        public RepositoryResult<bool> EditQuestion(Question question, List<string> InputAnswers)
        {
            try
            {
                db.Entry(question).State = EntityState.Modified;
                db.SaveChanges();

                var getQuestion = db.QuestionAnswers;
                var getCurrentQuestionAnswers = getQuestion.Where(i => i.QuestionID == question.ID);


                int Counter = 0;
                foreach (var realAnswer in getCurrentQuestionAnswers)
                {
                    Answer RealAnswer = realAnswer.Answers;
                    RealAnswer.QuestionAnswer = InputAnswers[Counter];
                    Counter++;

                    db.Entry(RealAnswer).State = EntityState.Modified;
                }
                db.SaveChanges();

                return RepositoryResult<bool>.SuccessFunc();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return RepositoryResult<bool>.ErrorFunc();
            }
        }

        public RepositoryResult<bool>RemoveQuestion(int id)
        {
            try
            {
                var RemoveQuestion = db.Questions.Find(id);
                db.Questions.Remove(RemoveQuestion);

                var questionAnswers = db.QuestionAnswers;
                var getCurrentQuestionAnswers = questionAnswers.Where(i => i.QuestionID == id);
                
                foreach (var answer in getCurrentQuestionAnswers)
                {
                    db.Answers.Remove(answer.Answers);
                }

                db.SaveChanges();

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
