using CleanProject.Core.Features.Authorization.Queries.Results;
using CleanProject.Data.Entities.Identity;

namespace CleanProject.Core.Mapping.Authorization
{
    public partial class AuthorizationProfile
    {
        public void GetRoleByIdMapping()
        {
            CreateMap<Role, GetRoleByIdResponse>();
        }
    }
}
