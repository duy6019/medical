using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Bravure.Entities;
using Bravure.Services;

namespace Bravure.Controllers
{
    [ApiController]
    [Route("api/MedicalAssistance")]
    public class MedicalAssistanceController : ControllerBase
    {
        private readonly IMedicalAssistanceService _MedicalAssistanceService;

        public MedicalAssistanceController(IMedicalAssistanceService MedicalAssistanceService)
        {
            _MedicalAssistanceService = MedicalAssistanceService;
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<MedicalAssistance> GetMedicalAssistance([FromRoute] Guid id)
        {
            var MedicalAssistance = _MedicalAssistanceService.GetMedicalAssistance(id);
            if (MedicalAssistance == null)
            {
                return NotFound();
            }

            return Ok(MedicalAssistance);
        }

        [HttpGet]
        [Route("patiens/all")]
        public ActionResult<List<MedicalAssistance>> GetAllMedicalAssistances()
        {
            var MedicalAssistances = _MedicalAssistanceService.GetAllMedicalAssistances();
            return Ok(MedicalAssistances);
        }

        [HttpPost]
        public ActionResult<MedicalAssistance> CreateMedicalAssistance(MedicalAssistance MedicalAssistance)
        {
            _MedicalAssistanceService.CreateMedicalAssistance(MedicalAssistance);
            return CreatedAtAction(nameof(GetMedicalAssistance), new { id = MedicalAssistance.Id }, MedicalAssistance);
        }

        [HttpPut]
        [Route("update/{id}")]
        public IActionResult UpdateMedicalAssistance([FromRoute] Guid id, [FromBody] MedicalAssistance MedicalAssistance)
        {
            if (id != MedicalAssistance.Id)
            {
                return BadRequest();
            }

            _MedicalAssistanceService.UpdateMedicalAssistance(MedicalAssistance);
            return NoContent();
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult DeleteMedicalAssistance([FromRoute] Guid id)
        {
            _MedicalAssistanceService.DeleteMedicalAssistance(id);
            return NoContent();
        }
    }
}
