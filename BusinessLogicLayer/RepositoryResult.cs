using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class RepositoryResult<T>
    {
        public T Value { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }

        public RepositoryResult(bool success, string message, T value)
        {
            this.Value = value;
            this.Message = message;
            this.Success = success;
        }

        public RepositoryResult(bool success, string message)
        {
            this.Message = message;
            this.Success = success;
        }

        public RepositoryResult(bool success)
        {
            this.Success = success;
        }

        public RepositoryResult() { }


        public static RepositoryResult<T> SuccessFunc()
        {
            return new RepositoryResult<T>(true);
        }
        public static RepositoryResult<T> SuccessFunc(string message)
        {
            return new RepositoryResult<T>(true, message);
        }
        public static RepositoryResult<T> SuccessFunc(string message, T value)
        {
            return new RepositoryResult<T>(true, message, value);
        }

        public static RepositoryResult<T> ErrorFunc()
        {
            return new RepositoryResult<T>(false);
        }
        public static RepositoryResult<T> ErrorFunc(string message)
        {
            return new RepositoryResult<T>(false, message);
        }
        public static RepositoryResult<T> ErrorFunc(string message, T value)
        {
            return new RepositoryResult<T>(false, message, value);
        }
    }
}
