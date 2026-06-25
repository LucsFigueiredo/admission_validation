namespace admission_validation.Models
{
    public enum EmployeeType
    {
        Efetivo,
        Comissionado,
        Temporario
    }

    public class DocumentUploadRequest
    {
        public string CandidateName { get; set; }
        public bool IsMale { get; set; }
        public IFormFile? RG { get; set; }
        public IFormFile? CPF { get; set; }
        public IFormFile? MilitaryCertificate { get; set; }
        public EmployeeType EmployeeType { get; set; }
    }
}