using Checkout.PaymentGateway.Core.Abstract.Repositories;
using Checkout.PaymentGateway.Core.Abstract.Services;
using Checkout.PaymentGateway.Core.Services;
using Checkout.PaymentGateway.Infrastructure.Persistence;
using Checkout.PaymentGateway.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Checkout.PaymentGateway.Infrastructure.Di
{
    public static class Installer
    {
        public static IServiceCollection AddDiConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IBankService, InMemoryBankService>();
            services.AddScoped<IPaymentRepository, InMemoryPaymentRepository>();
            services.AddScoped<IPaymentService, PaymentService>();

            return services;
        }
    }
}
