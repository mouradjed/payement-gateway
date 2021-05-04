using Checkout.PaymentGateway.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Checkout.PaymentGateway.Core.Entities;

namespace Checkout.PaymentGateway.UnitTests.Core.Entities
{
    public class PaymentRequestTests
    {
        [Fact]
        public void Parse_From_Valid_Dto_Should_Succeed()
        {
            var cardDto = new PaymentCardDto
            {
                CardType = "Visa",
                CVV = "518",
                Expiry = new PaymentCardExpiryDto
                {
                    Month = 12,
                    Year = 2022
                },
                Number = "4155279860457"
            };

            var dto = new PaymentRequestDto
            {
                Amount = "172.5",
                Card = cardDto,
                Currency = "EUR",
            };
            var pmtOrError = PaymentRequest.Create(dto);
            Assert.True(pmtOrError.IsSuccess);
        }

        [Fact]
        public void Parse_From_Bad_Card_Should_Fail()
        {
            var cardDto = new PaymentCardDto
            {
                CardType = "Visa",
                CVV = "518",
                Expiry = new PaymentCardExpiryDto
                {
                    Month = 128,
                    Year = 2022
                },
                Number = "4155279860457"
            };

            var dto = new PaymentRequestDto
            {
                Amount = "172.5M",
                Card = cardDto,
                Currency = "EUR",
            };
            var pmtOrError = PaymentRequest.Create(dto);
            Assert.False(pmtOrError.IsSuccess);
        }

        [Fact]
        public void Parse_With_Bad_Currency_Should_Fail()
        {
            var cardDto = new PaymentCardDto
            {
                CardType = "Visa",
                CVV = "518",
                Expiry = new PaymentCardExpiryDto
                {
                    Month = 12,
                    Year = 2022
                },
                Number = "4155279860457"
            };

            var dto = new PaymentRequestDto
            {
                Amount = "172.5",
                Card = cardDto,
                Currency = "EURO",
            };
            var pmtOrError = PaymentRequest.Create(dto);
            Assert.False(pmtOrError.IsSuccess);
        }
    }
}
