using CleanProject.Data.Entities;
using CleanProject.Data.Enum;
using CleanProject.Infrastructure.InfrastructureBases;
using CleanProject.Infrastructure.Repositories.IRepository;
using CleanProject.Srevice.IServices;
using Microsoft.EntityFrameworkCore;

namespace CleanProject.Srevice.Services
{
    public class StudentService : IStudentService
    {

        #region Feilds
        private readonly IStudentRepository _studentRepository;
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Constructor
        public StudentService(IStudentRepository studentRepository, IUnitOfWork unitOfWork)
        {
            _studentRepository = studentRepository;
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region Handle Functions
        public async Task<Student> AddStudentAsync(Student student, CancellationToken cancellationToken = default)
        {
            var students = await _studentRepository.AddAsync(student);
            await _unitOfWork.CompleteAsync(cancellationToken);
            return students;
        }

        public async Task<bool> EditStudentAsync(Student student, CancellationToken cancellationToken = default)
        {
            _studentRepository.UpdateAsync(student);
            await _unitOfWork.CompleteAsync(cancellationToken);
            return true;
        }

        public async Task<bool> DeleteStudentAsync(Student student, CancellationToken cancellationToken = default)
        {
            _studentRepository.DeleteAsync(student);
            await _unitOfWork.CompleteAsync(cancellationToken);
            return true;
        }

        public async Task<List<Student>> GetStudentListAsync()
        {
            var studentList = await _studentRepository.GetTableNoTracking()
                .Include(x => x.Department).ToListAsync();
            return studentList;
        }

        public IQueryable<Student> FilterStudentPaginatedQuerable(StudentOrderingEnum orderingEnum, string Search)
        {
            var queryable = _studentRepository.GetTableNoTracking().AsQueryable();
            if (Search != null)
            {
                queryable = queryable.Where(x => x.StudID.Equals(int.Parse(Search)) ||
                x.NameAr.Contains(Search) || x.NameEn.Contains(Search) || x.Phone.Contains(Search) ||
                x.Address.Contains(Search) || x.Department.DNameAr.Contains(Search));
            }
            switch (orderingEnum)
            {
                case StudentOrderingEnum.StudID:
                    queryable = queryable.OrderBy(x => x.StudID);
                    break;
                case StudentOrderingEnum.NameAr:
                    queryable = queryable.OrderBy(x => x.NameAr);
                    break;
                case StudentOrderingEnum.NameEn:
                    queryable = queryable.OrderBy(x => x.NameEn);
                    break;
                case StudentOrderingEnum.Phone:
                    queryable = queryable.OrderBy(x => x.Phone);
                    break;
                case StudentOrderingEnum.DepartmementName:
                    queryable = queryable.OrderBy(x => x.Department.DNameAr);
                    break;
            }
            return queryable;
        }

        public async Task<Student> GetStudentById(int id)
        {
            var student = _studentRepository.GetTableNoTracking().Include(x => x.Department)
                .Where(x => x.StudID.Equals(id)).FirstOrDefault();
            return student;
        }


        public IQueryable<Student> GetStudentsByDepartmentIDQuerable(int DID)
        {
            return _studentRepository.GetTableNoTracking().Where(x => x.DID.Equals(DID)).AsQueryable();
        }
        #endregion
    }
}
