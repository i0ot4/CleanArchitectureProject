using CleanProject.Data.Entities;
using CleanProject.Infrastructure.InfrastructureBases;

namespace CleanProject.Infrastructure.Repositories.IRepository
{
    public interface IDepartmentRepository : IGenericRepositoryAsync<Department>
    {
        Task<List<Department>> GetDepartmentListAsync();
    }
}
