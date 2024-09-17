using CleanProject.Data.Entities.Identity;
using CleanProject.Infrastructure.Context;
using CleanProject.Infrastructure.InfrastructureBases;
using CleanProject.Infrastructure.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;

namespace CleanProject.Infrastructure.Repositories.Implmentation
{
    public class RefreshTokenRepository : GenericRepositoryAsync<UserRefreshToken>, IRefreshTokenRepository
    {
        #region Fields
        private DbSet<UserRefreshToken> _refreshToken;
        #endregion

        #region Constructors
        public RefreshTokenRepository(ApplicationDBContext context) : base(context)
        {
            _refreshToken = context.Set<UserRefreshToken>();
        }
        #endregion

        #region Handle Function

        #endregion
    }
}
