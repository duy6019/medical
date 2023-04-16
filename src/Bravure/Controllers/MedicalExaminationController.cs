using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Bravure.Entities;
using Bravure.Services;

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
        public ActionResult<Patient> CreateMedicalExamination(MedicalExamination medicalExamination)
        {
            _medicalExaminationService.CreateMedicalExamination(medicalExamination);
            return CreatedAtAction(nameof(GetMedicalExamination), new { id = medicalExamination.Id }, medicalExamination);
        }

        [HttpPut]
        [Route("update/{id}")]
        public IActionResult UpdateMedicalExamination([FromRoute] Guid id, [FromBody] MedicalExamination medicalExamination)
        {
            if (id != medicalExamination.Id)
            {
                return BadRequest();
            }

            _medicalExaminationService.UpdateMedicalExamination(medicalExamination);
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
