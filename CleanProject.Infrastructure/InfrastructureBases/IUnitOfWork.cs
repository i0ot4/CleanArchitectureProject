using Microsoft.EntityFrameworkCore;

namespace CleanProject.Infrastructure.InfrastructureBases
{
    public interface IUnitOfWork
    {
        DbContext GetContext();
        Task BeginTransactionAsync(CancellationToken cancellationToken = default);
        Task CommitTransactionAsync(CancellationToken cancellationToken = default);
        Task<int> CompleteAsync(CancellationToken cancellationToken = default);
        Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
    }
}
