using CleanProject.Core.Features.Students.Queries.Results;
using CleanProject.Data.Entities;

namespace CleanProject.Core.Mapping.Students
{
    public partial class StudentProfile
    {
        public void GetStudentPaginatedQueryMapping()
        {
            CreateMap<Student, GetStudentPaginatedResponse>()
                .ForMember(dest => dest.DepartmementName, opt => opt.MapFrom(src => src.Department.DNameAr));
        }
    }
}
