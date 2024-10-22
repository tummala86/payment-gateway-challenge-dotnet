using PaymentGateway.Domain.Models;
using PaymentGateway.Domain.Ports;

namespace PaymentGateway.Domain.Handlers;

public class GetPaymentHandler : IRequestHandler<GetPaymentRequest, GetPaymentResponse>
{
    private readonly IGetPaymentQuery _getPaymentQuery;

    public GetPaymentHandler(IGetPaymentQuery getPaymentQuery)
    {
        _getPaymentQuery = getPaymentQuery;
    }

    public async Task<GetPaymentResponse> HandleAsync(GetPaymentRequest request)
    {
        return await _getPaymentQuery.GetPayment(request);
    }
}