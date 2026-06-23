using admission_validation.Models;

namespace admission_validation.Services
{
    public class DocumentValidationService
    {
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

            return errors;
        }

        private void ValidateRequired(IFormFile file, string name, List<string> errors)
        {
            if (file == null)
                errors.Add($"{name} é obrigatório");
        }
    }
}