using CleanProject.Core.Bases;
using MediatR;

namespace CleanProject.Core.Features.Users.Commands.Models
{
    public class DeleteUserCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
    }
}
