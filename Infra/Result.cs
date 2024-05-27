using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cafeRecAPI.Infra
{
	public class Result
	{
        public bool Success { get; private set; }
        public string ErrorMessage { get; private set; }
        public Exception Exception { get; private set; }
        public bool Failure => !Success;
        protected Result(bool success, string errorMessage)
        {
            Contracts.Require((success || !string.IsNullOrEmpty(errorMessage)), "Create result");
            Contracts.Require((!success || string.IsNullOrEmpty(errorMessage)), "Create result");

            Success = success;
            ErrorMessage = errorMessage;
            Exception = new Exception(errorMessage);
        }
        protected Result(bool success, Exception exception)
        {
            Contracts.Require((success || exception != null), "Create result");
            Contracts.Require((!success || exception == null), "Create result");

            Success = success;
            Exception = exception;
            ErrorMessage = exception.Message;
        }
        public static Result Fail(string message) => new Result(false, message);
        public static Result Fail(Exception exception) => new Result(false, exception);
        public static Result<T> Fail<T>(string message) => new Result<T>(default(T), false, message);
        public static Result<T> Fail<T>(Exception exception) => new Result<T>(default(T), false, exception);
        public static Result Ok() => new Result(true, String.Empty);
        public static Result<T> Ok<T>(T value) => new Result<T>(value, true, String.Empty);
        public static Result Combine(params Result[] results)
        {
            foreach (Result result in results)
            {
                if (result.Failure)
                    return result;
            }
            return Ok();
        }
    }


    public sealed class Result<T> : Result
    {
        private T _value;
        public T Value
        {
            get
            {
                Contracts.Require(Success, $"Read result for {typeof(T)}");
                return _value;
            }
            private set { _value = value; }
        }

        internal Result()
            : base(false, new Exception("Empty Result"))
        {
        }

        internal Result(T value)
            : base(true, "")
        {
            Value = value;
        }

        internal Result(T value, bool success, string errorMessage)
            : base(success, errorMessage)
        {
            Contracts.Require((value != null || !success), $"Create result for {typeof(T)}");
            Value = value;
        }

        internal Result(T value, bool success, Exception exception)
            : base(success, exception)
        {
            Contracts.Require((value != null || !success), $"Create result for {typeof(T)}");
            Value = value;
        }


        public T ValueOrFallback(T fallbackValue)
        {
            if (fallbackValue == null)
            {
                throw new ArgumentNullException(nameof(fallbackValue));
            }

            if (this.Success)
            {
                return this.Value;
            }
            else
            {
                return fallbackValue;
            }
        }

        public Result<TResult> Select<TResult>(Func<T, TResult> selector)
        {
            _ = selector ?? throw new ArgumentNullException(nameof(selector));

            if (this.Success)
            {
                return new Result<TResult>(selector(this.Value));
            }
            else
            {
                return new Result<TResult>();
            }

        }

        public override bool Equals(object obj)
        {
            var other = obj as Result<T>;
            return other == null ? false : object.Equals(this.Value, other.Value);
        }

        public override int GetHashCode()
        {
            return this.Success ? this.Value.GetHashCode() : 0;
        }
    }

    internal static class Contracts
    {
        internal static void Require(bool precondition, string operation = "")
        {
            if (!precondition)
                throw new ResultException($"Invalid operation - {operation}");
        }
    }

    [Serializable]
    public sealed class ResultException : Exception
    {
        public ResultException(string message) : base(message)
        {
        }
    }
}
