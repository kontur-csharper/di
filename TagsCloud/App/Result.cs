using System;

namespace TagsCloud.App
{
    public class None
    {
        private None()
        {
        }
    }

    public struct Result<T>
    {
        public Result(string error, T value = default)
        {
            Error = error;
            Value = value;
        }

        public static implicit operator Result<T>(T v) => Result.Ok(v);

        public string Error { get; }
        internal T Value { get; }

        public T GetValueOrThrow()
        {
            if (IsSuccess) return Value;
            throw new InvalidOperationException($"No value. Only Error {Error}");
        }

        public bool IsSuccess => Error == null;
    }

    public static class Result
    {
        public static Result<T> AsResult<T>(this T value) => Ok(value);

        public static Result<T> Ok<T>(T value) => new Result<T>(null, value);

        public static Result<None> Ok() => Ok<None>(null);

        public static Result<T> Fail<T>(string e) => new Result<T>(e);

        public static Result<T> Of<T>(Func<T> f, string error = null)
        {
            try
            {
                return Ok(f());
            }
            catch (Exception e)
            {
                return Fail<T>(error ?? e.Message);
            }
        }

        public static Result<None> OfAction(Action f, string error = null)
        {
            try
            {
                f();
                return Ok();
            }
            catch (Exception e)
            {
                return Fail<None>(error ?? e.Message);
            }
        }

        public static Result<TOutput> Then<TInput, TOutput>(
            this Result<TInput> input,
            Func<TInput, TOutput> continuation) =>
            input.Then(inp => Of(() => continuation(inp)));

        public static Result<None> Then<TInput>(
            this Result<TInput> input,
            Action<TInput> continuation) =>
            input.Then(inp => OfAction(() => continuation(inp)));

        public static Result<TOutput> Then<TInput, TOutput>(
            this Result<TInput> input,
            Func<TInput, Result<TOutput>> continuation) =>
            input.IsSuccess
                ? continuation(input.Value)
                : Fail<TOutput>(input.Error);

        public static Result<TInput> OnFail<TInput>(
            this Result<TInput> input,
            Action<string> handleError)
        {
            if (!input.IsSuccess) handleError(input.Error);
            return input;
        }

        public static Result<TInput> ReplaceError<TInput>(
            this Result<TInput> input,
            Func<string, string> replaceError)
        {
            if (input.IsSuccess) return input;
            return Fail<TInput>(replaceError(input.Error));
        }

        public static Result<TInput> RefineError<TInput>(
            this Result<TInput> input,
            string errorMessage) =>
            input.ReplaceError(err => errorMessage + ". " + err);
    }
}