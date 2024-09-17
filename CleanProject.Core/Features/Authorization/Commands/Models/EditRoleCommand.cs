using CleanProject.Core.Bases;
using CleanProject.Data.Requests;
using MediatR;

namespace CleanProject.Core.Features.Authorization.Commands.Models
{
    public class EditRoleCommand : EditRoleRequest, IRequest<Response<string>>
    {
    }
}
