﻿using CleanProject.Core.Bases;
using CleanProject.Core.Features.Authentication.Commands.Models;
using CleanProject.Core.Resources;
using CleanProject.Data.Entities.Identity;
using CleanProject.Data.Results;
using CleanProject.Srevice.IServices;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace CleanProject.Core.Features.Authentication.Commands.Handlers
{
    public class AuthenticationCommandHandler : ResponseHandler,
        IRequestHandler<SignInCommand, Response<JwtAuthResult>>,
        IRequestHandler<RefreshTokenCommand, Response<JwtAuthResult>>
    {
        #region Feilds
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IAuthenticationService _authenticationService;


        #endregion

        #region Constructors
        public AuthenticationCommandHandler(
            IStringLocalizer<SharedResources> stringLocalizer,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IAuthenticationService authenticationService) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _userManager = userManager;
            _signInManager = signInManager;
            _authenticationService = authenticationService;
        }


        #endregion

        #region Handle Function
        public async Task<Response<JwtAuthResult>> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            //get userName
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null) return BadRequest<JwtAuthResult>(_stringLocalizer[SharedResourcesKeys.UserNameIsNotExist]);

            var signInResult = _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

            if (!signInResult.IsCompletedSuccessfully) return BadRequest<JwtAuthResult>(_stringLocalizer[SharedResourcesKeys.PasswordNotCorrect]);

            var accessToken = await _authenticationService.GetJWTToken(user);

            return Success(accessToken);

        }
        public async Task<Response<JwtAuthResult>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var jwtToken = _authenticationService.ReadJWTToken(request.AccessToken);
            var userIdAndExpireDate = await _authenticationService.ValidateDetails(jwtToken, request.AccessToken, request.RefreshToken);
            switch (userIdAndExpireDate)
            {
                case ("AlgorithmIsWrong", null): return Unauthorized<JwtAuthResult>(_stringLocalizer[SharedResourcesKeys.AlgorithmIsWrong]);
                case ("TokenIsNotExpired", null): return Unauthorized<JwtAuthResult>(_stringLocalizer[SharedResourcesKeys.TokenIsNotExpired]);
                case ("RefreshTokenIsNotFound", null): return Unauthorized<JwtAuthResult>(_stringLocalizer[SharedResourcesKeys.RefreshTokenIsNotFound]);
                case ("RefreshTokenIsExpired", null): return Unauthorized<JwtAuthResult>(_stringLocalizer[SharedResourcesKeys.RefreshTokenIsExpired]);
            }
            var (userId, expiryDate) = userIdAndExpireDate;
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound<JwtAuthResult>();

            var result = await _authenticationService.GetRefreshToken(user, jwtToken, expiryDate, request.RefreshToken);
            return Success(result);
        }

        #endregion
    }
}
