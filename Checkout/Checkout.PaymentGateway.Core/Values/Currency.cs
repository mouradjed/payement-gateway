using Checkout.PaymentGateway.Common.Validation;
using System;

namespace Checkout.PaymentGateway.Core.Values
{
    public class Currency : Enumeration<CurrencyTypeEnum>
    {
        public readonly int Code;
        
        private Currency(int code, CurrencyTypeEnum type): base(type)
        {
            Code = code;
        }

        public static Result<Currency> Create(string raw)
        {
            if (!Enum.TryParse(raw, out CurrencyTypeEnum value))
            {
                return Result<Currency>.Fail<Currency>(AppError.BadCurrenyValue);
            }
            return Result<Currency>.Ok(new Currency((int)value, value));
        }

        public static Currency EUR = new Currency(978, CurrencyTypeEnum.EUR);
        public static Currency USD = new Currency(840, CurrencyTypeEnum.USD);
    }

    public enum CurrencyTypeEnum
    {
        EUR = 978,
        USD = 840
    }
}
