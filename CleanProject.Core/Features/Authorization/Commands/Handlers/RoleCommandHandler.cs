using CleanProject.Core.Bases;
using CleanProject.Core.Features.Authorization.Commands.Models;
using CleanProject.Core.Resources;
using CleanProject.Srevice.IServices;
using MediatR;
using Microsoft.Extensions.Localization;

namespace CleanProject.Core.Features.Authorization.Commands.Handlers
{
    public class RoleCommandHandler : ResponseHandler,
                                      IRequestHandler<AddRoleCommand, Response<string>>,
                                      IRequestHandler<EditRoleCommand, Response<string>>,
                                      IRequestHandler<DeleteRoleCommand, Response<string>>,
                                      IRequestHandler<UpdateUserRolesCommand, Response<string>>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IAuthorizationService _authorizationService;
        #endregion


        #region Constructors
        public RoleCommandHandler(IStringLocalizer<SharedResources> stringLocalizer, IAuthorizationService authorizationService) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _authorizationService = authorizationService;
        }


        #endregion


        #region Handle Functions
        public async Task<Response<string>> Handle(AddRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _authorizationService.AddRoleAsync(request.RoleName);
            if (role == "Success") return Success("");
            return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.AddFailed]);
        }

        public async Task<Response<string>> Handle(EditRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _authorizationService.EditRoleAsync(request);
            if (role == "Success") return Success((string)_stringLocalizer[SharedResourcesKeys.Updated]);
            else if (role == "NotFound") return NotFound<string>();
            else return BadRequest<string>(role);
        }

        public async Task<Response<string>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _authorizationService.DeleteRoleAsync(request.Id);
            switch (role)
            {
                case "Success": return Success<string>(_stringLocalizer[SharedResourcesKeys.Deleted]);
                case "NotFound": return NotFound<string>();
                case "Used": return Success<string>(_stringLocalizer[SharedResourcesKeys.RoleIsUsed]);
                default: return BadRequest<string>(role);
            }
        }

        public async Task<Response<string>> Handle(UpdateUserRolesCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizationService.UpdateUserRoles(request, cancellationToken);
            switch (result)
            {
                case "UserIsNull": return NotFound<string>(_stringLocalizer[SharedResourcesKeys.UserIsNotFound]);
                case "FailedToRemoveOldRoles": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToRemoveOldRoles]);
                case "FailedToAddNewRoles": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToAddNewRoles]);
                case "FailedToUpdateUserRoles": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToUpdateUserRoles]);
            }
            return Success<string>(_stringLocalizer[SharedResourcesKeys.Success]);
        }


        #endregion
    }
}
