using System;
using System.Linq;

namespace Windtalker.Plumbing
{
    public interface IResult
    {
        bool WasSuccessful { get; }
        string[] Errors { get; }
        bool WasFailure { get; }
        string ErrorString { get; }
    }

    public class Result : IResult
    {
        private string[] _errors;

        private Result()
        {
        }

        public bool WasSuccessful { get; private set; }
        public bool WasFailure => !WasSuccessful;
        public string[] Errors => _errors.ToArray();
        public string ErrorString => string.Join(Environment.NewLine, _errors);

        public static Result<T, TErrorData> Failed<T, TErrorData>(TErrorData value, params string[] errors)
        {
            return Result<T, TErrorData>.Failed(value, errors);
        }

        public static Result Failed(params string[] errors)
        {
            return new Result {_errors = errors.ToArray()};
        }

        public static Result Failed(params IResult[] becauseOf)
        {
            return new Result {_errors = becauseOf.SelectMany(b => b.Errors).ToArray()};
        }

        public static Result Success()
        {
            return new Result {WasSuccessful = true};
        }

        public static Result<T> Success<T>(T value)
        {
            return Result<T>.Success(value);
        }

        public static Result From(params Result[] results)
        {
            var failed = results.Where(r => !r.WasSuccessful).ToArray();
            if (failed.Length == 0)
                return Success();
            return Failed(failed.SelectMany(f => f.Errors).ToArray());
        }
    }

    public class Result<T> : IResult
    {
        protected string[] _errors;
        private T _value;

        protected Result()
        {
        }

        public T Value
        {
            get
            {
                if (!WasSuccessful)
                    throw new Exception("No value as the operation was not successful");
                return _value;
            }
        }

        public string[] Errors => _errors?.ToArray();
        public string ErrorString => _errors != null ? string.Join(Environment.NewLine, _errors) : null;
        public bool WasSuccessful { get; private set; }
        public bool WasFailure => !WasSuccessful;

        public static Result<T> Failed(T data, params string[] errors)
        {
            return new Result<T> {_errors = errors, _value = data};
        }

        public static Result<T> Failed(T data)
        {
            return new Result<T> {_errors = new string[0], _value = data};
        }

        public static Result<T> Failed()
        {
            return new Result<T> {_errors = new string[0]};
        }

        public static Result<T> Failed(params string[] errors)
        {
            return new Result<T> {_errors = errors.ToArray()};
        }

        public static Result<T> Failed(params IResult[] becauseOf)
        {
            return new Result<T> {_errors = becauseOf.SelectMany(b => b.Errors).ToArray()};
        }

        public static Result<T> Success(T value)
        {
            return new Result<T> {WasSuccessful = true, _value = value};
        }

        public static implicit operator Result<T>(T value)
        {
            return Success(value);
        }

        public static implicit operator T(Result<T> result)
        {
            return result.Value;
        }

        public T ValueOr(T def)
        {
            return WasSuccessful ? Value : def;
        }

        public override string ToString()
        {
            return WasSuccessful
                ? "" + Value
                : ErrorString;
        }

        public static Result<T> From<TIn>(Result<TIn> result) where TIn : T
        {
            return result.WasSuccessful
                ? Success(result.Value)
                : Failed(result, new string[0]);
        }
    }

    public class Result<T, TErrorData> : IResult
    {
        private TErrorData _errorData;
        protected string[] _errors;
        private T _value;

        public T Value
        {
            get
            {
                if (!WasSuccessful)
                    throw new Exception("No value as the operation was not successful");
                return _value;
            }
        }

        public TErrorData ErrorData
        {
            get
            {
                if (WasSuccessful)
                    throw new Exception("No value as the operation was successful");
                return _errorData;
            }
        }

        public bool WasSuccessful { get; protected set; }
        public string[] Errors => _errors?.ToArray();
        public bool WasFailure => !WasSuccessful;
        public string ErrorString => _errors != null ? string.Join(Environment.NewLine, _errors) : null;

        public static Result<T, TErrorData> Failed(TErrorData data, params string[] errors)
        {
            return new Result<T, TErrorData> {WasSuccessful = false, _errorData = data, _errors = errors};
        }

        public static Result<T, TErrorData> Failed(params string[] errors)
        {
            return new Result<T, TErrorData> {WasSuccessful = false, _errors = errors.ToArray()};
        }

        public static Result<T, TErrorData> Success(T value)
        {
            return new Result<T, TErrorData> {WasSuccessful = true, _value = value};
        }

        public static Result<T, TErrorData> Success()
        {
            return new Result<T, TErrorData> {WasSuccessful = true};
        }
    }
}