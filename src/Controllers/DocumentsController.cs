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

            var results = _validationService.Validate(request);

            
            var status = GetFinalStatus(results);

            var folderPath = _storageService.SaveFiles(request, status);

            _storageService.GenerateReport(
                folderPath,
                request.CandidateName,
                results
            );

            return Ok(new
            {
                status = status,
                documents = results
            });
        }

        private string GetFinalStatus(List<DocumentValidationDetail> results)
        {
            if (results.Any(r => r.Score < 40))
                return "Rejected";

            if (results.Any(r => r.Score < 70))
                return "Review";

            return "Approved";
        }
    }
}