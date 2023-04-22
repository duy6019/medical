using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Bravure.Entities;
using Bravure.Services;
using Bravure.Models.Patients;

namespace Bravure.Controllers
{
    [ApiController]
    [Route("api/patient")]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<Patient> GetPatient([FromRoute] Guid id)
        {
            var patient = _patientService.GetPatient(id);
            if (patient == null)
            {
                return NotFound();
            }

            return Ok(patient);
        }

        [HttpGet]
        [Route("all")]
        public ActionResult<List<Patient>> GetAllPatients()
        {
            var result = _patientService.GetAllPatients();
            return Ok(new { items = result });
        }

        [HttpPost]
        public ActionResult<Patient> CreatePatient(PatientDto patient)
        {
            _patientService.CreatePatient(patient);
            return CreatedAtAction(nameof(GetPatient), new { id = patient.Id }, patient);
        }

        [HttpPut]
        [Route("update/{id}")]
        public IActionResult UpdatePatient([FromRoute] Guid id, [FromBody] PatientDto patient)
        {
            if (id != patient.Id)
            {
                return BadRequest();
            }

            _patientService.UpdatePatient(patient);
            return NoContent();
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult DeletePatient([FromRoute] Guid id)
        {
            _patientService.DeletePatient(id);
            return NoContent();
        }
    }
}
