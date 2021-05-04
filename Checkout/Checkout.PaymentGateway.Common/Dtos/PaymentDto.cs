namespace Checkout.PaymentGateway.Common.Dtos
{
    public class PaymentDto
    {
        public int Id { get; set; }
        public string Reference { get; set; }
        public string Currency { get; set; }
        public PaymentCardDto Card { get; set; }
        public string Status { get; set; }
        public decimal Amount { get; set; }
    }
}
