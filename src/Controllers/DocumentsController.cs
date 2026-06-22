using Microsoft.AspNetCore.Mvc;
using admission_validation.Services;
using admission_validation.Models;

namespace admission_validation.Controllers
{
    [ApiController]
    [Route("api/documents")]
    public class DocumentsController : ControllerBase
    {
        [HttpPost("upload")]
        public IActionResult Upload([FromForm] DocumentUploadRequest request)
        {
            var validationService = new DocumentValidationService();

            var errors = validationService.Validate(request);

            if (errors.Any())
            {
                return BadRequest(new 
                { 
                    message = "Documentos obrigatórios não foram enviados",
                    errors 
                });
            }

            return Ok(new 
            { 
                message = "Documentos enviados com sucesso" 
            });
        }
    }
}