using CleanProject.Core.Features.Departments.Commands.Models;
using CleanProject.Data.Entities;

namespace CleanProject.Core.Mapping.Departments
{
    public partial class DepartmentProfile
    {
        public void EditDepartmentCommandMapping()
        {
            CreateMap<EditDepartmentCommand, Department>();
        }
    }
}
