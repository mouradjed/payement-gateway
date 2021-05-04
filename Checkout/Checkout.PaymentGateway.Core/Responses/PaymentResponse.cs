using Checkout.PaymentGateway.Common.Dtos;
using Checkout.PaymentGateway.Core.Values;
using System;

namespace Checkout.PaymentGateway.Core.Responses
{
    public class PaymentResponse
    {
        public readonly PaymentStatus Status;
        public readonly string Message;
        public PaymentResponse(PaymentStatus status, string message)
        {
            Status = status;
            Message = message;
        }

        public virtual PaymentResponseDto ToDto()
        {
            return new PaymentResponseDto
            {
                Status = Status.Label,
                Message = Message
            };
        }
    }
}
