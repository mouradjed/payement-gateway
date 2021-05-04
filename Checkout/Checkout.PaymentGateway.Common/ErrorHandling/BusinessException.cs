

using System;

namespace Checkout.PaymentGateway.Common.Validation
{
    public class BusinessException : ApplicationException
    {
        public readonly AppError Error;
        public BusinessException(AppError error): base(error.Message)
           
        {
            Error = error;
        }
    }
}
