using BusinessLogicLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IQuestionRepository
    {
        IEnumerable<User> Users { get; }
        RepositoryResult<Question> GetNextQuestion(Question question, string id);
        RepositoryResult<bool> ClearTempClass();
        TestResult GetTestResult(Question question);
        RepositoryResult<bool> CheckAnswer(Question question);
    }
}
