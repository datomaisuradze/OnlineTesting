using BusinessLogicLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IAdminRepository
    {
        IEnumerable<User> Users { get; }
        IEnumerable<Question> Questions { get; }
        RepositoryResult<bool> CreateQuestion(Question question);
        RepositoryResult<bool> CreateQuestionAnswer(List<string> Answers);
        RepositoryResult<Question> QuestionExists(int id);
        RepositoryResult<User> UserExists(int id);
        RepositoryResult<User> EditUser(User user);
        RepositoryResult<bool> EditQuestion(Question question, List<string> InputAnswers);
        RepositoryResult<bool> RemoveQuestion(int id);
    }
}
