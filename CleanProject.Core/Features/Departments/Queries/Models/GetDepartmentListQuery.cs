using CleanProject.Core.Bases;
using CleanProject.Core.Features.Departments.Queries.Results;
using MediatR;

namespace CleanProject.Core.Features.Departments.Queries.Models
{
    public class GetDepartmentListQuery : IRequest<Response<List<GetDepartmentListResponse>>>
    {
    }
}
