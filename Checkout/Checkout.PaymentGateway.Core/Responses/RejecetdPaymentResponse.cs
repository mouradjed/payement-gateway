using Checkout.PaymentGateway.Core.Values;

namespace Checkout.PaymentGateway.Core.Responses
{
    public class RejecetdPaymentResponse: PaymentResponse
    {
        public RejecetdPaymentResponse(string reason): base(PaymentStatus.Invalid, reason)
        {
        }
    }
}
