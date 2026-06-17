using admission_validation.Data;
using admission_validation.Models;

namespace admission_validation.Services
{
    public class ValidationService
    {
        public ValidationResult ValidateDocument(List<string> uploadedDocuments)
        {
            var requiredDocuments = Documents.All
                .Where(d => d.IsRequired);

            foreach (var document in requiredDocuments)
            {
                if (!uploadedDocuments.Contains(document.Name))
                {
                    return new ValidationResult
                    {
                        IsValid = false,
                        Message = $"Documento obrigatório não enviado: {document.Name}"
                    };
                }
            }

            return new ValidationResult
            {
                IsValid = true,
                Message = "Todos os documentos obrigatórios foram enviados."
            };
        }
    }
}