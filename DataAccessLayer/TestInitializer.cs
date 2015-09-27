using BusinessLogicLayer.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class TestInitializer:DropCreateDatabaseAlways<TestContext>
    {
        protected override void Seed(TestContext context)
        {
            var questions = new List<Question>()
            {
                new Question{TestQuestion="რომელ წელს შეიქმნა პროგრამირების ენა C#?", /*Answer1="1995", Answer2="2001", Answer3="2000", Answer4="2002",*/ CorrectAnswer="2000", TestName="Programming", DifficultyLevel="2", Grade=5},
                new Question{TestQuestion="რომელ წელს შეიქმნა პროგრამირების ენა Java?",  /*Answer1="1995", Answer2="2001", Answer3="2000", Answer4="2002",*/ CorrectAnswer="1995", TestName="Programming", DifficultyLevel="2", Grade=5},
                new Question{TestQuestion="რამდენი გრადუსია სამკუთხედის კუთხეების ჯამი?", /*Answer1="360", Answer2="270", Answer3="90", Answer4="180",*/ CorrectAnswer="180", TestName="Mathematics", DifficultyLevel="1", Grade=2}
            };
            questions.ForEach(s => context.Questions.Add(s));
            context.SaveChanges();

            var users = new List<User>()
            {
                new User{UserName="John", Email="John@gmail.com", UserPassword="JohnPassword", UserRepeatPassword="JohnPassword", IsActive=true},
                new User{UserName="Jane", Email="Jane@gmail.com", UserPassword="JanePassword", UserRepeatPassword="JanePassword", IsActive=true},
                new User{UserName="Mike", Email="Mike@gmail.com", UserPassword="MikePassword", UserRepeatPassword="MikePassword", IsActive=true}
            };
            users.ForEach(s => context.Users.Add(s));
            context.SaveChanges();

            var answers = new List<Answer>()
            {
                new Answer{AnswerID=1, QuestionAnswer="1995"},
                new Answer{AnswerID=2, QuestionAnswer="2001"},
                new Answer{AnswerID=3, QuestionAnswer="2000"},
                new Answer{AnswerID=4, QuestionAnswer="2002"},
                new Answer{AnswerID=5, QuestionAnswer="1995"},
                new Answer{AnswerID=6, QuestionAnswer="2001"},
                new Answer{AnswerID=7, QuestionAnswer="2000"},
                new Answer{AnswerID=8, QuestionAnswer="2002"},
                new Answer{AnswerID=9, QuestionAnswer="360"},
                new Answer{AnswerID=10, QuestionAnswer="270"},
                new Answer{AnswerID=11, QuestionAnswer="90"},
                new Answer{AnswerID=12, QuestionAnswer="180"}
            };
            answers.ForEach(s => context.Answers.Add(s));
            context.SaveChanges();

            var questionAnswers = new List<QuestionAnswer>()
            {
                new QuestionAnswer{QuestionID=1, AnswerID=1},
                new QuestionAnswer{QuestionID=1, AnswerID=2},
                new QuestionAnswer{QuestionID=1, AnswerID=3},
                new QuestionAnswer{QuestionID=1, AnswerID=4},
                
                new QuestionAnswer{QuestionID=2, AnswerID=5},
                new QuestionAnswer{QuestionID=2, AnswerID=6},
                new QuestionAnswer{QuestionID=2, AnswerID=7},
                new QuestionAnswer{QuestionID=2, AnswerID=8},

                
                new QuestionAnswer{QuestionID=3, AnswerID=9},
                new QuestionAnswer{QuestionID=3, AnswerID=10},
                new QuestionAnswer{QuestionID=3, AnswerID=11},
                new QuestionAnswer{QuestionID=3, AnswerID=12},
            };
            questionAnswers.ForEach(s => context.QuestionAnswers.Add(s));
            context.SaveChanges();
        }
    }
}
