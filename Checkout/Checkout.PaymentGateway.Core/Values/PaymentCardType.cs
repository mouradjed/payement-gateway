
using Checkout.PaymentGateway.Common.Validation;
using System;

namespace Checkout.PaymentGateway.Core.Values
{
    public class PaymentCardType : Enumeration<PaymentCardTypeEnum>
    {
        private PaymentCardType(PaymentCardTypeEnum type): base(type)
        {
        }

        public static Result<PaymentCardType> Create(string raw)
        {
            if (!Enum.TryParse(raw, out PaymentCardTypeEnum value))
            {
                return Result<PaymentCardType>.Fail<PaymentCardType>(AppError.BadPaymentCardTypeValue);
            }
            return Result<PaymentCardType>.Ok(new PaymentCardType(value));
        }

        public static PaymentCardType Visa = new PaymentCardType(PaymentCardTypeEnum.Visa);
        public static PaymentCardType MasterCard = new PaymentCardType(PaymentCardTypeEnum.MasterCard);
        public static PaymentCardType Amex = new PaymentCardType(PaymentCardTypeEnum.Amex);
    }
}

public enum PaymentCardTypeEnum
{
    Visa,
    MasterCard,
    Amex
}
