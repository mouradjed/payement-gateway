
using System.Collections.Generic;
using System.Linq;

namespace Checkout.PaymentGateway.Common.Validation
{
    public class Result
    {
        public readonly AppError Error;
        public readonly bool IsSuccess;
        protected Result(AppError error = null)
        {
            IsSuccess = error == null;
            Error = error;
        }

        public static Result Fail(AppError error)
        {
            return new Result(error);
        }

        public static Result Ok()
        {
            return new Result();
        }

        public static Result Combine(IEnumerable<Result> results)
        {
            var failed = results.Where(r => !r.IsSuccess);
            if (failed.Any())
            {
                var combinedMessage = string.Join('|', failed.Select(f => f.Error.Message));
                var combinedError = new AppError(AppErrorEnum.CombinedError, AppErrorLevelEnum.Business, combinedMessage);
                return Fail(combinedError);
            }
            return Ok();
        }

    }
    public class Result<T> : Result
    {
        private readonly T _value;
        private Result(T value): base()
        {
            _value = value;
        }

        public T Value
        {
            get 
            {
                if (!IsSuccess)
                {
                    throw new BusinessException(AppError.BadDataAccess);
                }
                return _value;
            }
        }

        private Result(AppError error) : base(error)
        {

        }
        public static Result<U> Ok<U>(U value)
        {
            return new Result<U>(value);
        }
        public static Result<U> Fail<U>(AppError error)
        {
            return new Result<U>(error);
        }
    }


}
