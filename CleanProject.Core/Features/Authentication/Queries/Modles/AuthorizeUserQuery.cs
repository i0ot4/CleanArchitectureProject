using CleanProject.Core.Bases;
using MediatR;

namespace CleanProject.Core.Features.Authentication.Queries.Modles
{
    public class AuthorizeUserQuery : IRequest<Response<string>>
    {
        public string AccessToken { get; set; }
    }
}
