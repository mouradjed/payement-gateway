using Checkout.PaymentGateway.Common.Dtos;
using Checkout.PaymentGateway.Core.Values;

namespace Checkout.PaymentGateway.Core.Requests
{
    public class ProcessWithdrawRequest
    {
        public PaymentCard Card { get; set; }
        public PaymentAmount Amount { get; set; }
        public Currency Currency { get; set; }
    }
}
