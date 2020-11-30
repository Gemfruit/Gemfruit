using System;
using Gemfruit.Mod.API.Exceptions;

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

        public bool Contains(T val)
        {
            return _value.Equals(val);
        }

        public bool ContainsErr(TErr err)
        {
            return _error.Equals(err);
        }

        public Result<TNew, TErr> Map<TNew>(Func<T, TNew> op)
        {
            return IsError() ? new Result<TNew, TErr>(_error) : new Result<TNew, TErr>(op(_value));
        }

        public TNew MapOr<TNew>(TNew def, Func<T, TNew> op)
        {
            return IsOk() ? op(_value) : def;
        }

        public TNew MapOrElse<TNew>(Func<TErr, TNew> def, Func<T, TNew> op)
        {
            return IsOk() ? op(_value) : def(_error);
        }

        public Result<T, TNewErr> MapError<TNewErr>(Func<TErr, TNewErr> op)
        {
            return IsOk() ? new Result<T, TNewErr>(_value) : new Result<T, TNewErr>(op(_error));
        }

        public Result<TNew, TErr> And<TNew>(Result<TNew, TErr> res)
        {
            return IsError() ? new Result<TNew, TErr>(_error) : res;
        }

        public Result<TNew, TErr> AndThen<TNew>(Func<T, Result<TNew, TErr>> op)
        {
            return IsOk() ? new Result<TNew, TErr>(_error) : op(_value);
        }

        public Result<T, TNewErr> Or<TNewErr>(Result<T, TNewErr> res)
        {
            return IsOk() ? new Result<T, TNewErr>(_value) : res;
        }

        public Result<T, TNewErr> OrElse<TNewErr>(Func<TErr, Result<T, TNewErr>> op)
        {
            return IsOk() ? new Result<T, TNewErr>(_value) : op(_error);
        }

        public T UnwrapOr(T def)
        {
            return IsOk() ? _value : def;
        }

        public T UnwrapOrElse(Func<TErr, T> op)
        {
            return IsOk() ? _value : op(_error);
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

        public T Expect(string message)
        {
            if(IsError())
            {
                throw new ResultExpectationException(message, _error.ToString());
            }

            return _value;
        }

        /// <summary>
        /// Unwraps the <see cref="Result{T,TErr}">Result</see>'s value as its underlying type. This will throw an
        /// exception if null, so checking w/ <see cref="IsError"/> is almost always required.
        /// </summary>
        /// <returns>The underlying value of the <see cref="Result{T,TErr}">Result</see></returns>
        /// <exception cref="InvalidOperationException">Thrown if the <see cref="Result{T,TErr}">Result</see>'s value is <code>null</code>.</exception>
        public T Unwrap()
        {
            if (IsError())
            {
                throw new InvalidOperationException(_error.ToString());
            }
            return _value;
        }

        public TErr ExpectError(string message)
        {
            if (IsOk())
            {
                throw new ResultExpectationException(message, _value.ToString());
            }

            return _error;
        }
        
        public TErr UnwrapError()
        {
            if (IsOk())
            {
                throw new InvalidOperationException(_value.ToString());
            }
            return _error;
        }

        public T UnwrapOrDefault()
        {
            return IsError() ? default : _value;
        }
    }
}