using Checkout.PaymentGateway.Common.Dtos;
using Checkout.PaymentGateway.Core.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.PaymentGateway.UnitTests.Core.Values
{
    public class PaymentCardTests
    {
        [Fact]
        public void Parse_From_Valid_Dto_Should_Succeed()
        {
            var dto = new PaymentCardDto
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
            var pcOrError = PaymentCard.Create(dto);
            Assert.True(pcOrError.IsSuccess);
        }

        [Fact]
        public void Parse_From_Bad_Number_Should_Fail()
        {
            var dto = new PaymentCardDto
            {
                CardType = "Visa",
                CVV = "518",
                Expiry = new PaymentCardExpiryDto
                {
                    Month = 12,
                    Year = 2022
                },
                Number = "415527986045758"
            };
            var pcOrError = PaymentCard.Create(dto);
            Assert.False(pcOrError.IsSuccess);
        }

        [Fact]
        public void Parse_From_Bad_Cvv_Should_Fail()
        {
            var dto = new PaymentCardDto
            {
                CardType = "Visa",
                CVV = "5185287",
                Expiry = new PaymentCardExpiryDto
                {
                    Month = 12,
                    Year = 2022
                },
                Number = "4155279860457"
            };
            var pcOrError = PaymentCard.Create(dto);
            Assert.False(pcOrError.IsSuccess);
        }

        [Fact]
        public void Parse_From_Bad_Expiry_Should_Fail()
        {
            var dto = new PaymentCardDto
            {
                CardType = "Visa",
                CVV = "518",
                Expiry = new PaymentCardExpiryDto
                {
                    Month = 13,
                    Year = 2022
                },
                Number = "4155279860457"
            };
            var pcOrError = PaymentCard.Create(dto);
            Assert.False(pcOrError.IsSuccess);
        }

        [Fact]
        public void Equals_Should_Succeed()
        {
            var dto1 = new PaymentCardDto
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

            var dto2 = new PaymentCardDto
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
            var pc1 = PaymentCard.Create(dto1).Value;
            var pc2 = PaymentCard.Create(dto2).Value;
            Assert.Equal(pc1, pc2);
        }

        [Fact]
        public void Not_Equals_Should_Succeed()
        {
            var dto1 = new PaymentCardDto
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

            var dto2 = new PaymentCardDto
            {
                CardType = "Visa",
                CVV = "515",
                Expiry = new PaymentCardExpiryDto
                {
                    Month = 12,
                    Year = 2022
                },
                Number = "4155279860457"
            };
            var pc1 = PaymentCard.Create(dto1).Value;
            var pc2 = PaymentCard.Create(dto2).Value;
            Assert.NotEqual(pc1, pc2);
        }
    }
}
