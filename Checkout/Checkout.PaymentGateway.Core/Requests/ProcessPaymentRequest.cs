using Checkout.PaymentGateway.Common.Dtos;

namespace Checkout.PaymentGateway.Core.Requests
{
    public class ProcessPaymentRequest
    {
        public PaymentCardDto Card {get; set;}
        public decimal Amount { get; set; }
        public string Currency { get; set; }
    }
}
