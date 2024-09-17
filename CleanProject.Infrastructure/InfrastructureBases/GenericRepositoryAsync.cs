using CleanProject.Infrastructure.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace CleanProject.Infrastructure.InfrastructureBases
{
    public class GenericRepositoryAsync<T> : IGenericRepositoryAsync<T> where T : class
    {
        #region Fields
        public ApplicationDBContext _context;
        #endregion

        #region Constructors
        public GenericRepositoryAsync(ApplicationDBContext context)
        {
            _context = context;
        }
        #endregion

        #region Handles Functions
        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }


        public virtual IQueryable<T> Entities => _context.Set<T>().AsNoTracking();
        public IQueryable<T> GetTableNoTracking()
        {
            return _context.Set<T>().AsNoTracking().AsQueryable();
        }
        public virtual async Task<List<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }
        public virtual async Task<List<T>> GetAll(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().Where(expression).ToListAsync();
        }


        public virtual async Task AddRangeAsync(ICollection<T> entities)
        {
            await _context.Set<T>().AddRangeAsync(entities);

        }
        public async Task<T> AddAndReturn(T entity)
        {
            var result = await _context.Set<T>().AddAsync(entity);

            return result.Entity;
        }
        public virtual async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);

            return entity;
        }
        public async Task<string> AddImage(IFormFile formFile, string folderName)
        {
            var fileName = $"{Guid.NewGuid()}.{formFile.ContentType.Split('/')[1]}";
            var filePath = Path.Combine($"wwwroot/Image/{folderName}/", fileName);
            String result = $"Image/{folderName}/" + fileName;

            using (var fileSrteam = new FileStream(filePath, FileMode.Create))
            {
                await formFile.CopyToAsync(fileSrteam);
            }
            return result;
        }

        public virtual void UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public virtual void DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
        public virtual void DeleteRangeAsync(ICollection<T> entities)
        {
            foreach (var entity in entities)
            {
                _context.Entry(entity).State = EntityState.Deleted;
            }
        }

        public IDbContextTransaction BeginTransaction()
        {


            return _context.Database.BeginTransaction();
        }

        public void Commit()
        {
            _context.Database.CommitTransaction();

        }

        public void RollBack()
        {
            _context.Database.RollbackTransaction();

        }

        public IQueryable<T> GetTableAsTracking()
        {
            return _context.Set<T>().AsQueryable();

        }

        public virtual void UpdateRangeAsync(ICollection<T> entities)
        {
            _context.Set<T>().UpdateRange(entities);
        }


        #endregion

    }
}
