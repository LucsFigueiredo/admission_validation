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
        public string CandidateName { get; set; } = string.Empty;
        public bool IsMale { get; set; }
        public IFormFile? RGFile { get; set; }
        public IFormFile? CPFFile { get; set; }
        public IFormFile? MilitaryCertificateFile { get; set; }
        public IFormFile? AdressProofFile { get; set; }
        public IFormFile? PisFile { get; set; }
        public IFormFile? ExtratoFile { get; set; }
        public IFormFile? NascimentoCasamentoFile { get; set; }
        public IFormFile? AntecedentesFile { get; set; }
        public IFormFile? DiplomaFile { get; set; }
        public IFormFile? HistoricoFile { get; set; }
        public IFormFile? BensFile { get; set; }
        public IFormFile? ProventosFile { get; set; }
        public IFormFile? AcumuloFile { get; set; }
        public IFormFile? VoterCardFile { get; set; }
        public EmployeeType EmployeeType { get; set; }
    }
}