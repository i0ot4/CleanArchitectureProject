using AutoMapper;
using CleanProject.Core.Bases;
using CleanProject.Core.Features.Users.Queries.Models;
using CleanProject.Core.Features.Users.Queries.Results;
using CleanProject.Core.Resources;
using CleanProject.Core.Wrappers;
using CleanProject.Data.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace CleanProject.Core.Features.Users.Queries.Handlers
{
    public class UserQueryHandler : ResponseHandler,
        IRequestHandler<GetUserListQuery, PaginatedResult<GetUserListResponse>>,
        IRequestHandler<GetUserByIdQuery, Response<GetUserByIdResponse>>
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _sharedResources;
        private readonly UserManager<User> _userManager;

        #endregion


        #region Conatructor
        public UserQueryHandler(IStringLocalizer<SharedResources> stringLocalizer, IMapper mapper, UserManager<User> userManager) : base(stringLocalizer)
        {
            _mapper = mapper;
            _sharedResources = stringLocalizer;
            _userManager = userManager;
        }

        #endregion


        #region Handle Functions
        public async Task<PaginatedResult<GetUserListResponse>> Handle(GetUserListQuery request, CancellationToken cancellationToken)
        {
            var users = _userManager.Users.AsQueryable();
            var paginatedUser = await _mapper.ProjectTo<GetUserListResponse>(users)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);

            return paginatedUser;
        }

        public async Task<Response<GetUserByIdResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {

            var users = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (users == null) return NotFound<GetUserByIdResponse>(_sharedResources[SharedResourcesKeys.NotFound]);

            var result = _mapper.Map<GetUserByIdResponse>(users);
            return Success(result);
        }
        #endregion
    }
}
