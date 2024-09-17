using CleanProject.Core.Wrappers;

namespace CleanProject.Core.Features.Departments.Queries.Results
{
    public class GetDepartmentByIdResponse
    {
        public int Id { get; set; }
        public string DNameAr { get; set; }
        public string DNameEn { get; set; }
        public string ManagerName { get; set; }
        public PaginatedResult<StudentResponse>? StudentList { get; set; }
        public List<SubjectResponse>? SubjectList { get; set; }
        public List<InstructorResponse>? InstructorList { get; set; }

        public class StudentResponse
        {
            public int Id { get; set; }
            public string NameAr { get; set; }
            public string NameEn { get; set; }

            public StudentResponse(int id, string nameAr, string nameEn)
            {
                Id = id;
                NameAr = nameAr;
                NameEn = nameEn;
            }


        }

        public class SubjectResponse
        {
            public int Id { get; set; }
            public string NameAr { get; set; }
            public string NameEn { get; set; }
        }

        public class InstructorResponse
        {
            public int Id { get; set; }
            public string NameAr { get; set; }
            public string NameEn { get; set; }
        }

    }
}
