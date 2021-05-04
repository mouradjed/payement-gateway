using Checkout.PaymentGateway.Common.Dtos;
using Checkout.PaymentGateway.Core.Values;
using System;

namespace Checkout.PaymentGateway.Core.Responses
{
    public class FulfilledPaymentResponse : PaymentResponse
    {
        public readonly Guid BankOperationId;
        public readonly Guid PaymentId;
        public FulfilledPaymentResponse(Guid bankId, Guid paymentId, PaymentStatus status, string message): base(status, message)
        {
            BankOperationId = bankId;
            PaymentId = paymentId;
        }

        public override PaymentResponseDto ToDto()
        {
            var fromBase =  base.ToDto();
            fromBase.PaymentId = PaymentId;
            return fromBase;
        }
    }
}
