using CleanProject.Data.Entities;
using CleanProject.Data.Enum;
using CleanProject.Infrastructure.InfrastructureBases;
using CleanProject.Infrastructure.Repositories.IRepository;
using CleanProject.Srevice.IServices;
using Microsoft.EntityFrameworkCore;

namespace CleanProject.Srevice.Services
{
    public class DepartmentService : IDepartmentService
    {

        #region Fields
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IUnitOfWork _unitOfWork;

        #endregion

        #region Constructors
        public DepartmentService(IDepartmentRepository departmentRepository, IUnitOfWork unitOfWork)
        {
            _departmentRepository = departmentRepository;
            _unitOfWork = unitOfWork;
        }

        #endregion

        #region Handle Functions

        public async Task<string> AddAsync(Department department, CancellationToken cancellationToken = default)
        {
            await _departmentRepository.AddAsync(department);
            await _unitOfWork.CompleteAsync(cancellationToken);
            return "Success";
        }

        public async Task<string> EditAsync(Department department, CancellationToken cancellationToken = default)
        {
            _departmentRepository.UpdateAsync(department);
            await _unitOfWork.CompleteAsync(cancellationToken);
            return "Success";
        }

        public async Task<bool> DeletedAsync(int id, CancellationToken cancellationToken = default)
        {
            var department = await _departmentRepository.GetByIdAsync(id);
            if (department == null) return false;

            _departmentRepository.DeleteAsync(department);
            await _unitOfWork.CompleteAsync(cancellationToken);
            return true;
        }

        public async Task<bool> IsDepartmentArExist(string name)
        {
            var department = _departmentRepository.GetTableNoTracking()
                .Where(x => x.DNameAr.Equals(name)).FirstOrDefault();
            if (department == null) return false;
            return true;
        }
        public async Task<bool> IsDepartmentEnExist(string name)
        {
            var department = _departmentRepository.GetTableNoTracking()
                .Where(x => x.DNameEn.Equals(name)).FirstOrDefault();
            if (department == null) return false;
            return true;
        }
        public async Task<Department> GetDepartmentByIdAsync(int id)
        {
            var department = _departmentRepository.GetTableNoTracking().Where(x => x.DID.Equals(id))
                .Include(x => x.DepartmentSubjects).ThenInclude(x => x.Subject)
                .Include(x => x.Students)
                .Include(x => x.Instructors)
                .Include(x => x.Instructor)
                .FirstOrDefault();
            return department;
        }

        public async Task<List<Department>> GetDepartmentListAsync()
        {
            return await _departmentRepository.GetDepartmentListAsync();
        }

        public IQueryable<Department> FilterDepartmentPaginatedQuerable(DepartmentOrderingEnum orderingEnum, string search)
        {
            var queryable = _departmentRepository.GetTableNoTracking().AsQueryable();
            if (search != null)
            {
                queryable = queryable.Where(x => x.DID.Equals(int.Parse(search)) || x.DNameEn.Contains(search) || x.DNameAr.Contains(search));
            }
            switch (orderingEnum)
            {
                case DepartmentOrderingEnum.DID:
                    queryable = queryable.OrderBy(x => x.DID);
                    break;
                case DepartmentOrderingEnum.DNameEn:
                    queryable = queryable.OrderBy(x => x.DNameEn);
                    break;
                case DepartmentOrderingEnum.DNameAr:
                    queryable = queryable.OrderBy(x => x.DNameAr);
                    break;
            }
            return queryable;
        }

        #endregion
    }
}
