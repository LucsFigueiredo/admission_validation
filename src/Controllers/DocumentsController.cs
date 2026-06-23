using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using admission_validation.Services;
using admission_validation.Models;

namespace admission_validation.Controllers
{
    [ApiController]
    [Route("api/documents")]
    public class DocumentsController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public DocumentsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        [HttpPost("upload")]
        public IActionResult Upload([FromForm] DocumentUploadRequest request)
        {
            var validationService = new DocumentValidationService();
            var storageService = new FileStorageService(_configuration);

            var errors = validationService.Validate(request);

            if (errors.Any())
            {
                storageService.SaveFiles(request, "Rejected");

                return BadRequest(new
                {
                    message = "Erro na validação",
                    errors
                });
            }

            storageService.SaveFiles(request, "Approved");

            return Ok(new
            {
                message = "Documentos enviados com sucesso"
            });
        }
    }
}