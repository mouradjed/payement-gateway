using Checkout.PaymentGateway.Common.Validation;
using System;

namespace Checkout.PaymentGateway.Common.ErrorHandling
{
    public class PaymentNotFoundException : BusinessException
    {
        public PaymentNotFoundException(Guid paymentId):base(new AppError(AppErrorEnum.PaymentNotFoundException, 
                                                                          AppErrorLevelEnum.Business,   
                                                                          string.Format(ErrorMessages.PaymentNotFoundErrorMessage, paymentId)))
        {

        }
    }
}
