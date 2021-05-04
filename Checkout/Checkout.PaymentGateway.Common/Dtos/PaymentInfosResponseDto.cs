using System;

namespace Checkout.PaymentGateway.Common.Dtos
{
    public class PaymentInfosResponseDto
    {
        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; }
        public Guid PaymentId { get; set; }
        public CardInfo Card { get; set; }
    }
}
