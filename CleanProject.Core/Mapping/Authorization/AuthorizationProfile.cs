using AutoMapper;

namespace CleanProject.Core.Mapping.Authorization
{
    public partial class AuthorizationProfile : Profile
    {
        public AuthorizationProfile()
        {
            #region Query
            GetRoleListMapping();
            GetRoleByIdMapping();
            #endregion

            #region Command

            #endregion
        }
    }
}
