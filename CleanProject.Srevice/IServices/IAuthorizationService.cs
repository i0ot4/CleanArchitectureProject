using CleanProject.Data.Entities.Identity;
using CleanProject.Data.Requests;
using CleanProject.Data.Results;

namespace CleanProject.Srevice.IServices
{
    public interface IAuthorizationService
    {
        public Task<string> AddRoleAsync(string roleName);
        public Task<bool> IsRoleExistByName(string roleName);
        public Task<string> EditRoleAsync(EditRoleRequest request);
        Task<string> DeleteRoleAsync(int roleId);
        Task<string> UpdateUserRoles(UpdateUserRolesRequest request, CancellationToken cancellationToken);
        Task<bool> IsRoleExistById(int roleId);
        Task<List<Role>> GetRolesList();
        Task<Role> GetRoleById(int Id);
        Task<ManageUserRolesResult> ManageUserRolesData(User user);
    }
}
