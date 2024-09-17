using CleanProject.Core.Features.Departments.Queries.Results;
using CleanProject.Core.Wrappers;
using CleanProject.Data.Enum;
using MediatR;

namespace CleanProject.Core.Features.Departments.Queries.Models
{
    public class GetDepartmentPaginatedListQuery : IRequest<PaginatedResult<GetDepartmentPaginatedListResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public DepartmentOrderingEnum OrderBy { get; set; }
        public string? Search { get; set; }
    }
}
