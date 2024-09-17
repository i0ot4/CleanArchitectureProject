using CleanProject.Core.Bases;
using CleanProject.Data.Requests;
using MediatR;

namespace CleanProject.Core.Features.Authorization.Commands.Models
{
    public class UpdateUserRolesCommand : UpdateUserRolesRequest, IRequest<Response<string>>
    {
    }
}
