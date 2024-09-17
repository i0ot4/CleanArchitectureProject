using CleanProject.Core.Bases;
using MediatR;

namespace CleanProject.Core.Features.Authorization.Commands.Models
{
    public class AddRoleCommand : IRequest<Response<string>>
    {
        public string RoleName { get; set; }
    }
}
