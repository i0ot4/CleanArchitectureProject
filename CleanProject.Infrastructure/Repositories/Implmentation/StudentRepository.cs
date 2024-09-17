using CleanProject.Data.Entities;
using CleanProject.Infrastructure.Context;
using CleanProject.Infrastructure.InfrastructureBases;
using CleanProject.Infrastructure.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;

namespace CleanProject.Infrastructure.Repositories.Implmentation
{
    public class StudentRepository : GenericRepositoryAsync<Student>, IStudentRepository
    {
        #region Feilds
        private DbSet<Student> _students;

        #endregion

        #region Constructor
        public StudentRepository(ApplicationDBContext context) : base(context)
        {
            _students = context.Set<Student>();
        }
        #endregion

        #region Handle Functions

        public async Task<List<Student>> GetStudentListAsync()
        {
            return await _students.ToListAsync();
        }
        #endregion
    }
}
