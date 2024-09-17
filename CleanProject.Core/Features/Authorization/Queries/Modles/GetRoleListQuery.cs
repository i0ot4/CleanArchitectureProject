using CleanProject.Core.Bases;
using CleanProject.Core.Features.Authorization.Queries.Results;
using MediatR;

namespace CleanProject.Core.Features.Authorization.Queries.Modles
{
    public class GetRoleListQuery : IRequest<Response<List<GetRoleListResponse>>>
    {
    }
}
