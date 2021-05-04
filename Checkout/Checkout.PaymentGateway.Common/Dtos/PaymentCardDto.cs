namespace Checkout.PaymentGateway.Common.Dtos
{
    public class PaymentCardDto
    {
        public string Number { get; set; }
        public PaymentCardExpiryDto Expiry { get; set; }
        public string CVV { get; set; }
        public string CardType { get; set; }
    }
}
