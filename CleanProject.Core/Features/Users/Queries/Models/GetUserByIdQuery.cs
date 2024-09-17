using CleanProject.Core.Bases;
using CleanProject.Core.Features.Users.Queries.Results;
using MediatR;

namespace CleanProject.Core.Features.Users.Queries.Models
{
    public class GetUserByIdQuery : IRequest<Response<GetUserByIdResponse>>
    {
        public int Id { get; set; }
    }
}
