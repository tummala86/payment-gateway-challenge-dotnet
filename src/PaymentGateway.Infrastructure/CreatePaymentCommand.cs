﻿using Microsoft.Extensions.Logging;
using PaymentGateway.Domain.Models;
using PaymentGateway.Domain.Models.Enum;
using PaymentGateway.Domain.Ports;
using PaymentGateway.Infrastructure.Database.Extensions;
using PaymentGateway.Infrastructure.Extensions;
using PaymentGateway.Infrastructure.ExternalServices;
using PaymentGateway.Infrastructure.Repositories;

namespace PaymentGateway.Infrastructure;

public class CreatePaymentCommand : ICreatePaymentCommand
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IAcquiringBankClient _acquiringBankClient;
    private readonly ILogger<CreatePaymentCommand> _logger;

    public CreatePaymentCommand(IPaymentRepository paymentRepository,
        IAcquiringBankClient acquiringBankClient,
        ILogger<CreatePaymentCommand> logger)
    {
        _paymentRepository = paymentRepository;
        _acquiringBankClient = acquiringBankClient;
        _logger = logger;
    }

    public async Task<CreatePaymentResponse> CreatePayment(CreatePaymentRequest request)
    {
        try
        {
            var paymentRequest = request.ToPaymentEntity(Guid.NewGuid());
            var result = await _paymentRepository.InsertAsync(paymentRequest);

            if (result != null)
            {
                var paymentResult = await _acquiringBankClient.ProcessPayment(request.ToPaymentRequest());

                return await paymentResult.Match<Task<CreatePaymentResponse>>(
                   async success =>
                    {
                        result.Status = success.Authorized ? PaymentStatus.Authorized : PaymentStatus.Declined;
                        result.BankAuthorizationCode = success.AuthorizationCode;
                        var updateResult = await _paymentRepository.UpdateAsync(result, success.AuthorizationCode);
                        return new CreatePaymentResponse.Success(result.ToDomainPaymentDetails());
                    },
                    error => Task.FromResult<CreatePaymentResponse>(new CreatePaymentResponse.InternalError())
                );
            }

            return new CreatePaymentResponse.InternalError();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "There is an error while processing payment");
            return new CreatePaymentResponse.InternalError();
        }
    }
}