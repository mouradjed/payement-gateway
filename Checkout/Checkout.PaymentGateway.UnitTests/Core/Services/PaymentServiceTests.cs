using Checkout.PaymentGateway.Common.Dtos;
using Checkout.PaymentGateway.Common.Validation;
using Checkout.PaymentGateway.Core.Abstract.Repositories;
using Checkout.PaymentGateway.Core.Abstract.Services;
using Checkout.PaymentGateway.Core.Entities;
using Checkout.PaymentGateway.Core.Requests;
using Checkout.PaymentGateway.Core.Responses;
using Checkout.PaymentGateway.Core.Services;
using Checkout.PaymentGateway.Core.Values;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.PaymentGateway.UnitTests.Core.Services
{
    public class PaymentServiceTests
    {
        private readonly PaymentService _cut;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IBankService _bankService;
        private readonly ILogger<PaymentService> _logger;
        private static readonly PaymentCard _validCard = PaymentCard.Create(
            new PaymentCardDto
            {
                CardType = PaymentCardType.Visa.Label,
                CVV = "930",
                Expiry = new PaymentCardExpiryDto
                {
                    Month = 12,
                    Year = 2023
                },
                Number = "4155279860457"
            }).Value;

        private static Payment _payment = new Payment(Guid.NewGuid(), Guid.NewGuid(), _validCard, PaymentAmount.Create(1000).Value, Currency.EUR, PaymentStatus.BankAccepted); 
        

        public PaymentServiceTests()
        {
            _paymentRepository = Substitute.For<IPaymentRepository>();
            _logger = Substitute.For<ILogger<PaymentService>>();
            _bankService = Substitute.For<IBankService>();
            _cut = new PaymentService(_paymentRepository, _bankService, _logger);

        }

        [Fact]
        public async Task Process_Invalid_Request_Payment_Should_Be_rejecetd()
        {
            var request = new PaymentRequestDto
            {
                Amount = "-15",
                Card = _validCard.ToDto(),
                Currency = Currency.EUR.Label,
            };
            var response = await _cut.ProcessPayment(request);
            var rejected = response as RejecetdPaymentResponse;
            Assert.NotNull(rejected);
            Assert.Equal(rejected.Message, AppError.BadPaymentAmountValue.Message);
            Assert.Equal(response.Status, PaymentStatus.Invalid);
        }

        [Fact]
        public async Task Process_Valid_Request_Should_Be_Fulfilled()
        {
            var request = new PaymentRequestDto
            {
                Amount = "1000",
                Card = _validCard.ToDto(),
                Currency = Currency.EUR.Label,
            };

            var bankReponse = new ProcessWithdrawResponse
            {
                Message = "OK",
                OperationId = Guid.NewGuid(),
                Status = true
            };

            _bankService.ProcessWithdraw(Arg.Any<ProcessWithdrawRequest>()).Returns(Task.FromResult(bankReponse));

            _paymentRepository.Save(Arg.Any<PaymentRequest>(), Arg.Any<PaymentStatus>(), bankReponse.OperationId).Returns(_payment);
            var response = await _cut.ProcessPayment(request);
            var accepted = response as FulfilledPaymentResponse;
            Assert.NotNull(accepted);
            Assert.Equal(response.Status, PaymentStatus.BankAccepted);
        }

        [Fact]
        public async Task Process_Valid_Request_And_Raised_Exception_Should_Be_Aborted()
        {
            var request = new PaymentRequestDto
            {
                Amount = "1000",
                Card = _validCard.ToDto(),
                Currency = Currency.EUR.Label,
            };

            var bankReponse = new ProcessWithdrawResponse
            {
                Message = "OK",
                OperationId = Guid.NewGuid(),
                Status = true
            };

            var failure = "FATAL EXCEPTION";

            _bankService.When(s => s.ProcessWithdraw(Arg.Any<ProcessWithdrawRequest>()))
                        .Do(s => throw new Exception(failure));

            _paymentRepository.Save(Arg.Any<PaymentRequest>(), Arg.Any<PaymentStatus>(), bankReponse.OperationId).Returns(_payment);
            var response = await _cut.ProcessPayment(request);
            var aborted = response as AbortedPaymentResponse;
            Assert.NotNull(aborted);
            Assert.Equal(aborted.Status, PaymentStatus.Aborted);
            Assert.Equal(failure, aborted.Message);
        }
    }
}
