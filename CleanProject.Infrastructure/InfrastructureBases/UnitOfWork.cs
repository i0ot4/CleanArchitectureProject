using CleanProject.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace CleanProject.Infrastructure.InfrastructureBases
{
    public class UnitOfWork : IUnitOfWork
    {
        public readonly ApplicationDBContext _context;
        private IDbContextTransaction _transaction;
        public UnitOfWork(ApplicationDBContext context)
        {
            _context = context;
        }
        public DbContext GetContext()
        {
            return _context;
        }

        public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            _transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
        }
        public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
        {
            await _transaction.RollbackAsync(cancellationToken);
        }

        public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
        {
            await _transaction.CommitAsync(cancellationToken);
        }

        public async Task<int> CompleteAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            _transaction?.Dispose();

            _context.Dispose();
        }
    }
}
