using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Bravure.Entities;
using Bravure.Services;

namespace Bravure.Controllerp
{
    [ApiController]
    [Route("api/department")]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<Department> GetDepartment([FromRoute] Guid id)
        {
            var department = _departmentService.GetDepartment(id);
            if (department == null)
            {
                return NotFound();
            }

            return Ok(department);
        }

        [HttpGet]
        [Route("all")]
        public ActionResult<List<Department>> GetAllDepartments()
        {
            var departments = _departmentService.GetAllDepartments();
            return Ok(departments);
        }

        [HttpPost]
        public ActionResult<Department> CreateDepartment(Department department)
        {
            _departmentService.CreateDepartment(department);
            return CreatedAtAction(nameof(GetDepartment), new { id = department.Id }, department);
        }

        [HttpPut]
        [Route("update/{id}")]
        public IActionResult UpdateDepartment([FromRoute] Guid id, [FromBody] Department department)
        {
            if (id != department.Id)
            {
                return BadRequest();
            }

            _departmentService.UpdateDepartment(department);
            return NoContent();
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult DeleteDepartment([FromRoute] Guid id)
        {
            _departmentService.DeleteDepartment(id);
            return NoContent();
        }
    }
}
