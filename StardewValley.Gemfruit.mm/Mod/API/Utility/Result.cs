using System;

namespace Gemfruit.Mod.API.Utility
{
    public class Result<T, TErr>
    {
        private T _value;
        private TErr _error;

        public bool IsOk()
        {
            return _value != null;
        }

        public bool IsError()
        {
            return _error != null;
        }
        
        private Result(T value)
        {
            _value = value;
            _error = default;
        }

        private Result(TErr error)
        {
            _value = default;
            _error = error;
        }

        public static Result<T, Exception> FromException(Exception e)
        {
            return new Result<T, Exception>(e);
        }

        public static Result<T, TErr> FromValue(T t)
        {
            return new Result<T, TErr>(t);
        }

        public T Unwrap()
        {
            return _value;
        }

        public TErr Error()
        {
            return _error;
        }
        
        
    }
}