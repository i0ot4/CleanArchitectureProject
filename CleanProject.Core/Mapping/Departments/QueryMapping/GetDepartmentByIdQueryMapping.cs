using CleanProject.Core.Features.Departments.Queries.Results;
using CleanProject.Data.Entities;
using static CleanProject.Core.Features.Departments.Queries.Results.GetDepartmentByIdResponse;

namespace CleanProject.Core.Mapping.Departments
{
    public partial class DepartmentProfile
    {
        public void GetDepartmentByIdQueryMapping()
        {
            CreateMap<Department, GetDepartmentByIdResponse>()
                .ForMember(dest => dest.DNameAr, opt => opt.MapFrom(src => src.DNameAr))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.DID))
                .ForMember(dest => dest.ManagerName, opt => opt.MapFrom(src => src.Instructor.ENameAr))
                //List Mapping
                .ForMember(dest => dest.SubjectList, opt => opt.MapFrom(src => src.DepartmentSubjects))
                //.ForMember(dest => dest.StudentList, opt => opt.MapFrom(src => src.Students))
                .ForMember(dest => dest.InstructorList, opt => opt.MapFrom(src => src.Instructors));

            CreateMap<DepartmetSubject, SubjectResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.SubID))
                .ForMember(dest => dest.NameAr, opt => opt.MapFrom(src => src.Subject.SubjectNameAr))
                .ForMember(dest => dest.NameEn, opt => opt.MapFrom(src => src.Subject.SubjectNameEn));

            /*CreateMap<Student, StudentResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.StudID))
                .ForMember(dest => dest.NameAr, opt => opt.MapFrom(src => src.NameAr))
                .ForMember(dest => dest.NameEn, opt => opt.MapFrom(src => src.NameEn));*/

            CreateMap<Instructor, InstructorResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.InsId))
                .ForMember(dest => dest.NameAr, opt => opt.MapFrom(src => src.ENameAr))
                .ForMember(dest => dest.NameEn, opt => opt.MapFrom(src => src.ENameEn));
        }
    }
}
