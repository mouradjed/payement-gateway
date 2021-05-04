using Checkout.PaymentGateway.Common.Validation;
using Checkout.PaymentGateway.Core.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Checkout.PaymentGateway.UnitTests.Core.Values
{
    public class PaymentStatusTests
    {
        [Fact]
        public void Parse_From_Valid_Value_Should_Succeed()
        {
            var psOrError = PaymentStatus.Create("BankAccepted");
            Assert.True(psOrError.IsSuccess);
        }

        [Fact]
        public void Parse_From_Non_Valid_Value_Should_Fail()
        {
            var psOrError = PaymentStatus.Create("FAIL");
            Assert.False(psOrError.IsSuccess);
            Assert.Equal(psOrError.Error.Message, AppError.BadPaymentStatusValue.Message);
        }

        [Fact]
        public void Equals_Should_Succeed()
        {
            var ps1 = PaymentStatus.Create("BankAccepted").Value;
            var ps2 = PaymentStatus.BankAccepted;
            Assert.Equal(ps1, ps2);
        }

        [Fact]
        public void Not_Equals_Should_Succeed()
        {
            var ps1 = PaymentStatus.Aborted;
            var ps2 = PaymentStatus.BankAccepted;
            Assert.NotEqual(ps1, ps2);
        }
    }
}
