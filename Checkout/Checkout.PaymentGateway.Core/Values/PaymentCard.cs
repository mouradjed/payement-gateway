
using Checkout.PaymentGateway.Common.Dtos;
using Checkout.PaymentGateway.Common.Validation;
using System;
using System.Text.RegularExpressions;

namespace Checkout.PaymentGateway.Core.Values
{
    public struct PaymentCard : IEquatable<PaymentCard>
    {
        public readonly string CardNumber;
        public readonly PaymentCardExpiry Expiry;
        public readonly string Cvv;
        public readonly PaymentCardType CardType;

        

        public static Result<PaymentCard> Create(PaymentCardDto dto)
        {
            var cardTypeOrError = PaymentCardType.Create(dto.CardType);
            
            if (!cardTypeOrError.IsSuccess)
            {
                return Result<PaymentCard>.Fail<PaymentCard>(cardTypeOrError.Error);
            }

            var cardExpiryOrError = PaymentCardExpiry.Create(dto.Expiry.Month, dto.Expiry.Year);

            if (!cardExpiryOrError.IsSuccess)
            {
                return Result<PaymentCard>.Fail<PaymentCard>(cardExpiryOrError.Error);
            }

            if (!_validateCardNumber(dto.Number, cardTypeOrError.Value))
            {
                return Result<PaymentCard>.Fail<PaymentCard>(AppError.BadPaymentCardNumberValue);
            }

            if (!_validateCvv(dto.CVV))
            {
                return Result<PaymentCard>.Fail<PaymentCard>(AppError.BadPaymentCardCvvValue);
            }

            var good = new PaymentCard(dto.Number, cardExpiryOrError.Value, dto.CVV, cardTypeOrError.Value);

            return Result<PaymentCard>.Ok(good);
        }

        public PaymentCardDto ToDto()
        {
            return new PaymentCardDto
            {
                CardType = CardType.Label,
                CVV = Cvv,
                Expiry = new PaymentCardExpiryDto
                {
                    Month = Expiry.Month,
                    Year = Expiry.Year
                },
                Number = CardNumber
            };
        }

        public CardInfo ToMasked()
        {
            var numberLength = CardNumber.Length;
            return new CardInfo
            {
                CardType = CardType.Label,
                MaskedCvv = $"**{Cvv.Substring(2)}",
                MaskedExpiry = $"**/{Expiry.Year}",
                MaskedNumber =  $"{ new string('*', numberLength - 4)}{CardNumber.Substring(numberLength - 4)}"
            };
        }

        public bool Equals(PaymentCard other)
        {
            return CardNumber == other.CardNumber
                && Expiry.Equals(other.Expiry)
                && Cvv == other.Cvv
                && CardType == other.CardType;
        }

        public override bool Equals(object ob)
        {
            if (ob is PaymentCard c)
            {
                return this == c;
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return CardNumber.GetHashCode()
                    + 11 * Expiry.Month.GetHashCode()
                    + 17 * Expiry.Year.GetHashCode()
                    + 23 * Cvv.GetHashCode()
                    + 31 * CardType.GetHashCode();
            }
            
        }

        public static bool operator ==(PaymentCard obj1, PaymentCard obj2)
        {
            //Invoke the Equals() version implemented above
            return obj1.Equals(obj2);
        }

        //The == and != should be overloaded in pair
        public static bool operator !=(PaymentCard obj1, PaymentCard obj2)
        {
            return !(obj1 == obj2);
        }

        private PaymentCard(string cardNumber, PaymentCardExpiry expiry, string cvv, PaymentCardType cardType)
        {
            CardNumber = cardNumber;
            Expiry = expiry;
            Cvv = cvv;
            CardType = cardType;
        }

        private static bool _validateCardNumber(string number, PaymentCardType cardType)
        {
            Regex numberRegex = null;

            switch (cardType.InnerEnum)
            {
                case PaymentCardTypeEnum.Amex:
                    numberRegex = new Regex(@"^3[47][0-9]{13}$");
                    break;
                case PaymentCardTypeEnum.MasterCard:
                    numberRegex = new Regex(@"^5[1-5][0-9]{14}$");
                    break;
                case PaymentCardTypeEnum.Visa:
                    numberRegex = new Regex(@"^4[0-9]{12}(?:[0-9]{3})?$");
                    break;
            }

            Match m = numberRegex.Match(number);

            return m.Success;
        }

        private static bool _validateCvv(string cvv)
        {
            Regex cvvRegex = new Regex(@"^[0-9]{3,4}$");

            var cvvMatch = cvvRegex.Match(cvv);

            return cvvMatch.Success;
        }
    }
}
