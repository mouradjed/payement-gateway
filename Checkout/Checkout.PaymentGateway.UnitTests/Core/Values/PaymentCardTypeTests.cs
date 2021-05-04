using Checkout.PaymentGateway.Common.Validation;
using Checkout.PaymentGateway.Core.Values;
using Xunit;

namespace Checkout.PaymentGateway.UnitTests.Core.Values
{
    public class PaymentCardTypeTests
    {
        [Fact]
        public void Parse_From_Valid_Value_Should_Succeed()
        {
            var pctOrError = PaymentCardType.Create("Visa");
            Assert.True(pctOrError.IsSuccess);
            Assert.Equal(pctOrError.Value, PaymentCardType.Visa);
        }

        [Fact]
        public void Parse_From_Non_Valid_Value_Should_Fail()
        {
            var pctOrError = PaymentCardType.Create("FAIL");
            Assert.False(pctOrError.IsSuccess);
            Assert.Equal(pctOrError.Error.Message, AppError.BadPaymentCardTypeValue.Message);
        }

        [Fact]
        public void Equals_Should_Succeed()
        {
            var pct1 = PaymentCardType.Create("Visa").Value;
            var pct2 = PaymentCardType.Visa;
            Assert.Equal(pct1, pct2);
        }

        [Fact]
        public void Not_Equals_Should_Succeed()
        {
            var pct1 = PaymentCardType.Amex;
            var pct2 = PaymentCardType.Visa;
            Assert.NotEqual(pct1, pct2);
        }
    }
}
