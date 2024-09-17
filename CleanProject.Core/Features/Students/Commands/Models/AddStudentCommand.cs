using CleanProject.Core.Bases;
using CleanProject.Data.Entities;
using MediatR;

namespace CleanProject.Core.Features.Students.Commands.Models
{
    public class AddStudentCommand : IRequest<Result<Student>>
    {
        public string? NameAr { get; set; }
        public string? NameEn { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public int DepartmementId { get; set; }
    }
}
