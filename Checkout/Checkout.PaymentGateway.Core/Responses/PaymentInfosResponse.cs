using Checkout.PaymentGateway.Common.Dtos;
using Checkout.PaymentGateway.Core.Entities;
using System;

namespace Checkout.PaymentGateway.Core.Responses
{
    public class PaymentInfosResponse
    {
        public readonly DateTime PaymentDate;
        public readonly Decimal Amount;
        public readonly Guid PaymentId;
        public readonly CardInfo Card;
        public PaymentInfosResponse(Payment payment)
        {
            PaymentDate = payment.PaymentDate;
            Amount = payment.Amount.Value;
            PaymentId = payment.Id;
            Card = payment.Card.ToMasked();
        }

        public PaymentInfosResponseDto ToDto()
        {
            return new PaymentInfosResponseDto
            {
                Amount = Amount,
                Card = Card,
                PaymentId = PaymentId,
                PaymentDate = PaymentDate
            };
        }
    }
}
