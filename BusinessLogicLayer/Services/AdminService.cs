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
    public class AdminService
    {
        public IAdminRepository adminRepository;
        public AdminService(IAdminRepository adminRepository)
        {
            this.adminRepository=adminRepository;
        }

        public IEnumerable<User> Users
        {
            get
            {
                return adminRepository.Users;
            }
        }

        public IEnumerable<Question> Questions
        {
            get
            {
                return adminRepository.Questions;
            }
        }

        public ServiceResult<Question> CreateQuestion(Question Question, List<string> Answers)
        {
            try
            {
                var questionCreated=adminRepository.CreateQuestion(Question);
                if(questionCreated.Success==true)
                {
                    var answerCreated = adminRepository.CreateQuestionAnswer(Answers);
                    if(answerCreated.Success==true)
                    {
                        return ServiceResult<Question>.SuccessFunc("Question created successfully", Question);
                    }
                    else
                    {
                        return ServiceResult<Question>.ErrorFunc("Question can not be created", Question);
                    }
                }
                else
                {
                    return ServiceResult<Question>.ErrorFunc("Question already exists", Question);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return null;
        }

        public ServiceResult<User> UserExists(int id)
        {
            try
            {
                if (id > 0)
                {
                    var userExists = adminRepository.UserExists(id);
                    if (userExists.Success)
                    {
                        return ServiceResult<User>.SuccessFunc("Question exists", userExists.Value);
                    }
                    else
                    {
                        return ServiceResult<User>.ErrorFunc("Question not found");
                    }
                }
                else
                {
                    return ServiceResult<User>.ErrorFunc("Question not found");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return null;
        }

        public ServiceResult<User> EditUser(User user)
        {
            try
            {
                var userEdited = adminRepository.EditUser(user);
                return userEdited.Success ? ServiceResult<User>.SuccessFunc("",user) : ServiceResult<User>.ErrorFunc("", user);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return null;
        }

        public ServiceResult<Question> QuestionExists(int id)
        {
            try
            {
                if(id>0)
                {
                    var questionExists = adminRepository.QuestionExists(id);
                    if(questionExists.Success)
                    {
                        return ServiceResult<Question>.SuccessFunc("Question exists", questionExists.Value);
                    }
                    else
                    {
                        return ServiceResult<Question>.ErrorFunc("Question not found");
                    }
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

        public ServiceResult<bool> EditQuestion(Question question, List<string> InputAnswers)
        {
            try
            {
                var questionEdited = adminRepository.EditQuestion(question, InputAnswers);
                if(questionEdited.Success)
                {
                    return ServiceResult<bool>.SuccessFunc();
                }
                else
                {
                    return ServiceResult<bool>.ErrorFunc();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return null;
        }

        public ServiceResult<bool> RemoveQuestion(int id)
        {
            try
            {
                var questionRemoved = adminRepository.RemoveQuestion(id);
                if(questionRemoved.Success)
                {
                    return ServiceResult<bool>.SuccessFunc();
                }
                else
                {
                    return ServiceResult<bool>.ErrorFunc();
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
