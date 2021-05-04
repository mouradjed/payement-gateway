using Checkout.PaymentGateway.Core.Values;
using Xunit;

namespace Checkout.PaymentGateway.UnitTests.Core.Values
{
    public class PaymentAmountTests
    {
        [Fact]
        public void Parse_From_Valid_Decimal_Value_Should_Succeed()
        {
            var amtOrError = PaymentAmount.Create(125.5M);
            Assert.True(amtOrError.IsSuccess);
        }

        [Fact]
        public void Parse_From_Valid_String_Value_Should_Succeed()
        {
            var amtOrError = PaymentAmount.Create("125.5");
            Assert.True(amtOrError.IsSuccess);
        }

        [Fact]
        public void Parse_From_Valid_Int_Value_Should_Succeed()
        {
            var amtOrError = PaymentAmount.Create(125);
            Assert.True(amtOrError.IsSuccess);
        }

        [Fact]
        public void Parse_From_Valid_String_Int_Value_Should_Succeed()
        {
            var amtOrError = PaymentAmount.Create("125");
            Assert.True(amtOrError.IsSuccess);
        }

        [Fact]
        public void Parse_From_Non_Valid_String_Value_Should_Fail()
        {
            var amtOrError = PaymentAmount.Create("-125.5");
            Assert.False(amtOrError.IsSuccess);
        }

        [Fact]
        public void Parse_From_Non_Valid_String_Value2_Should_Fail()
        {
            var amtOrError = PaymentAmount.Create("FAIL");
            Assert.False(amtOrError.IsSuccess);
        }

        [Fact]
        public void Parse_From_Non_Valid_String_With_Comma__Should_Fail()
        {
            var amtOrError = PaymentAmount.Create("125,25");
            Assert.False(amtOrError.IsSuccess);
        }

        [Fact]
        public void Parse_From_Non_Valid_Decimal_Value_Should_Fail()
        {
            var amtOrError = PaymentAmount.Create(-125.5M);
            Assert.False(amtOrError.IsSuccess);
        }

        [Fact]
        public void Parse_From_Zero_Decimal_Value_Should_Fail()
        {
            var amtOrError = PaymentAmount.Create(0);
            Assert.False(amtOrError.IsSuccess);
        }

        [Fact]
        public void Equals_Should_Succeed()
        {
            var amt1 = PaymentAmount.Create(120).Value;
            var amt2 = PaymentAmount.Create(120).Value;
            Assert.Equal(amt1, amt2);
        }

        [Fact]
        public void Not_Equals_Should_Succeed()
        {
            var amt1 = PaymentAmount.Create(120).Value;
            var amt2 = PaymentAmount.Create(120.00000012M).Value;
            Assert.NotEqual(amt1, amt2);
        }
    }
}
