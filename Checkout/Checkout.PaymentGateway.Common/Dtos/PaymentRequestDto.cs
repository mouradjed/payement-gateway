using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.PaymentGateway.Common.Dtos
{
    public class PaymentRequestDto
    {
        public PaymentCardDto Card { get; set; }
        public string Amount { get; set; }
        public string Currency { get; set; }
    }
}
