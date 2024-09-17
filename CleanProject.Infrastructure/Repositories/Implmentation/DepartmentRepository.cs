using CleanProject.Data.Entities;
using CleanProject.Infrastructure.Context;
using CleanProject.Infrastructure.InfrastructureBases;
using CleanProject.Infrastructure.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;

namespace CleanProject.Infrastructure.Repositories.Implmentation
{
    public class DepartmentRepository : GenericRepositoryAsync<Department>, IDepartmentRepository
    {

        #region Fields
        private DbSet<Department> _departments;
        #endregion

        #region Constructors
        public DepartmentRepository(ApplicationDBContext dbContext) : base(dbContext)
        {
            _departments = dbContext.Set<Department>();
        }
        #endregion

        #region Handle Functions
        public async Task<List<Department>> GetDepartmentListAsync()
        {
            return await _departments.ToListAsync();
        }

        #endregion
    }
}
