using CleanProject.Core.Bases;
using MediatR;

namespace CleanProject.Core.Features.Students.Commands.Models
{
    public class EditStudentCommand : IRequest<Result<bool>>
    {
        public int StudID { get; set; }
        public string? NameAr { get; set; }
        public string? NameEn { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public int DepartmementId { get; set; }

    }
}
