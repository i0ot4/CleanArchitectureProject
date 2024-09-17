﻿using CleanProject.Core.Features.Departments.Queries.Results;
using CleanProject.Data.Entities;

namespace CleanProject.Core.Mapping.Departments
{
    public partial class DepartmentProfile
    {
        public void GetDepartmentPaginatedMapping()
        {
            CreateMap<Department, GetDepartmentPaginatedListResponse>();
        }
    }
}