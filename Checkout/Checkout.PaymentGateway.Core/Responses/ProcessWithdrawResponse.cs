using Checkout.PaymentGateway.Core.Values;
using System;

namespace Checkout.PaymentGateway.Core.Responses
{
    public class ProcessWithdrawResponse
    {
        public Guid OperationId { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }

}
