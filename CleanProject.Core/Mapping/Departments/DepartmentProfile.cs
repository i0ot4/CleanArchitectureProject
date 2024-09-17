using AutoMapper;

namespace CleanProject.Core.Mapping.Departments
{
    public partial class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            #region Query Function
            GetDepartmentListQueryMapping();
            GetDepartmentByIdQueryMapping();
            GetDepartmentPaginatedMapping();
            #endregion

            #region Command Function
            DepartAddDepartmentCommandMappingmentProfile();
            EditDepartmentCommandMapping();
            #endregion
        }
    }
}
