using Checkout.PaymentGateway.Core.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.PaymentGateway.Core.Responses
{
    public class AbortedPaymentResponse: PaymentResponse
    {
        public AbortedPaymentResponse(string reason) : base(PaymentStatus.Aborted, reason)
        {
        }
    }
}
