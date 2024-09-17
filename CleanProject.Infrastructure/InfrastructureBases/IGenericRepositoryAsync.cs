using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace CleanProject.Infrastructure.InfrastructureBases
{
    public interface IGenericRepositoryAsync<T> where T : class
    {
        IQueryable<T> Entities { get; }

        Task<T> GetByIdAsync(int id);
        IDbContextTransaction BeginTransaction();
        void Commit();
        void RollBack();
        IQueryable<T> GetTableNoTracking();
        IQueryable<T> GetTableAsTracking();
        Task<T> AddAsync(T entity);
        Task<T> AddAndReturn(T entity);
        Task AddRangeAsync(ICollection<T> entities);
        Task<string> AddImage(IFormFile formFile, string folderName);
        Task<List<T>> GetAll();
        Task<List<T>> GetAll(Expression<Func<T, bool>> expression);
        void DeleteAsync(T entity);
        void UpdateRangeAsync(ICollection<T> entities);
        void DeleteRangeAsync(ICollection<T> entities);
        void UpdateAsync(T entity);
    }
}
