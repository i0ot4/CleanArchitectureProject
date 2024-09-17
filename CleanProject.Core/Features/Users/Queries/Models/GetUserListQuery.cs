using CleanProject.Core.Features.Users.Queries.Results;
using CleanProject.Core.Wrappers;
using MediatR;

namespace CleanProject.Core.Features.Users.Queries.Models
{
    public class GetUserListQuery : IRequest<PaginatedResult<GetUserListResponse>>
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
