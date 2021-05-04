using Checkout.PaymentGateway.Common.Dtos;
using Checkout.PaymentGateway.Common.Validation;
using Checkout.PaymentGateway.Core.Abstract.Repositories;
using Checkout.PaymentGateway.Core.Abstract.Services;
using Checkout.PaymentGateway.Core.Entities;
using Checkout.PaymentGateway.Core.Requests;
using Checkout.PaymentGateway.Core.Responses;
using Checkout.PaymentGateway.Core.Values;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using System.Transactions;

namespace Checkout.PaymentGateway.Core.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IBankService _bankService;
        private readonly IPaymentRepository _paymentRepository;
        private readonly ILogger<PaymentService> _logger;
        public PaymentService(IPaymentRepository paymentRepository, IBankService bankService, ILogger<PaymentService>  logger)
        {
            _bankService = bankService;
            _logger = logger;
            _paymentRepository = paymentRepository;
        }

        public async Task<PaymentInfosResponse> GetPaymentInfo(PaymentRequestInfoDto request)
        {
            //exception will be handled upper in the controller
            var payment = await _paymentRepository.Get(request.PaymentId);
            return new PaymentInfosResponse(payment);
        }

        public async Task<PaymentResponse> ProcessPayment(PaymentRequestDto request)
        {
            _logger.LogInformation("Start processing payment request....");
            PaymentResponse response;
            try
            {
                using (TransactionScope transactionScope = new TransactionScope())
                {
                    var paymentRequest = PaymentRequest.Create(request);
                    
                    if (!paymentRequest.IsSuccess)
                    {
                        response = new RejecetdPaymentResponse(paymentRequest.Error.Message);
                    }
                    else
                    {
                        var bankRequest = new ProcessWithdrawRequest
                        {
                            Amount = paymentRequest.Value.Amount,
                            Card = paymentRequest.Value.Card
                        };

                        var bankReponse = await _bankService.ProcessWithdraw(bankRequest);

                        var status = PaymentStatus.CreateFromBankReponse(bankReponse.Status);

                        var paiment = await _paymentRepository.Save(paymentRequest.Value, status, bankReponse.OperationId);

                        response = new FulfilledPaymentResponse(bankReponse.OperationId, paiment.Id, status, bankReponse.Message);
                    }

                    transactionScope.Complete();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, null);
                response = new AbortedPaymentResponse(ex.Message);
            }
            return response;
        }
    }
}
