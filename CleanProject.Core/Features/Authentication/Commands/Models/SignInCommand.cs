using CleanProject.Core.Bases;
using CleanProject.Data.Results;
using MediatR;

namespace CleanProject.Core.Features.Authentication.Commands.Models
{
    public class SignInCommand : IRequest<Response<JwtAuthResult>>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
