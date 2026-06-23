using admission_validation.Models;

namespace admission_validation.Services
{
    public class DocumentValidationService
    {
        private readonly FileStorageService _storageService;
        private readonly OcrService _ocrService;

        public DocumentValidationService(FileStorageService storageService, OcrService ocrService)
        {
            _storageService = storageService;
            _ocrService = ocrService;
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

            
            if (request.RGFront != null)
            {
                var path = SaveTempFile(request.RGFront); // ou reutiliza seu storage

                var ocr = new OcrService();
                var text = ocr.ExtractText(path);

                if (!IsValidRG(text))
                {
                    errors.Add("Documento enviado não parece ser um RG");
                }
            }


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

        public bool IsValidRG(string text)
        {
            var normalized = Normalize(text);

            return normalized.Contains("RG") ||
                normalized.Contains("REGISTRO") ||
                normalized.Contains("GERAL");
        }

        private string Normalize(string text)
        {
            return text
                .ToUpper()
                .Replace(" ", "")
                .Replace("\n", "")
                .Replace("\r", "");
        }
    }
}