using PaymentGateway.Domain.Models;
using PaymentGateway.Infrastructure.Database.Entities;

namespace PaymentGateway.Infrastructure.Repositories;

public interface IPaymentRepository
{
    Task<Payment> InsertAsync(CreatePaymentRequest payment);
    Task<Payment?> GetAsync(Guid id);
    Task<Payment> UpdateAsync(Payment payment, string authorizationCode);
}