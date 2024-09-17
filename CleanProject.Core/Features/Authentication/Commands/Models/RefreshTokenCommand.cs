using CleanProject.Core.Bases;
using CleanProject.Data.Results;
using MediatR;

namespace CleanProject.Core.Features.Authentication.Commands.Models
{
    public class RefreshTokenCommand : IRequest<Response<JwtAuthResult>>
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
