using Checkout.PaymentGateway.Core.Requests;
using Checkout.PaymentGateway.Core.Responses;
using System.Threading.Tasks;

namespace Checkout.PaymentGateway.Core.Abstract.Services
{
    public interface IBankService
    {
        Task<ProcessWithdrawResponse> ProcessWithdraw(ProcessWithdrawRequest request);
    }
}
