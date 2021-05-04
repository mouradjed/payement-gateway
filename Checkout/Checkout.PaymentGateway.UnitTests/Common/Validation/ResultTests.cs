
using Checkout.PaymentGateway.Common.Validation;
using Xunit;

namespace Checkout.PaymentGateway.UnitTests.Common.Validation
{
    public class ResultTests
    {
        [Fact]
        public void FailedResult_Should_Throw_Exception()
        {
            // Arrange
            var result = Result<int>.Fail<int>(AppError.BadDataAccess);

            // Act and assert
            var exception = Assert.Throws<BusinessException>(() => result.Value);
            Assert.Equal(exception.Message, AppError.BadDataAccess.Message);
        }

        [Fact]
        public void Combine_With_Failed_Results_Should_Return_Combined_Failed_Result()
        {
            // Arrange
            var result1 = Result.Fail(AppError.BadDataAccess);
            var result2 = Result<int>.Ok(10);
            var result3 = Result.Fail(AppError.BadDataAccess);

            // Act 
            var combined = Result.Combine(new Result[] { result1, result2, result3 });
            Assert.False(combined.IsSuccess);
            Assert.Equal(combined.Error.Message.Split('|'), new string[] { result1.Error.Message, result3.Error.Message });
        }
    }
}
