using System;
using System.Collections.Generic;
using System.Linq;
using Bravure.Entities;
using Bravure.Exceptions;

namespace Bravure.Services;

public class DepartmentService : IDepartmentService
{
    private readonly BravureDbContext _context;

    public DepartmentService(BravureDbContext context)
    {
        _context = context;
    }

    public void CreateDepartment(Department department)
    {
        if (_context.Departments.Any(x => x.DisplayId == department.DisplayId))
        {
            throw new BadRequestException("Mã phòng ban tồn tại");
        }
        _context.Departments.Add(department);
        _context.SaveChanges();
    }

    public Department GetDepartment(Guid id)
    {
        return _context.Departments.Find(id);
    }

    public List<Department> GetAllDepartments()
    {
        return _context.Departments.ToList();
    }

    public void UpdateDepartment(Department department)
    {
        if (_context.Departments.Any(x => x.DisplayId == department.DisplayId && x.Id != department.Id))
        {
            throw new BadRequestException("Mã phòng ban tồn tại");
        }
        _context.Departments.Update(department);
        _context.SaveChanges();
    }

    public void DeleteDepartment(Guid id)
    {
        var department = _context.Departments.Find(id);
        if (department != null)
        {
            _context.Departments.Remove(department);
            _context.SaveChanges();
        }
    }
}
