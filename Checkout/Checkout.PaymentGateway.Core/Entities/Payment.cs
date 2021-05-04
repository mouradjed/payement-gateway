using Checkout.PaymentGateway.Core.Abstract;
using Checkout.PaymentGateway.Core.Values;
using System;

namespace Checkout.PaymentGateway.Core.Entities
{
    public class Payment : EntityBase
    {
        
        public readonly PaymentCard Card;
        public readonly PaymentAmount Amount;
        public readonly Currency Currency;
        public readonly PaymentStatus Status;
        public readonly Guid BankOperationId;
        public readonly DateTime PaymentDate;

        public Payment(Guid id, Guid bankOperationId, PaymentCard card, PaymentAmount amount, Currency currency, PaymentStatus status): base(id)
        {
            Card = card;
            Amount = amount;
            Currency = currency;
            Status = status;
            BankOperationId = bankOperationId;
            PaymentDate = DateTime.Now;
        }
    }
}
