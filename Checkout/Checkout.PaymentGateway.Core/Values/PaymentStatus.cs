
using Checkout.PaymentGateway.Common.Validation;
using System;

namespace Checkout.PaymentGateway.Core.Values
{
    public class PaymentStatus : Enumeration<PaymentStatusEnum>
    {
        private PaymentStatus(PaymentStatusEnum status) : base(status)
        {
        }

        public static Result<PaymentStatus> Create(string raw)
        {
            if (!Enum.TryParse(raw, out PaymentStatusEnum value))
            {
                return Result<PaymentStatus>.Fail<PaymentStatus>(AppError.BadPaymentStatusValue);
            }
            return Result<PaymentStatus>.Ok(new PaymentStatus(value));
        }

        public static PaymentStatus CreateFromBankReponse(bool flag)
        {
            return flag ? BankAccepted : BankRejected;
        }

        public static PaymentStatus BankAccepted = new PaymentStatus(PaymentStatusEnum.BankAccepted);
        public static PaymentStatus BankRejected = new PaymentStatus(PaymentStatusEnum.BankRejected);
        public static PaymentStatus Invalid = new PaymentStatus(PaymentStatusEnum.Invalid);
        public static PaymentStatus Aborted = new PaymentStatus(PaymentStatusEnum.Aborted);
    }

    public enum PaymentStatusEnum
    {
        BankAccepted,
        BankRejected,
        Invalid,
        Aborted
    }
}
