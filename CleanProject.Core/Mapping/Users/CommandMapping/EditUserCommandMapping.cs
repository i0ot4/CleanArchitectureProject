using CleanProject.Core.Features.Users.Commands.Models;
using CleanProject.Data.Entities.Identity;

namespace CleanProject.Core.Mapping.Users
{
    public partial class UserProfile
    {
        public void EditUserCommandMapping()
        {
            CreateMap<EditUserCommand, User>();
        }
    }
}
