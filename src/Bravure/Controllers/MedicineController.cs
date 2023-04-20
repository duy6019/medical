using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Bravure.Entities;
using Bravure.Services;

namespace Bravure.Controllers
{
    [ApiController]
    [Route("api/medicine")]
    public class MedicineController : ControllerBase
    {
        private readonly IMedicineService _medicineService;

        public MedicineController(IMedicineService medicineService)
        {
            _medicineService = medicineService;
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<Medicine> GetMedicine([FromRoute] Guid id)
        {
            var medicine = _medicineService.GetMedicine(id);
            if (medicine == null)
            {
                return NotFound();
            }

            return Ok(medicine);
        }

        [HttpGet]
        [Route("all")]
        public ActionResult<List<Medicine>> GetAllMedicines()
        {
            var medicines = _medicineService.GetAllMedicines();
            return Ok(medicines);
        }

        [HttpPost]
        public ActionResult<Medicine> CreateMedicine(Medicine medicine)
        {
            _medicineService.CreateMedicine(medicine);
            return CreatedAtAction(nameof(GetMedicine), new { id = medicine.Id }, medicine);
        }

        [HttpPut]
        [Route("update/{id}")]
        public IActionResult UpdateMedicine([FromRoute] Guid id, [FromBody] Medicine medicine)
        {
            if (id != medicine.Id)
            {
                return BadRequest();
            }

            _medicineService.UpdateMedicine(medicine);
            return NoContent();
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult DeleteMedicine([FromRoute] Guid id)
        {
            _medicineService.DeleteMedicine(id);
            return NoContent();
        }
    }
}
