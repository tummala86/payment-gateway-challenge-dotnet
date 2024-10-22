using Microsoft.EntityFrameworkCore;
using PaymentsGateway.Domain.Models;
using PaymentsGateway.Infrastructure.Database;
using PaymentsGateway.Infrastructure.Database.Entities;
using PaymentsGateway.Infrastructure.Database.Extensions;

namespace PaymentsGateway.Infrastructure.Repositories;

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
