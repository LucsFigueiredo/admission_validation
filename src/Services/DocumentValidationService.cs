using admission_validation.Models;

namespace admission_validation.Services
{
    public class DocumentValidationService
    {
        private readonly RgValidator _rgValidator;
        private readonly CpfValidator _cpfValidator;
        private readonly AdressValidator _adressValidator;
        private readonly PisValidator _pisValidator;
        private readonly VoterValidator _voterValidator;
        private readonly ExtratoValidator _extratoValidator;
        private readonly NascimentoCasamentoValidator _nascimentoValidator;
        private readonly AntecedentesValidator _antecedentesValidator;
        private readonly DiplomaValidator _diplomaValidator;
        private readonly HistoricoValidator _historicoValidator;
        private readonly BensValidator _bensValidator;
        private readonly ProventosValidator _proventosValidator;
        private readonly AcumuloValidator _acumuloValidator;

        public DocumentValidationService(RgValidator rgValidator, CpfValidator cpfValidator, AdressValidator adressValidator,
         PisValidator pisValidator, VoterValidator voterValidator, ExtratoValidator extratoValidator, NascimentoCasamentoValidator nascimentoCasamentoValidator, AntecedentesValidator antecedentesValidator, 
         DiplomaValidator diplomaValidator, HistoricoValidator historicoValidator, BensValidator bensValidator, ProventosValidator proventosValidator, AcumuloValidator acumuloValidator)
        {
            _rgValidator = rgValidator;
            _cpfValidator = cpfValidator;
            _adressValidator = adressValidator;
            _pisValidator = pisValidator;
            _voterValidator = voterValidator;
            _extratoValidator = extratoValidator;
            _nascimentoValidator = nascimentoCasamentoValidator;
            _antecedentesValidator = antecedentesValidator;
            _diplomaValidator = diplomaValidator;
            _historicoValidator = historicoValidator;
            _bensValidator = bensValidator;
            _proventosValidator = proventosValidator;
            _acumuloValidator = acumuloValidator;
        }

