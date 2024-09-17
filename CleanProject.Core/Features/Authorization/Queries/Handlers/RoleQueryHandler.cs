using AutoMapper;
using CleanProject.Core.Bases;
using CleanProject.Core.Features.Authorization.Queries.Modles;
using CleanProject.Core.Features.Authorization.Queries.Results;
using CleanProject.Core.Resources;
using CleanProject.Data.Entities.Identity;
using CleanProject.Data.Results;
using CleanProject.Srevice.IServices;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace CleanProject.Core.Features.Authorization.Queries.Handlers
{
    public class RoleQueryHandler : ResponseHandler,
                                    IRequestHandler<GetRoleListQuery, Response<List<GetRoleListResponse>>>,
                                    IRequestHandler<GetRoleByIdQuery, Response<GetRoleByIdResponse>>,
                                    IRequestHandler<ManageUserRolesQuery, Response<ManageUserRolesResult>>
    {
        #region Feilds
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IAuthorizationService _authorizationService;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        #endregion

        #region Constructors
        public RoleQueryHandler(IStringLocalizer<SharedResources> stringLocalizer, IAuthorizationService authorizationService, IMapper mapper, UserManager<User> userManager) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _authorizationService = authorizationService;
            _mapper = mapper;
            _userManager = userManager;
        }


        #endregion

        #region Handle Function
        public async Task<Response<List<GetRoleListResponse>>> Handle(GetRoleListQuery request, CancellationToken cancellationToken)
        {
            var roles = await _authorizationService.GetRolesList();
            var rolesMapping = _mapper.Map<List<GetRoleListResponse>>(roles);
            return Success(rolesMapping);
        }

        public async Task<Response<GetRoleByIdResponse>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var role = await _authorizationService.GetRoleById(request.Id);
            if (role == null) return NotFound<GetRoleByIdResponse>();
            var roleMapping = _mapper.Map<GetRoleByIdResponse>(role);
            return Success(roleMapping);
        }

        public async Task<Response<ManageUserRolesResult>> Handle(ManageUserRolesQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user == null) return NotFound<ManageUserRolesResult>();

            var result = await _authorizationService.ManageUserRolesData(user);
            return Success(result);
        }


        #endregion

    }
}
