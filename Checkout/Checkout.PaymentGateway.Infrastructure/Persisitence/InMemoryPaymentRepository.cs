using Checkout.PaymentGateway.Common.ErrorHandling;
using Checkout.PaymentGateway.Core.Abstract.Repositories;
using Checkout.PaymentGateway.Core.Entities;
using Checkout.PaymentGateway.Core.Values;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Checkout.PaymentGateway.Infrastructure.Persistence
{
    class InMemoryPaymentRepository : IPaymentRepository
    {
        private static readonly Dictionary<Guid, Payment> _memory = new Dictionary<Guid, Payment>();
        public InMemoryPaymentRepository()
        {

        }
        public async Task<Payment> Save(PaymentRequest request, PaymentStatus status, Guid bankOperationId)
        {
            var payment = new Payment(Guid.NewGuid(), bankOperationId, request.Card, request.Amount, request.Currency, status);
            _memory.Add(payment.Id, payment);
            return await Task.FromResult(payment);
        }

        public async Task<Payment> Get(Guid id)
        {
            if (!_memory.ContainsKey(id))
            {
                throw new PaymentNotFoundException(id);
            }
            return await Task.FromResult(_memory[id]);
        }
    }
}
