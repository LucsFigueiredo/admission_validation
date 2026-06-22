namespace admission_validation.Models
{
    public class DocumentUploadRequest
    {
        public string CandidateName { get; set; }

        public bool HasChildren { get; set; }
        public bool IsMale { get; set; }
        public bool HasPublicJobBefore { get; set; }
        public bool WillAccumulatePublicJobs { get; set; }
        public bool HasRetirementOrPension { get; set; }

        // Documentos principais
        public IFormFile AdmissionForm { get; set; }
        public IFormFile BirthOrMarriageCertificate { get; set; }
        public IFormFile RGFront { get; set; }
        public IFormFile RGBack { get; set; }
        public IFormFile CPF { get; set; }
        public IFormFile PIS { get; set; }
        public IFormFile VoterRegistration { get; set; }
        public IFormFile CriminalRecord { get; set; }
        public IFormFile AddressProof { get; set; }
        public IFormFile BankStatement { get; set; }
        public IFormFile Diploma { get; set; }
        public IFormFile SchoolHistory { get; set; }
        public IFormFile AssetDeclaration { get; set; }
        public IFormFile IncomeDeclaration { get; set; }
        public IFormFile AccumulationDeclaration { get; set; }

        // Condicionais
        public IFormFile MilitaryCertificate { get; set; }
        public IFormFile ChildrenBirthCertificate { get; set; }
        public IFormFile ChildrenCPF { get; set; }
        public IFormFile ChildrenVaccination { get; set; }
        public IFormFile SchoolDeclaration { get; set; }
        public IFormFile RetirementDeclaration { get; set; }
        public IFormFile WorkScheduleDocument { get; set; }
        public IFormFile AdministrativeProcessDeclaration { get; set; }

        // Opcionais
        public IFormFile WorkCard { get; set; }
        public IFormFile CNH { get; set; }
    }
}