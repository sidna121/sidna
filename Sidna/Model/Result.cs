using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sidna.Model
{
    public class Result
    {
        public bool Error { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }

        public static Result set(bool success = false, bool error = false, string message = "")
            => new Result { Success = success, Error = error, Message = message };
        public static Result Failure(bool error = false, string message = "")
            => new Result { Success = false, Error = error, Message = message };
        public static Result Successful(bool error = false, string message = "")
            => new Result { Success = true, Error = error, Message = message };


        protected object _data;

        public object GetData() => _data;

    }

    public class Result<T> : Result
    {

        public T Data
        {
            get { return (T)_data; }
            set { _data = value; }
        }

        public static Result<T> set(bool success= false, bool error=false, string message = "", T data = default(T))
            => new Result<T> { Success = success, Error = error, Message = message, Data = data};
        public static Result<T> Failure(bool error = false, string message = "", T data = default(T))
            => new Result<T> { Success = false, Error = error, Message = message, Data = data };
        public static Result<T> Successful(bool error = false, string message = "", T data = default(T))
            => new Result<T> { Success = true, Error = error, Message = message, Data = data };
    }
}
