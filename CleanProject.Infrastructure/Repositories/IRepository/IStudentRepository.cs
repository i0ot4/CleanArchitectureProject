using CleanProject.Data.Entities;
using CleanProject.Infrastructure.InfrastructureBases;

namespace CleanProject.Infrastructure.Repositories.IRepository
{
    public interface IStudentRepository : IGenericRepositoryAsync<Student>
    {
        Task<List<Student>> GetStudentListAsync();
    }
}
