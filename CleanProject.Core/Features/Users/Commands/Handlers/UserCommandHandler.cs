using AutoMapper;
using CleanProject.Core.Bases;
using CleanProject.Core.Features.Users.Commands.Models;
using CleanProject.Core.Resources;
using CleanProject.Data.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace CleanProject.Core.Features.Users.Commands.Handlers
{
    public class UserCommandHandler : ResponseHandler,
        IRequestHandler<AddUserCommand, Response<string>>,
        IRequestHandler<EditUserCommand, Response<string>>,
        IRequestHandler<DeleteUserCommand, Response<string>>,
        IRequestHandler<ChangeUserPasswordCommand, Response<string>>
    {
        #region Fields
        private readonly UserManager<User> _userManager;
        private readonly IStringLocalizer<SharedResources> _sharedResources;
        private readonly IMapper _mapper;
        #endregion



        #region Construcrors
        public UserCommandHandler(UserManager<User> userManager, IMapper mapper, IStringLocalizer<SharedResources> sharedResources) : base(sharedResources)
        {
            _userManager = userManager;
            _mapper = mapper;
            _sharedResources = sharedResources;
        }

        #endregion


        #region Action

        public async Task<Response<string>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            //if Email is Exist
            var user = await _userManager.FindByEmailAsync(request.Email);
            //Email is Exist
            if (user != null) return BadRequest<string>(_sharedResources[SharedResourcesKeys.EmailIsExist]);
            //if username is Exist
            var userByUserName = await _userManager.FindByNameAsync(request.UserName);
            //username is Exist
            if (userByUserName != null) return BadRequest<string>(_sharedResources[SharedResourcesKeys.UserNameIsExist]);
            //Mapping
            var userMapping = _mapper.Map<User>(request);
            //Create
            var createResult = await _userManager.CreateAsync(userMapping, request.Password);
            //Failed
            if (!createResult.Succeeded) return BadRequest<string>(_sharedResources[SharedResourcesKeys.FaildToAddUser]);
            //message
            //Create
            //Success
            return Created("");
        }

        public async Task<Response<string>> Handle(EditUserCommand request, CancellationToken cancellationToken)
        {
            var oldUser = await _userManager.FindByIdAsync(request.Id.ToString());
            if (oldUser == null) return NotFound<string>(_sharedResources[SharedResourcesKeys.NotFound]);

            var newUser = _mapper.Map(request, oldUser);

            //if username is Exist
            var userByUserName = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == newUser.UserName && x.Id != newUser.Id);
            //username is Exist
            if (userByUserName != null) return BadRequest<string>(_sharedResources[SharedResourcesKeys.UserNameIsExist]);

            var result = await _userManager.UpdateAsync(newUser);

            if (!result.Succeeded) return BadRequest<string>(_sharedResources[SharedResourcesKeys.UpdateFailed]);

            return Success((string)_sharedResources[SharedResourcesKeys.Updated]);
        }

        public async Task<Response<string>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Id.ToString());
            if (user == null) return NotFound<string>(_sharedResources[SharedResourcesKeys.NotFound]);

            var result = await _userManager.DeleteAsync(user);

            if (!result.Succeeded) return BadRequest<string>(_sharedResources[SharedResourcesKeys.DeletedFailed]);
            return Success((string)_sharedResources[SharedResourcesKeys.Deleted]);
        }

        public async Task<Response<string>> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
        {
            //get user
            //check if user is exist
            var user = await _userManager.FindByIdAsync(request.Id.ToString());
            //if Not Exist notfound
            if (user == null) return NotFound<string>();

            //Change User Password
            var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
            //var user1=await _userManager.HasPasswordAsync(user);
            //await _userManager.RemovePasswordAsync(user);
            //await _userManager.AddPasswordAsync(user, request.NewPassword);

            //result
            if (!result.Succeeded) return BadRequest<string>(result.Errors.FirstOrDefault().Description);
            return Success((string)_sharedResources[SharedResourcesKeys.Success]);
        }


        #endregion

    }
}
