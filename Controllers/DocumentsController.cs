using Microsoft.AspNetCore.Mvc;
using admission_validation.Services;
using admission_validation.Models;

namespace admission_validation.Controllers
{
    [ApiController]
    [Route("api/documents")]
    public class DocumentsController : ControllerBase
    {
        [HttpPost]
        public IActionResult Upload(DocumentsRequest documentsRequest)
        {
            var services = new ValidationService();

            var files = documentsRequest.Documents.Select(d => d.FileName).ToList();

            var result = services.ValidateDocument(files);

            if (!result.IsValid)
            {
                return BadRequest(new { message = "Faltam documentos obrigatórios." });
            }
            return Ok(new { message = "Documentos enviados com sucesso." });
        }
    }
}