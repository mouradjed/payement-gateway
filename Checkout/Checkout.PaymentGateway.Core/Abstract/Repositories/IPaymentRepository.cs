using Checkout.PaymentGateway.Common.Dtos;
using Checkout.PaymentGateway.Core.Entities;
using Checkout.PaymentGateway.Core.Values;
using System;
using System.Threading.Tasks;

namespace Checkout.PaymentGateway.Core.Abstract.Repositories
{
    public interface IPaymentRepository
    {
        Task<Payment> Save(PaymentRequest  request, PaymentStatus status, Guid bankOperationId);
        Task<Payment> Get(Guid id);
    }
}
