﻿namespace PaymentGateway.Domain.Handlers;

public interface IRequestHandler<in TRequest, TResult>
{
    Task<TResult> HandleAsync(TRequest request);
}