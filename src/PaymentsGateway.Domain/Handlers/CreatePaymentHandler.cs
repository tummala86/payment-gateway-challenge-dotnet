using PaymentsGateway.Domain.Models;
using PaymentsGateway.Domain.Ports;

namespace PaymentsGateway.Domain.Handlers
{
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
}
