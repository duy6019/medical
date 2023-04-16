using System;
using System.Collections.Generic;
using Bravure.Entities;

namespace Bravure.Services
{
    public interface IDepartmentService
    {
        void CreateDepartment(Department department);
        void DeleteDepartment(Guid id);
        List<Department> GetAllDepartments();
        Department GetDepartment(Guid id);
        void UpdateDepartment(Department department);
    }
}