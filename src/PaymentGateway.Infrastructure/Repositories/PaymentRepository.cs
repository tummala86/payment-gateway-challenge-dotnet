using Microsoft.EntityFrameworkCore;
using PaymentGateway.Domain.Models;
using PaymentGateway.Infrastructure.Database;
using PaymentGateway.Infrastructure.Database.Entities;
using PaymentGateway.Infrastructure.Database.Extensions;

namespace PaymentGateway.Infrastructure.Repositories;

public class PaymentRepository : IPaymentRepository
{
    public Task<Payment?> GetAsync(Guid id)
    {
        using var context = new PaymentGatewayDbContext();
        return context.Payments.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Payment> InsertAsync(CreatePaymentRequest createPaymentRequest)
    {
        using var context = new PaymentGatewayDbContext();
        var paymentRequest = createPaymentRequest.ToPaymentEntity(Guid.NewGuid());
        await context.Payments.AddAsync(paymentRequest);
        await context.SaveChangesAsync();
        return paymentRequest;
    }

    public async Task<Payment> UpdateAsync(Payment payment, string authorizationCode)
    {
        var paymentDetails = await GetAsync(payment.Id);
        paymentDetails!.Status = payment.Status;
        paymentDetails.BankAuthorizationCode = authorizationCode;
        paymentDetails.UpdatedAt = DateTime.UtcNow;

        using var context = new PaymentGatewayDbContext();
        context.Payments.Update(paymentDetails);
        await context.SaveChangesAsync();
        return payment;
    }
}