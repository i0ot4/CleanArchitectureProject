using CleanProject.Core.Bases;
using MediatR;

namespace CleanProject.Core.Features.Authorization.Commands.Models
{
    public class DeleteRoleCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
    }
}
