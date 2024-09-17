using CleanProject.Core.Bases;
using MediatR;

namespace CleanProject.Core.Features.Students.Commands.Models
{
    public class DeleteStudentCommand : IRequest<Result<bool>>
    {
        public int StudID { get; set; }
        public DeleteStudentCommand(int id)
        {
            StudID = id;
        }
    }
}
