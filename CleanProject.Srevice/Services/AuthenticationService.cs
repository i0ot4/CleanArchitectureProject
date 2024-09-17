using CleanProject.Data.Entities.Identity;
using CleanProject.Data.helper;
using CleanProject.Data.Results;
using CleanProject.Infrastructure.InfrastructureBases;
using CleanProject.Infrastructure.Repositories.IRepository;
using CleanProject.Srevice.IServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CleanProject.Srevice.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        #region Feilds
        private readonly JwtSettings _jwtSettings;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        #endregion

        #region Constructor
        public AuthenticationService(JwtSettings jwtSettings, IRefreshTokenRepository refreshTokenRepository, IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            _jwtSettings = jwtSettings;
            _refreshTokenRepository = refreshTokenRepository;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }
        #endregion

        #region Handle Function

        public async Task<JwtAuthResult> GetJWTToken(User user, CancellationToken cancellationToken = default)
        {
            //This Func is To Get The Token and The RefreshToken And Using Them in the System

            //Generate The Token, Get the Token Data In The Parameter (jwtToken) and the Token String In The Parameter (accessToken)
            var (jwtToken, accessToken) = await GenerateJWTToken(user);
            //Generate The RefreshToken and the RefreshToken String In The Parameter (refreshToken)
            var refreshToken = GetRefreshToken(user.UserName);
            //Register The RefreshToken Data in the Object (userRefreshToken) To Store and Save The Data of the RefreshToken in the DataBase
            var userRefreshToken = new UserRefreshToken
            {
                AddedTime = DateTime.Now,
                ExpiryDate = DateTime.Now.AddDays(_jwtSettings.RefreshTokenExpireDate),
                JwtId = jwtToken.Id,
                IsRevoked = false,
                IsUsed = false,
                RefreshToken = refreshToken.TokenString,
                Token = accessToken,
                UserId = user.Id,
            };

            //Store The Data of the RefreshToken from the Object (userRefreshToken) in the DataBase
            await _refreshTokenRepository.AddAsync(userRefreshToken);
            //Save and Commit The Data in the DataBase
            await _unitOfWork.CompleteAsync(cancellationToken);
            //Get the Token And RefreshToken to return them on the Object response of the type Class(JwtAuthResult)
            var response = new JwtAuthResult();
            response.refreshToken = refreshToken;
            response.AccessToken = accessToken;
            //Return Token And RefreshToken On the Parameter (response)
            return response;
        }

        private async Task<(JwtSecurityToken, string)> GenerateJWTToken(User user)
        {
            //Get The User Claims (Data)
            var claims = await GetClaims(user);
            //Generate The Token
            var jwtToken = new JwtSecurityToken(
                _jwtSettings.Issuer,
                _jwtSettings.Audience,
                claims,
                expires: DateTime.Now.AddMinutes(_jwtSettings.AccessTokenExpireDate),
                //Security Algorithms Of the Token Generations
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret)), SecurityAlgorithms.HmacSha256Signature)
                );
            //Store the token that Generate in the accessToken
            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            //return the Token Data (jwtToken) and the Token String (accessToken)
            return (jwtToken, accessToken);
        }
        private RefreshToken GetRefreshToken(string userName)
        {
            var refreshToken = new RefreshToken
            {
                ExpireAt = DateTime.Now.AddDays(_jwtSettings.RefreshTokenExpireDate),
                UserName = userName,
                //Get the RefreshToken String and Store It
                TokenString = GenerateRefreshToken()
            };
            //Return the RefreshToken String
            return refreshToken;
        }
        private string GenerateRefreshToken()
        {
            //This Func is To Generate The RefreshToken String
            var randomNumber = new byte[32];
            var randomNumberGenerate = RandomNumberGenerator.Create();
            randomNumberGenerate.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public async Task<List<Claim>> GetClaims(User user)
        {
            //This Func is To Store The User Data in the Claims to Used it on the Token Generation
            var roles = await _userManager.GetRolesAsync(user);
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.NameIdentifier,user.UserName),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(nameof(UserClaimModel.PhoneNumber), user.PhoneNumber),
                new Claim(nameof(UserClaimModel.Id), user.Id.ToString())
            };
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var userClaims = await _userManager.GetClaimsAsync(user);
            claims.AddRange(userClaims);
            return claims;
        }

        public async Task<(string, DateTime?)> ValidateDetails(JwtSecurityToken jwtToken, string accessToken, string refreshToken, CancellationToken cancellationToken = default)
        {
            //Chack If the Security Algorithms Is The Same on the Tokens and The User Using
            if (jwtToken == null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature))
            {
                return ("AlgorithmIsWrong", null);
            }
            //Chack If the Token Is Expired or Not
            if (jwtToken.ValidTo > DateTime.UtcNow)
            {
                return ("TokenIsNotExpired", null);
            }

            //Get User
            var userId = jwtToken.Claims.FirstOrDefault(x => x.Type == nameof(UserClaimModel.Id)).Value;
            //Get The User RefreshToken By The User Id, User AccessToken and User RefreshToken
            var userRefreshToken = await _refreshTokenRepository.GetTableNoTracking()
                                                           .FirstOrDefaultAsync(x => x.RefreshToken == refreshToken &&
                                                           x.Token == accessToken && x.UserId == int.Parse(userId));
            //Chack If the Token Is Found or Not
            if (userRefreshToken == null)
            {
                return ("RefreshTokenIsNotFound", null);
            }
            //Chack If the RefreshToken Date Is Expired or Not
            if (userRefreshToken.ExpiryDate < DateTime.UtcNow)
            {
                //If The RefreshToken Date Is Expired
                userRefreshToken.IsRevoked = true;
                userRefreshToken.IsUsed = false;
                //Update The User RefreshToken Data
                _refreshTokenRepository.UpdateAsync(userRefreshToken);
                //Save Ans Commit The User RefreshToken (Make It Expired On The DataBase)
                await _unitOfWork.CompleteAsync(cancellationToken);
                return ("RefreshTokenIsExpired", null);
            }
            //If The RefreshToken Date Is NotExpired Store The User RefreshToken Expired Date in the Parameter(expirydate)
            var expirydate = userRefreshToken.ExpiryDate;
            //Return The User RefreshToken Expired Date
            return (userId, expirydate);
        }

        public JwtSecurityToken ReadJWTToken(string accessToken)
        {
            //Check If the accessToken Is (Empty || null) or Not
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new ArgumentNullException(nameof(accessToken));
            }

            var handler = new JwtSecurityTokenHandler();
            //Read The accessToken
            var response = handler.ReadJwtToken(accessToken);
            //Return The Token That Read If Is Null Or not 
            return response;
        }

        public Task<string> ValidateToken(string accessToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var parameters = new TokenValidationParameters
            {
                ValidateIssuer = _jwtSettings.ValidateIssuer,
                ValidIssuers = new[] { _jwtSettings.Issuer },
                ValidateIssuerSigningKey = _jwtSettings.ValidateIssuerSigningKey,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret)),
                ValidAudience = _jwtSettings.Audience,
                ValidateAudience = _jwtSettings.ValidateAudience,
                ValidateLifetime = _jwtSettings.ValidateLifeTime,
            };

            try
            {
                var validator = handler.ValidateToken(accessToken, parameters, out SecurityToken validatedToken);
                if (validator == null)
                {
                    return Task.FromResult("InvalidToken");
                }
                return Task.FromResult("NotExpired");
            }
            catch (Exception ex)
            {
                return Task.FromResult(ex.Message);
            }
        }

        public async Task<JwtAuthResult> GetRefreshToken(User user, JwtSecurityToken jwtToken, DateTime? expiryDate, string refreshToken)
        {
            //Generate Token
            var (jwtSecurityToken, newToken) = await GenerateJWTToken(user);
            //store The Old Token and RefreshToken in object to return them
            var response = new JwtAuthResult();
            //Store The Token String in JwtAuthResult
            response.AccessToken = newToken;
            //Get The RefreshToken Data
            var refreshTokenResult = new RefreshToken();
            //Get UserName and Store it in The RefreshToken UserName
            refreshTokenResult.UserName = jwtToken.Claims.FirstOrDefault(x => x.Type == nameof(UserClaimModel.UserName)).Value;
            //Get The RefreshToken Expiry Date
            refreshTokenResult.ExpireAt = (DateTime)expiryDate;
            //Get The RefreshToken String
            refreshTokenResult.TokenString = refreshToken;
            //Store The RefreshToken Data in JwtAuthResult
            response.refreshToken = refreshTokenResult;
            //Return The Old Token and RefreshToken
            return response;
        }


        #endregion
    }
}
