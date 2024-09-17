using CleanProject.Core.Features.Students.Queries.Results;
using CleanProject.Data.Entities;

namespace CleanProject.Core.Mapping.Students
{
    public partial class StudentProfile
    {
        public void GetStudentByIdQueryMapping()
        {
            CreateMap<Student, GetStudentByIdResponse>()
                .ForMember(dest => dest.DepartmementName, opt => opt.MapFrom(src => src.Department.DNameAr));
        }
    }
}
