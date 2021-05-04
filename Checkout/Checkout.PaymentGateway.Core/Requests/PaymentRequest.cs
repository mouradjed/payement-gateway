using Checkout.PaymentGateway.Common.Dtos;
using Checkout.PaymentGateway.Common.Validation;
using Checkout.PaymentGateway.Core.Values;

namespace Checkout.PaymentGateway.Core.Entities
{
    public class PaymentRequest 
    {
        public readonly PaymentCard Card;
        public readonly PaymentAmount Amount;
        public readonly Currency Currency;
        private PaymentRequest(PaymentCard paymentCard, PaymentAmount amount, Currency currency)
        {
            Card = paymentCard;
            Amount = amount;
            Currency = currency;
        }

        public static Result<PaymentRequest> Create(PaymentRequestDto dto)
        {
            var cardOrError = PaymentCard.Create(dto.Card);
            var amountOrError = PaymentAmount.Create(dto.Amount);
            var currencyOrError = Currency.Create(dto.Currency);

            var all = Result.Combine(new Result[] { cardOrError, amountOrError, currencyOrError });
            if (!all.IsSuccess)
            {
                return Result<PaymentRequest>.Fail<PaymentRequest>(all.Error);
            }
            var good = new PaymentRequest(cardOrError.Value, amountOrError.Value, currencyOrError.Value);
            return Result<PaymentRequest>.Ok(good);
        }
    }
}
