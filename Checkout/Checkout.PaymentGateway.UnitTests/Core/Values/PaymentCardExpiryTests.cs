using Checkout.PaymentGateway.Core.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.PaymentGateway.UnitTests.Core.Values
{
    public class PaymentCardExpiryTests
    {
        [Fact]
        public void Parse_From_Valid_Int_Values_Should_Succeed()
        {
            var expOrError = PaymentCardExpiry.Create(8, 2023);
            Assert.True(expOrError.IsSuccess);
        }

        [Fact]
        public void Parse_From_Valid_String_Values_Should_Succeed()
        {
            var expOrError = PaymentCardExpiry.Create("08", "2023");
            Assert.True(expOrError.IsSuccess);
        }

        [Fact]
        public void Parse_From_Non_Valid_Int_Month_Should_Fail()
        {
            var expOrError = PaymentCardExpiry.Create(14, 2023);
            Assert.False(expOrError.IsSuccess);
        }

        [Fact]
        public void Parse_From_Non_Valid_String_Month_Should_Fail()
        {
            var expOrError = PaymentCardExpiry.Create("14", "2023");
            Assert.False(expOrError.IsSuccess);
        }

        [Fact]
        public void Parse_From_Non_Valid_Int_Year_Should_Fail()
        {
            var expOrError = PaymentCardExpiry.Create(10, 2020);
            Assert.False(expOrError.IsSuccess);
        }

        [Fact]
        public void Parse_From_Non_Valid_String_Year_Should_Fail()
        {
            var expOrError = PaymentCardExpiry.Create("10", "2020");
            Assert.False(expOrError.IsSuccess);
        }

        [Fact]
        public void Parse_From_Int_Passed_Date_Should_Fail()
        {
            var expOrError = PaymentCardExpiry.Create(01, 2021);
            Assert.False(expOrError.IsSuccess);
        }

        [Fact]
        public void Parse_From_String_Passed_Date_Should_Fail()
        {
            var expOrError = PaymentCardExpiry.Create("01", "2021");
            Assert.False(expOrError.IsSuccess);
        }
    }
}
