using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.PaymentGateway.Common.Dtos
{
    public class PaymentResponseDto
    {
        public Guid PaymentId { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
    }
}
