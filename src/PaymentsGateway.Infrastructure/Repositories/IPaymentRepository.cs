using PaymentsGateway.Domain.Models;
using PaymentsGateway.Infrastructure.Database.Entities;

namespace PaymentsGateway.Infrastructure.Repositories
{
    public interface IPaymentRepository
    {
        Task<Payment> InsertAsync(CreatePaymentRequest payment);
        Task<Payment?> GetAsync(Guid id);
        Task<Payment> UpdateAsync(Payment payment, string authorizationCode);
    }
}