        public List<DocumentValidationDetail> Validate(DocumentUploadRequest request)
        {
            var results = new List<DocumentValidationDetail>();

            if (request.RGFile != null)
            {
                results.Add(_rgValidator.ValidateRG(request.RGFile, request.CandidateName));
            }
            else
            {
                results.Add(new DocumentValidationDetail
                {
                    DocumentName = "RG",
                    Score = 0,
                    Status = "Não enviado",
                    Message = "RG não foi enviado"
                });
            }
            
            if (request.CPFFile != null)
            {
                results.Add(_cpfValidator.ValidateCPF(request.CPFFile, request.CandidateName));
            }
            else
            {
                results.Add(new DocumentValidationDetail
                {
                    DocumentName = "CPF",
                    Score = 0,
                    Status = "Não enviado",
                    Message = "CPF não foi enviado"
                });
            }

            if (request.MilitaryCertificateFile == null && request.IsMale == true)
            {
                results.Add(new DocumentValidationDetail
                {
                    DocumentName = "Comprovante de Reservista",
                    Score = 0,
                    Status = "Não enviado",
                    Message = "Comprovante de Reservista não foi enviado"
                });
            }

            if (request.AdressProofFile != null)
            {
                results.Add(_adressValidator.ValidateAdressProof(request.AdressProofFile, request.CandidateName));
            }
            else
            {
                results.Add(new DocumentValidationDetail
                {
                    DocumentName = "Comprovante de Endereço",
                    Score = 0,
                    Status = "Não enviado",
                    Message = "Comprovante de Endereço não foi enviado"
                });
            }

            if (request.PisFile != null)
            {
                results.Add(_pisValidator.ValidatePis(request.PisFile, request.CandidateName));
            }
            else
            {
                results.Add(new DocumentValidationDetail
                {
                    DocumentName = "PIS",
                    Score = 0,
                    Status = "Não enviado",
                    Message = "PIS não foi enviado"
                });
            }

            if (request.VoterCardFile != null)
            {
                results.Add(_voterValidator.ValidateVoterCard(request.VoterCardFile, request.CandidateName));
            }
            else
            {
                results.Add(new DocumentValidationDetail
                {
                    DocumentName = "Titulo de Eleitor",
                    Score = 0,
                    Status = "Não enviado",
                    Message = "Titulo de Eleitor não foi enviado"
                });
            }

            if (request.ExtratoFile != null)
            {
                results.Add(_extratoValidator.ValidateExtrato(request.ExtratoFile, request.CandidateName));
            }
            else
            {
                results.Add(new DocumentValidationDetail
                {
                    DocumentName = "Extrato - Santander",
                    Score = 0,
                    Status = "Não enviado",
                    Message = "Extrato do Santander não foi enviado"
                });
            }

            if (request.NascimentoCasamentoFile != null)
            {
                results.Add(_nascimentoValidator.ValidateNascimentoCasamento(request.NascimentoCasamentoFile, request.CandidateName));
            }
            else
            {
                results.Add(new DocumentValidationDetail
                {
                    DocumentName = "Certidão de Nascimento ou Casamento",
                    Score = 0,
                    Status = "Não enviado",
                    Message = "Certidão de Nascimento ou Casamento não foi enviada"
                });
            }

            if (request.AntecedentesFile != null)
            {
                results.Add(_antecedentesValidator.ValidateAntecedentes(request.AntecedentesFile, request.CandidateName));
            }
            else
            {
                results.Add(new DocumentValidationDetail
                {
                    DocumentName = "Antecedentes Criminais",
                    Score = 0,
                    Status = "Não enviado",
                    Message = "Antecedentes Criminais não foi enviado"
                });
            }

            if (request.DiplomaFile != null)
            {
                results.Add(_diplomaValidator.ValidateDiploma(request.DiplomaFile, request.CandidateName));
            }
            else
            {
                results.Add(new DocumentValidationDetail
                {
                    DocumentName = "Diploma ou Certificado Escolar",
                    Score = 0,
                    Status = "Não enviado",
                    Message = "Diploma ou Certificado Escolar não foi enviado"
                });
            }

            if (request.HistoricoFile != null)
            {
                results.Add(_historicoValidator.ValidateHistorico(request.HistoricoFile, request.CandidateName));
            }
            else
            {
                results.Add(new DocumentValidationDetail
                {
                    DocumentName = "Histórico Escolar",
                    Score = 0,
                    Status = "Não enviado",
                    Message = "Histórico Escolar não foi enviado"
                });
            }

            if (request.BensFile != null)
            {
                results.Add(_bensValidator.ValidateBens(request.BensFile, request.CandidateName));
            }
            else
            {
                results.Add(new DocumentValidationDetail
                {
                    DocumentName = "Declaração de Bens",
                    Score = 0,
                    Status = "Não enviado",
                    Message = "Declaração de Bens não foi enviada"
                });
            }

            if (request.ProventosFile != null)
            {
                results.Add(_proventosValidator.ValidateProventos(request.ProventosFile, request.CandidateName));
            }
            else
            {
                results.Add(new DocumentValidationDetail
                {
                    DocumentName = "Declaração de Acúmulo",
                    Score = 0,
                    Status = "Não enviado",
                    Message = "Declaração de Proventos não foi enviada"
                });
            }

            if (request.AcumuloFile != null)
            {
                results.Add(_acumuloValidator.ValidateAcumulo(request.AcumuloFile, request.CandidateName));
            }
            else
            {
                results.Add(new DocumentValidationDetail
                {
                    DocumentName = "Declaração de Acúmulo",
                    Score = 0,
                    Status = "Não enviado",
                    Message = "Declaração de Acúmulo não foi enviada"
                });
            }

            return results;
        }
    }
}