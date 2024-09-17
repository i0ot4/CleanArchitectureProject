using CleanProject.Data.Entities;
using CleanProject.Data.Enum;

namespace CleanProject.Srevice.IServices
{
    public interface IDepartmentService
    {
        Task<string> AddAsync(Department department, CancellationToken cancellationToken = default);
        Task<bool> DeletedAsync(int id, CancellationToken cancellationToken = default);
        Task<string> EditAsync(Department department, CancellationToken cancellationToken = default);
        IQueryable<Department> FilterDepartmentPaginatedQuerable(DepartmentOrderingEnum orderingEnum, string search);
        Task<Department> GetDepartmentByIdAsync(int id);
        Task<List<Department>> GetDepartmentListAsync();

        public Task<bool> IsDepartmentArExist(string name);
        public Task<bool> IsDepartmentEnExist(string name);
    }
}
