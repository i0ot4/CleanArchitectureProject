using CleanProject.Core.Features.Users.Queries.Results;
using CleanProject.Data.Entities.Identity;

namespace CleanProject.Core.Mapping.Users
{
    public partial class UserProfile
    {
        public void GetUserListMapping()
        {
            CreateMap<User, GetUserListResponse>();
        }
    }
}
