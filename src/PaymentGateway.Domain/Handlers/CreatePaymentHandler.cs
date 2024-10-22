using PaymentGateway.Domain.Models;
using PaymentGateway.Domain.Ports;

namespace PaymentGateway.Domain.Handlers;

public class CreatePaymentHandler : IRequestHandler<CreatePaymentRequest, CreatePaymentResponse>
{
    private readonly ICreatePaymentCommand _createPaymentCommand;

    public CreatePaymentHandler(ICreatePaymentCommand createPaymentCommand)
    {
        _createPaymentCommand = createPaymentCommand;
    }

    public async Task<CreatePaymentResponse> HandleAsync(CreatePaymentRequest request)
    {
        return await _createPaymentCommand.CreatePayment(request);
    }
}