using Microsoft.AspNetCore.Mvc;
using PdfGenerator.Models;
using Wkhtmltopdf.NetCore;

namespace PdfGenerator.Controllers
{
    [ApiController]
    public class PdfGeneratorController : ControllerBase
    {
        private readonly ILogger<PdfGeneratorController> _logger;
        private readonly IGeneratePdf _generatePdf;

        public PdfGeneratorController(IGeneratePdf generatePdf,
            ILogger<PdfGeneratorController> logger)
        {
            _generatePdf = generatePdf;
            _logger = logger;
        }

        [HttpPost]
        [Route("/pdfgenerator/compliance")]
        public async Task<IActionResult> GenerateComplianceForm(ComplianceFormModel model)
        {
          _logger.LogInformation("Generating PDF");

            var pdf = await _generatePdf.GetByteArray("PdfViews/ComplianceForm.cshtml", model);
            var pdfStream = new System.IO.MemoryStream();
            pdfStream.Write(pdf, 0, pdf.Length);
            pdfStream.Position = 0;
            return new FileStreamResult(pdfStream, "application/pdf");
        }
    }
}
