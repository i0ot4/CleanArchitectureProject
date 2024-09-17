using CleanProject.Core.Features.Students.Queries.Results;
using CleanProject.Core.Wrappers;
using CleanProject.Data.Enum;
using MediatR;

namespace CleanProject.Core.Features.Students.Queries.Models
{
    public class GetStudentPaginatedQuery : IRequest<PaginatedResult<GetStudentPaginatedResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public StudentOrderingEnum OrderBy { get; set; }
        public string? Search { get; set; }
    }
}
