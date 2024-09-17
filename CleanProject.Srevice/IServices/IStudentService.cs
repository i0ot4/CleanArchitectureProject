using CleanProject.Data.Entities;
using CleanProject.Data.Enum;

namespace CleanProject.Srevice.IServices
{
    public interface IStudentService
    {
        Task<Student> AddStudentAsync(Student student, CancellationToken cancellationToken = default);
        Task<bool> DeleteStudentAsync(Student student, CancellationToken cancellationToken = default);
        Task<bool> EditStudentAsync(Student student, CancellationToken cancellationToken = default);
        IQueryable<Student> FilterStudentPaginatedQuerable(StudentOrderingEnum orderingEnum, string Search);
        Task<Student> GetStudentById(int id);
        Task<List<Student>> GetStudentListAsync();
        IQueryable<Student> GetStudentsByDepartmentIDQuerable(int DID);
    }
}
