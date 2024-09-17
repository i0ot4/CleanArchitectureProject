using CleanProject.Data.Entities.Identity;
using CleanProject.Infrastructure.InfrastructureBases;

namespace CleanProject.Infrastructure.Repositories.IRepository
{
    public interface IRefreshTokenRepository : IGenericRepositoryAsync<UserRefreshToken>
    {
    }
}
