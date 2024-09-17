using CleanProject.Core.Bases;
using CleanProject.Core.Features.Authentication.Queries.Modles;
using CleanProject.Core.Resources;
using CleanProject.Srevice.IServices;
using MediatR;
using Microsoft.Extensions.Localization;

namespace CleanProject.Core.Features.Authentication.Queries.Handlers
{
    public class AuthenticationQueryHandler : ResponseHandler,
                                              IRequestHandler<AuthorizeUserQuery, Response<string>>
    {
        #region Feilds
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IAuthenticationService _authenticationService;


        #endregion

        #region Constructors
        public AuthenticationQueryHandler(
            IStringLocalizer<SharedResources> stringLocalizer,
            IAuthenticationService authenticationService) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _authenticationService = authenticationService;
        }

        #endregion

        #region Handle Functions
        public async Task<Response<string>> Handle(AuthorizeUserQuery request, CancellationToken cancellationToken)
        {
            var result = await _authenticationService.ValidateToken(request.AccessToken);
            if (result == "NotExpired") return Success(result);
            return Unauthorized<string>(_stringLocalizer[SharedResourcesKeys.TokenIsExpired]);
        }
        #endregion
    }
}
