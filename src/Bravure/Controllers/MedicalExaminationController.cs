using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Bravure.Entities;
using Bravure.Services;
using Bravure.Models.Examinations;

namespace Bravure.Controllers
{
    [ApiController]
    [Route("api/medical-examination")]
    public class MedicalExaminationController : ControllerBase
    {
        private readonly IMedicalExaminationService _medicalExaminationService;

        public MedicalExaminationController(IMedicalExaminationService medicalExaminationService)
        {
            _medicalExaminationService = medicalExaminationService;
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<MedicalExamination> GetMedicalExamination([FromRoute] Guid id)
        {
            var patient = _medicalExaminationService.GetMedicalExamination(id);
            if (patient == null)
            {
                return NotFound();
            }

            return Ok(patient);
        }

        [HttpGet]
        [Route("all")]
        public ActionResult<List<MedicalExamination>> GetAllMedicalExaminations()
        {
            var result = _medicalExaminationService.GetAllMedicalExaminations();
            return Ok(result);
        }

        [HttpPost]
        public ActionResult<Patient> CreateMedicalExamination(MedicalExaminationDto dto)
        {
            _medicalExaminationService.CreateMedicalExamination(dto);
            return CreatedAtAction(nameof(GetMedicalExamination), new { id = dto.Id }, dto);
        }

        [HttpPut]
        [Route("update/{id}")]
        public IActionResult UpdateMedicalExamination([FromRoute] Guid id, [FromBody]MedicalExaminationDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            _medicalExaminationService.UpdateMedicalExamination(dto);
            return NoContent();
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult DeletePatient([FromRoute] Guid id)
        {
            _medicalExaminationService.DeleteMedicalExamination(id);
            return NoContent();
        }
    }
}
