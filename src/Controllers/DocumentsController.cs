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
        private readonly DocumentValidationService _validationService;
        private readonly FileStorageService _storageService;

        public DocumentsController(DocumentValidationService validationService, FileStorageService storageService)
        {
            _validationService = validationService;
            _storageService = storageService;
        }
        
        [HttpPost("upload")]
        public IActionResult Upload([FromForm] DocumentUploadRequest request)
        {

            var errors = _validationService.Validate(request);

            if (errors.Any())
            {
                _storageService.SaveFiles(request, "Rejected");

                return BadRequest(new
                {
                    message = "Erro na validação",
                    errors
                });
            }
            else 
            {
                _storageService.SaveFiles(request, "Approved");

                return Ok(new
                {
                    message = "Documentos enviados com sucesso"
                });
            }
        }
    }
}