using CleanProject.Core.Bases;
using CleanProject.Core.Features.Students.Queries.Results;
using MediatR;

namespace CleanProject.Core.Features.Students.Queries.Models
{
    public class GetStudentByIdQuery : IRequest<Result<GetStudentByIdResponse>>
    {
        public int Id { get; set; }
        public GetStudentByIdQuery(int id)
        {
            Id = id;
        }
    }
}
