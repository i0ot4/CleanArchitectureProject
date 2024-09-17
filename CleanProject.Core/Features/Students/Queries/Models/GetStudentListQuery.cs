using CleanProject.Core.Bases;
using CleanProject.Core.Features.Students.Queries.Results;
using MediatR;

namespace CleanProject.Core.Features.Students.Queries.Models
{
    public class GetStudentListQuery : IRequest<Result<List<GetStudentListResponse>>>
    {
    }
}
