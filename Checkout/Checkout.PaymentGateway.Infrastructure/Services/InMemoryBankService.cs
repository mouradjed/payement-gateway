using Checkout.PaymentGateway.Common.Dtos;
using Checkout.PaymentGateway.Core.Abstract.Services;
using Checkout.PaymentGateway.Core.Requests;
using Checkout.PaymentGateway.Core.Responses;
using Checkout.PaymentGateway.Core.Values;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.PaymentGateway.Infrastructure.Services
{
    public class InMemoryBankService : IBankService
    {
        private readonly Dictionary<int, decimal> _balance = new Dictionary<int, decimal>();
        public InMemoryBankService()
        {
            var card1 = new PaymentCardDto
            {
                CardType = PaymentCardType.Visa.Label,
                CVV = "930",
                Expiry = new PaymentCardExpiryDto
                {
                    Month = 12,
                    Year = 2023
                },
                Number = "4155279860457"
            };
            var card2 = new PaymentCardDto
            {
                CardType = PaymentCardType.Visa.Label,
                CVV = "931",
                Expiry = new PaymentCardExpiryDto
                {
                    Month = 10,
                    Year = 2023
                },
                Number = "4155279860458"
            };

            _balance.Add(PaymentCard.Create(card1).Value.GetHashCode(), 10000m);
            _balance.Add(PaymentCard.Create(card2).Value.GetHashCode(), 5000m);

        }
        public async Task<ProcessWithdrawResponse> ProcessWithdraw(ProcessWithdrawRequest request)
        {
            
            ProcessWithdrawResponse response;
            var key = request.Card.GetHashCode();
            if (!_balance.ContainsKey(key))
            {
                response = new ProcessWithdrawResponse
                {
                    Message = "Unknow Card",
                    OperationId = Guid.NewGuid(),
                    Status = false
                };
            }
            else
            {
                var balance = _balance[key];
                if (request.Amount.Value >= balance )
                {
                    response = new ProcessWithdrawResponse
                    {
                        Message = "Insufficient Provision",
                        OperationId = Guid.NewGuid(),
                        Status = false
                    };
                }
                else
                {
                    _balance[key] = balance - request.Amount.Value;
                    response = new ProcessWithdrawResponse
                    {
                        Message = "Accepted",
                        OperationId = Guid.NewGuid(),
                        Status = true
                    };
                }
            }
            return await Task.FromResult(response);
        }
    }
}
