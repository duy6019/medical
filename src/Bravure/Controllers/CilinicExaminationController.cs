using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Bravure.Entities;
using Bravure.Services;

namespace Bravure.Controllers
{
    [ApiController]
    [Route("api/cilinic-examination")]
    public class CilinicExaminationController : ControllerBase
    {
        private readonly ICilinicExaminationService _cilinicExaminationService;

        public CilinicExaminationController(ICilinicExaminationService cilinicExaminationService)
        {
            _cilinicExaminationService = cilinicExaminationService;
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<CilinicExamination> GetCilinicExamination([FromRoute] Guid id)
        {
            var patient = _cilinicExaminationService.GetCilinicExamination(id);
            if (patient == null)
            {
                return NotFound();
            }

            return Ok(patient);
        }

        [HttpGet]
        [Route("all")]
        public ActionResult<List<CilinicExamination>> GetAllCilinicExaminations()
        {
            var result = _cilinicExaminationService.GetAllCilinicExaminations();
            return Ok(result);
        }

        [HttpPost]
        public ActionResult<Patient> CreateCilinicExamination(CilinicExamination cilinicExamination)
        {
            _cilinicExaminationService.CreateCilinicExamination(cilinicExamination);
            return CreatedAtAction(nameof(GetCilinicExamination), new { id = cilinicExamination.Id }, cilinicExamination);
        }

        [HttpPut]
        [Route("update/{id}")]
        public IActionResult UpdateCilinicExamination([FromRoute] Guid id, [FromBody] CilinicExamination cilinicExamination)
        {
            if (id != cilinicExamination.Id)
            {
                return BadRequest();
            }

            _cilinicExaminationService.UpdateCilinicExamination(cilinicExamination);
            return NoContent();
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult DeletePatient([FromRoute] Guid id)
        {
            _cilinicExaminationService.DeleteCilinicExamination(id);
            return NoContent();
        }
    }
}
