using admission_validation.Models;

namespace admission_validation.Services
{
    public class DocumentValidationService
    {
        public List<string> Validate(DocumentUploadRequest request)
        {
            var errors = new List<string>();

            // Sempre obrigatórios
            ValidateRequired(request.AdmissionForm, "Ficha admissional", errors);
            ValidateRequired(request.RGFront, "RG (frente)", errors);
            ValidateRequired(request.RGBack, "RG (verso)", errors);
            ValidateRequired(request.CPF, "CPF", errors);

            // Condicionais
            if (request.IsMale)
                ValidateRequired(request.MilitaryCertificate, "Certificado de reservista", errors);

            if (request.HasChildren)
            {
                ValidateRequired(request.ChildrenBirthCertificate, "Certidão filhos", errors);
                ValidateRequired(request.ChildrenCPF, "CPF filhos", errors);
            }

            if (request.HasRetirementOrPension)
                ValidateRequired(request.RetirementDeclaration, "Declaração aposentadoria/pensão", errors);

            if (request.WillAccumulatePublicJobs)
                ValidateRequired(request.WorkScheduleDocument, "Documento de horários", errors);

            if (request.HasPublicJobBefore)
                ValidateRequired(request.AdministrativeProcessDeclaration, "Declaração processo administrativo", errors);

            return errors;
        }

        private void ValidateRequired(IFormFile file, string name, List<string> errors)
        {
            if (file == null)
                errors.Add($"{name} é obrigatório");
        }
    }
}