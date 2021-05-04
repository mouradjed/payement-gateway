using Checkout.PaymentGateway.Common.Validation;
using System;
using System.Globalization;

namespace Checkout.PaymentGateway.Core.Values
{
    public struct PaymentAmount : IEquatable<PaymentAmount>
    {
        public readonly decimal Value;

        public static Result<PaymentAmount> Create(decimal amount)
        {
            if (amount <= 0)
            {
                return Result<PaymentAmount>.Fail<PaymentAmount>(AppError.BadPaymentAmountValue);
            }
            return Result<PaymentAmount>.Ok(new PaymentAmount(amount));
        }

        public static Result<PaymentAmount> Create(string amount)
        {
            if (!decimal.TryParse(amount, NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out decimal value))
            {
                return Result<PaymentAmount>.Fail<PaymentAmount>(AppError.BadPaymentAmountValueFormat);
            }

            if (value <= 0 )
            {
                return Result<PaymentAmount>.Fail<PaymentAmount>(AppError.BadPaymentAmountValueValue);
            }

            return Create(value);
        }
        private PaymentAmount(decimal amount)
        {
            Value = amount;
        }

        public bool Equals(PaymentAmount other)
        {
            return Value == other.Value;
        }
    }
}
