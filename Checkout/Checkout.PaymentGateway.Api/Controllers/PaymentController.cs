using Checkout.PaymentGateway.Common.Dtos;
using Checkout.PaymentGateway.Common.ErrorHandling;
using Checkout.PaymentGateway.Core.Abstract.Services;
using Checkout.PaymentGateway.Core.Requests;
using Checkout.PaymentGateway.Core.Responses;
using Checkout.PaymentGateway.Core.Values;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Checkout.PaymentGateway.Gateway.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentController : ControllerBase
    {
   
        private readonly ILogger<PaymentController> _logger;
        private readonly IPaymentService _paymentService;

        public PaymentController(ILogger<PaymentController> logger, IPaymentService paymentService)
        {
            _logger = logger;
            _paymentService = paymentService;
        }

        [HttpGet("{paymentId}", Name = "GetPaymentInfo")]
        public async Task<IActionResult> GetPaymentInfo(Guid paymentId)
        {
            try
            {
                var request = new PaymentRequestInfoDto
                {
                    PaymentId = paymentId
                };
                var response = await _paymentService.GetPaymentInfo(request);
                return Ok(response.ToDto());
            }
            catch (PaymentNotFoundException bexp)
            {
                return NotFound(bexp.Error.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostPayment(PaymentRequestDto request)
        {
            var response = await _paymentService.ProcessPayment(request);
            var dto = response.ToDto();
            switch (response.Status.InnerEnum)
            {
                case PaymentStatusEnum.Invalid:
                    return BadRequest(dto);
                case PaymentStatusEnum.Aborted:
                    return StatusCode(StatusCodes.Status500InternalServerError, dto);
                default:
                    var fulfilled = response as FulfilledPaymentResponse;
                    return Ok(dto);
            }
        }
    }
}
