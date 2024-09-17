using CleanProject.Core.Bases;
using CleanProject.Data.Results;
using MediatR;

namespace CleanProject.Core.Features.Authorization.Queries.Modles
{
    public class ManageUserRolesQuery : IRequest<Response<ManageUserRolesResult>>
    {
        public int UserId { get; set; }
    }
}
