using AutoMapper;

namespace CleanProject.Core.Mapping.Users
{
    public partial class UserProfile : Profile
    {
        public UserProfile()
        {

            #region Query
            GetUserListMapping();
            GetUserByIdMapping();
            #endregion
            #region Command
            AddUserCommandMapping();
            EditUserCommandMapping();
            #endregion
        }
    }
}
