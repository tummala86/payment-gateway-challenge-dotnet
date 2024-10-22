using PaymentGateway.Api.Models.Requests;
using PaymentGateway.Api.Validation;
using PaymentGateway.Domain.Handlers;
using PaymentGateway.Domain.Models;
using PaymentGateway.Domain.Ports;
using PaymentGateway.Infrastructure;
using PaymentGateway.Infrastructure.ExternalServices;
using PaymentGateway.Infrastructure.Repositories;
using GetPaymentResponse = PaymentGateway.Domain.Models.GetPaymentResponse;

namespace PaymentGateway.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static void SetupServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IRequestValidator<PostPaymentRequest>, PostPaymentRequestValidator>();
        services.AddScoped<IRequestHandler<CreatePaymentRequest, CreatePaymentResponse>, CreatePaymentHandler>();
        services.AddScoped<IRequestHandler<GetPaymentRequest, GetPaymentResponse>, GetPaymentHandler>();
        services.AddScoped<ICreatePaymentCommand, CreatePaymentCommand>();
        services.AddScoped<IGetPaymentQuery, GetPaymentQuery>();
        services.AddScoped<IPaymentRepository, PaymentRepository>();

        services.AddHttpClient<IAcquiringBankClient, AcquiringBankClient>()
            .ConfigureHttpClient(x => x.BaseAddress = new Uri(configuration["AcquiringBankSettings:Uri"]!));
    }
}