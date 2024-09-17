using CleanProject.Core.Features.Students.Commands.Models;
using CleanProject.Data.Entities;

namespace CleanProject.Core.Mapping.Students
{
    public partial class StudentProfile
    {
        public void EditStudentCommandMapping()
        {
            CreateMap<EditStudentCommand, Student>()
                .ForMember(dest => dest.NameAr, opt => opt.MapFrom(src => src.NameAr))
                .ForMember(dest => dest.NameEn, opt => opt.MapFrom(src => src.NameEn))
                .ForMember(dest => dest.DID, opt => opt.MapFrom(src => src.DepartmementId));
        }
    }
}
