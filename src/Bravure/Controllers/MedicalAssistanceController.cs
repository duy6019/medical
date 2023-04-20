using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Bravure.Entities;
using Bravure.Services;
using Bravure.Models.MedicalAssistances;

namespace Bravure.Controllers
{
    [ApiController]
    [Route("api/medical-assistance")]
    public class MedicalAssistanceController : ControllerBase
    {
        private readonly IMedicalAssistanceService _medicalAssistanceService;

        public MedicalAssistanceController(IMedicalAssistanceService medicalAssistanceService)
        {
            _medicalAssistanceService = medicalAssistanceService;
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<MedicalAssistance> GetMedicalAssistance([FromRoute] Guid id)
        {
            var MedicalAssistance = _medicalAssistanceService.GetMedicalAssistance(id);
            if (MedicalAssistance == null)
            {
                return NotFound();
            }

            return Ok(MedicalAssistance);
        }

        [HttpGet]
        [Route("all")]
        public ActionResult<List<MedicalAssistance>> GetAllMedicalAssistances()
        {
            var MedicalAssistances = _medicalAssistanceService.GetAllMedicalAssistances();
            return Ok(MedicalAssistances);
        }

        [HttpPost]
        public ActionResult<MedicalAssistance> CreateMedicalAssistance(MedicalAssistanceDto dto)
        {
            _medicalAssistanceService.CreateMedicalAssistance(dto);
            return CreatedAtAction(nameof(GetMedicalAssistance), new { id = dto.Id }, dto);
        }

        [HttpPut]
        [Route("update/{id}")]
        public IActionResult UpdateMedicalAssistance([FromRoute] Guid id, [FromBody] MedicalAssistanceDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            _medicalAssistanceService.UpdateMedicalAssistance(dto);
            return NoContent();
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult DeleteMedicalAssistance([FromRoute] Guid id)
        {
            _medicalAssistanceService.DeleteMedicalAssistance(id);
            return NoContent();
        }
    }
}
