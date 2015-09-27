using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class ServiceResult<T>
    {
        public T Value { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }

        public ServiceResult(bool success, string message, T value)
        {
            this.Value = value;
            this.Message = message;
            this.Success = success;
        }

        public ServiceResult(bool success, string message)
        {
            this.Message = message;
            this.Success = success;
        }

        public ServiceResult(bool success)
        {
            this.Success = success;
        }

        public ServiceResult() { }


        public static ServiceResult<T> SuccessFunc()
        {
            return new ServiceResult<T>(true);
        }
        public static ServiceResult<T> SuccessFunc(string message)
        {
            return new ServiceResult<T>(true, message);
        }
        public static ServiceResult<T> SuccessFunc(string message, T value)
        {
            return new ServiceResult<T>(true, message, value);
        }

        public static ServiceResult<T> ErrorFunc()
        {
            return new ServiceResult<T>(false);
        }
        public static ServiceResult<T> ErrorFunc(string message)
        {
            return new ServiceResult<T>(false, message);
        }
        public static ServiceResult<T> ErrorFunc(string message, T value)
        {
            return new ServiceResult<T>(false, message, value);
        }
    }
}
