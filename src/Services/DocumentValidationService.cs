using admission_validation.Models;

namespace admission_validation.Services
{
    public class DocumentValidationService
    {
        private readonly FileStorageService _storageService;
        private readonly OcrService _ocrService;
        private readonly RgValidator _rgValidator;

        public DocumentValidationService(FileStorageService storageService, OcrService ocrService, RgValidator rgValidator)
        {
            _storageService = storageService;
            _ocrService = ocrService;
            _rgValidator = rgValidator;
        }

        private string SaveTempFile(IFormFile file)
        {
            return _storageService.SaveTemp(file);
        }

        public List<string> Validate(DocumentUploadRequest request)
        {
            var errors = new List<string>();

            // Sempre obrigatórios
            ValidateRequired(request.RGFront, "RG (frente)", errors);
            ValidateRequired(request.RGBack, "RG (verso)", errors);
            ValidateRequired(request.CPF, "CPF", errors);
        
            // Condicionais
            if (request.IsMale == true)
                ValidateRequired(request.MilitaryCertificate, "Certificado de reservista", errors);

            
            ValidateRG(request.RGFront);


            return errors;
        }

        private void ValidateRequired(IFormFile file, string name, List<string> errors)
        {
            if (file == null)
            {
                errors.Add($"{name} é obrigatório");
            }              
            else if (file.FileName.EndsWith(".pdf"))
            {
                errors.Add("PDF ainda não suportado para OCR");
            }

        }

        private void ValidateRG(IFormFile file)
        {
            if (file != null)
            {
                var path = SaveTempFile(file);

                try
                {
                    var ocr = new OcrService();
                    var text = ocr.ExtractText(path);

                    if (!_rgValidator.Validate(text))
                    {                  
                        Console.WriteLine("OCR RESULT:");
                        Console.WriteLine(text);
                    }
                }
                finally
                {
                    File.Delete(path);
                }
            }
        }
    }
}