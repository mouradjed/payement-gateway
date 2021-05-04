using Checkout.PaymentGateway.Common.Validation;
using Checkout.PaymentGateway.Core.Values;
using Xunit;

namespace Checkout.PaymentGateway.UnitTests.Core.Values
{
    public class CurrencyTests
    {
        [Fact]
        public void Parse_From_Valid_Value_Should_Succeed()
        {
            var curOrError = Currency.Create("USD");
            Assert.True(curOrError.IsSuccess);
            Assert.Equal(curOrError.Value, Currency.USD);
        }

        [Fact]
        public void Parse_From_Non_Valid_Value_Should_Fail()
        {
            var curOrError = Currency.Create("FAIL");
            Assert.False(curOrError.IsSuccess);
            Assert.Equal(curOrError.Error.Message, AppError.BadCurrenyValue.Message);
        }

        [Fact]
        public void Equals_Should_Succeed()
        {
            var cur1 = Currency.Create("USD").Value;
            var cur2 = Currency.USD;
            Assert.Equal(cur1, cur2);
        }

        [Fact]
        public void Not_Equals_Should_Succeed()
        {
            var cur1 = Currency.EUR;
            var cur2 = Currency.USD;
            Assert.NotEqual(cur1, cur2);
        }
    }
}
