using CleanProject.Data.Entities.Identity;
using CleanProject.Data.Results;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CleanProject.Srevice.IServices
{
    public interface IAuthenticationService
    {


        Task<List<Claim>> GetClaims(User user);
        Task<JwtAuthResult> GetJWTToken(User user, CancellationToken cancellationToken = default);
        JwtSecurityToken ReadJWTToken(string accessToken);
        Task<(string, DateTime?)> ValidateDetails(JwtSecurityToken jwtToken, string accessToken, string refreshToken, CancellationToken cancellationToken = default);
        public Task<JwtAuthResult> GetRefreshToken(User user, JwtSecurityToken jwtToken, DateTime? expiryDate, string refreshToken);
        public Task<string> ValidateToken(string accessToken);

    }
}
