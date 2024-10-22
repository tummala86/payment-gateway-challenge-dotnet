using PaymentGateway.Api.Models.Requests;
using PaymentsGateway.Api.Validation;
using PaymentsGateway.Domain.Handlers;
using PaymentsGateway.Domain.Models;
using PaymentsGateway.Domain.Ports;
using PaymentsGateway.Infrastructure;
using PaymentsGateway.Infrastructure.ExternalServices;
using PaymentsGateway.Infrastructure.Repositories;
using GetPaymentResponse = PaymentsGateway.Domain.Models.GetPaymentResponse;

namespace PaymentsGateway.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void SetupServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IRequestValidator<PostPaymentRequest>, PostPaymentRequestValidator>();
            services.AddTransient<IRequestHandler<CreatePaymentRequest, CreatePaymentResponse>, CreatePaymentHandler>();
            services.AddTransient<IRequestHandler<GetPaymentRequest, GetPaymentResponse>, GetPaymentHandler>();
            services.AddTransient<ICreatePaymentCommand, CreatePaymentCommand>();
            services.AddScoped<IGetPaymentQuery, GetPaymentQuery>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();

            services.AddHttpClient<IAcquiringBankClient, AcquiringBankClient>()
                .ConfigureHttpClient(x => x.BaseAddress = new Uri(configuration["AcquiringBank:Uri"]!));
        }
    }
}
