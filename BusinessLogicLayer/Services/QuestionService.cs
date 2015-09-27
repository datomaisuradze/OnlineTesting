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
    public class QuestionService
    {
        public IQuestionRepository questionRepository;
        public QuestionService(IQuestionRepository questionRepository)
        {
            this.questionRepository = questionRepository;
        }

        public IEnumerable<User> Users
        {
            get
            {
                return questionRepository.Users;
            }
        }

        public ServiceResult<Question> GetNextQuestion(Question question, string id)
        {
            try
            {
                var GetQuestion = questionRepository.GetNextQuestion(question, id);
                if (GetQuestion.Success)
                {
                    return ServiceResult<Question>.SuccessFunc("Question found", GetQuestion.Value);
                }
                else
                {
                    return ServiceResult<Question>.ErrorFunc("Question not found");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return null;
        }

        public TestResult GetTestResult(Question question)
        {
            return questionRepository.GetTestResult(question);
        }

        public ServiceResult<bool> CheckAnswer(Question question)
        {
            try
            {
                var answerChecked = questionRepository.CheckAnswer(question);
                if (answerChecked.Success)
                    return ServiceResult<bool>.SuccessFunc();
                else
                    return ServiceResult<bool>.ErrorFunc();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return ServiceResult<bool>.ErrorFunc();
            }
        }

        public ServiceResult<bool> ClearTempClass()
        {
            try
            {
                var Clear = questionRepository.ClearTempClass();
                if (Clear.Success)
                    return ServiceResult<bool>.SuccessFunc();
                else
                    return null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return ServiceResult<bool>.ErrorFunc();
            }
        }
    }
}
