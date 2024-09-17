using AutoMapper;

namespace CleanProject.Core.Mapping.Students
{
    public partial class StudentProfile : Profile
    {
        public StudentProfile()
        {
            #region Commands
            AddStudentCommandMapping();
            EditStudentCommandMapping();
            #endregion

            #region Queries
            GetStudentQueryMapping();
            GetStudentByIdQueryMapping();
            GetStudentPaginatedQueryMapping();
            #endregion
        }
    }
}
