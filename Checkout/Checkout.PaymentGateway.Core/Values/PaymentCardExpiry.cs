using Checkout.PaymentGateway.Common.Validation;
using System;

namespace Checkout.PaymentGateway.Core.Values
{
    public struct PaymentCardExpiry : IEquatable<PaymentCardExpiry>
    {
        public readonly int Month;
        public readonly int Year;

        public static Result<PaymentCardExpiry> Create(int month, int year)
        {
            if (!_validate(month, year))
            {
                return Result<PaymentCardExpiry>.Fail<PaymentCardExpiry>(AppError.BadPaymentCardExpiryValue);
            }
            return Result<PaymentCardExpiry>.Ok(new PaymentCardExpiry(month, year));
        }

        public static Result<PaymentCardExpiry> Create(string month, string year)
        {
            if (!int.TryParse(month, out int monthValue))
            {
                return Result<PaymentCardExpiry>.Fail<PaymentCardExpiry>(AppError.BadPaymentCardExpiryValueFormat);
            }

            if (!int.TryParse(year, out int yearValue))
            {
                return Result<PaymentCardExpiry>.Fail<PaymentCardExpiry>(AppError.BadPaymentCardExpiryValueFormat);
            }
            return Create(monthValue, yearValue);
        }

        private PaymentCardExpiry(int month, int year): this()
        {
            Month = month;
            Year = year;
        }

        private static bool _validate(int month, int year)
        {
            var currentMonth = DateTime.Now.Month;
            var currentYear = DateTime.Now.Year;
            if (month < 0 || month > 12)
            {
                return false;
            }

            if (year < currentYear || year > 9999)
            {
                return false;
            }

            if (year == currentYear && month < currentMonth)
            {
                return false;
            }
            return true;
        }

        public bool Equals(PaymentCardExpiry other)
        {
            return Month == other.Month && Year == other.Year;
        }
    }
}
