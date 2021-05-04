using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.PaymentGateway.Common.Dtos
{
    public class CardInfo
    {
        public string MaskedNumber { get; set; }
        public string MaskedExpiry { get; set; }
        public string MaskedCvv { get; set; }
        public string CardType { get; set; }

    }
}
