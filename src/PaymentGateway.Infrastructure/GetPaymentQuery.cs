using Microsoft.Extensions.Logging;
using PaymentGateway.Domain.Models;
using PaymentGateway.Domain.Ports;
using PaymentGateway.Infrastructure.Extensions;
using PaymentGateway.Infrastructure.Repositories;

namespace PaymentGateway.Infrastructure;

public class GetPaymentQuery : IGetPaymentQuery
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly ILogger<GetPaymentQuery> _logger;

    public GetPaymentQuery(IPaymentRepository paymentRepository,
        ILogger<GetPaymentQuery> logger)
    {
        _paymentRepository = paymentRepository;
        _logger = logger;
    }

    public async Task<GetPaymentResponse> GetPayment(GetPaymentRequest request)
    {
        try
        {
            var result = await _paymentRepository.GetAsync(request.Id);

            if (result != null)
            {
                return new GetPaymentResponse.Success(result.ToDomainPaymentDetails());
            }

            return new GetPaymentResponse.NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "There is an error while fetching payment details");
            return new GetPaymentResponse.InternalError();
        }
    }
}