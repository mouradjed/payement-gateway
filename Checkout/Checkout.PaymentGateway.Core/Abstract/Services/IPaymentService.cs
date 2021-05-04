using Checkout.PaymentGateway.Common.Dtos;
using Checkout.PaymentGateway.Core.Requests;
using Checkout.PaymentGateway.Core.Responses;
using System.Threading.Tasks;

namespace Checkout.PaymentGateway.Core.Abstract.Services
{
    public interface IPaymentService
    {
        Task<PaymentResponse> ProcessPayment(PaymentRequestDto request);
        Task<PaymentInfosResponse> GetPaymentInfo(PaymentRequestInfoDto request);
    }
}
