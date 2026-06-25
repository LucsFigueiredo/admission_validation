using admission_validation.Models;

namespace admission_validation.Services
{
    public class DocumentValidationService
    {
        private readonly FileStorageService _storageService;
        private readonly OcrService _ocrService;
        private readonly RgValidator _rgValidator;
        private readonly CpfValidator _cpfValidator;
        private readonly AdressValidator _adressValidator;
        private readonly PisValidator _pisValidator;
        private readonly VoterValidator _voterValidator;

        public DocumentValidationService(FileStorageService storageService, OcrService ocrService, RgValidator rgValidator, CpfValidator cpfValidator, AdressValidator adressValidator, PisValidator pisValidator, VoterValidator voterValidator)
        {
            _storageService = storageService;
            _ocrService = ocrService;
            _rgValidator = rgValidator;
            _cpfValidator = cpfValidator;
            _adressValidator = adressValidator;
            _pisValidator = pisValidator;
            _voterValidator = voterValidator;
        }

        private string SaveTempFile(IFormFile file)
        {
            return _storageService.SaveTemp(file);
        }

        public class NameMatchResult
        {
            public int Matches { get; set; }
            public int TotalParts { get; set; }
            public string Message { get; set; } = "";
            public bool IsMatch { get; set; }
        }

        public List<DocumentValidationDetail> Validate(DocumentUploadRequest request)
        {
            var results = new List<DocumentValidationDetail>();

            if (request.RG != null)
            {
                results.Add(ValidateRG(request.RG, request.CandidateName));
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
            
            if (request.CPF != null)
            {
                results.Add(ValidateCPF(request.CPF, request.CandidateName));
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

            if (request.MilitaryCertificate == null && request.IsMale == true)
            {
                results.Add(new DocumentValidationDetail
                {
                    DocumentName = "Comprovante de Reservista",
                    Score = 0,
                    Status = "Não enviado",
                    Message = "Comprovante de Reservista não foi enviado"
                });
            }

            if (request.AdressProof != null)
            {
                results.Add(ValidateAdressProof(request.AdressProof, request.CandidateName));
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

            if (request.Pis != null)
            {
                results.Add(ValidatePis(request.Pis, request.CandidateName));
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

            if (request.VoterCard != null)
            {
                results.Add(ValidateVoterCard(request.VoterCard, request.CandidateName));
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

            return results;
        }

        //=============================================== RG ===============================================

        private DocumentValidationDetail ValidateRG(IFormFile file, string candidateName)
        {
            var extension = Path.GetExtension(file.FileName).ToLower();

            if (extension != ".png" && extension != ".jpg" && extension != ".jpeg")
            {
                return new DocumentValidationDetail
                {
                    DocumentName = "RG",
                    Score = 0,
                    Status = "Formato inválido",
                    Message = "RG deve ser enviado como imagem"
                };
            }

            var path = SaveTempFile(file);

            try
            {
                var text = _ocrService.ExtractText(path);
                var score = _rgValidator.CalculateScore(text);

                var nameResult = CheckNameMatch(text, candidateName);

                return new DocumentValidationDetail
                {
                    DocumentName = "RG",
                    Score = score,
                    Status = score >= 70 ? "OK" : "Inconsistente",
                    Message = score >= 70
                        ? $"RG válido e {nameResult.Message}"
                        : $"RG pode estar incorreto e {nameResult.Message}"
                };
            }
            finally
            {
                var tempDir = Path.GetDirectoryName(path);

                File.Delete(path);

                if (Directory.Exists(tempDir) && !Directory.EnumerateFileSystemEntries(tempDir).Any())
                {
                    Directory.Delete(tempDir);
                }
            }
        }

        //=============================================== CPF ===============================================

        private DocumentValidationDetail ValidateCPF(IFormFile file, string candidateName)
        {
            var extension = Path.GetExtension(file.FileName).ToLower();

            if (extension != ".png" && extension != ".jpg" && extension != ".jpeg")
            {
                return new DocumentValidationDetail
                {
                    DocumentName = "CPF",
                    Score = 0,
                    Status = "Formato inválido",
                    Message = "CPF deve ser enviado como imagem"
                };
            }

            var path = SaveTempFile(file);

            try
            {
                var text = _ocrService.ExtractText(path);
                var score = _cpfValidator.CalculateScore(text);

                var nameResult = CheckNameMatch(text, candidateName);

                return new DocumentValidationDetail
                {
                    DocumentName = "CPF",
                    Score = score,
                    Status = score >= 70 ? "OK" : "Inconsistente",
                    Message = score >= 70
                        ? $"CPF válido e {nameResult.Message}"
                        : $"CPF pode estar incorreto e {nameResult.Message}"
                };
            }
            finally
            {
                File.Delete(path);
            }
        }

        //=============================================== Comprovante de Endereço ===============================================

        private DocumentValidationDetail ValidateAdressProof(IFormFile file, string candidateName)
        {
            var extension = Path.GetExtension(file.FileName).ToLower();

            if (extension != ".png" && extension != ".jpg" && extension != ".jpeg")
            {
                return new DocumentValidationDetail
                {
                    DocumentName = "Comprovante de Endereço",
                    Score = 0,
                    Status = "Formato inválido",
                    Message = "Comprovante de Endereço deve ser enviado como imagem"
                };
            }

            var path = SaveTempFile(file);

            try
            {
                var text = _ocrService.ExtractText(path);
                var score = _adressValidator.CalculateScore(text);

                var nameResult = CheckNameMatch(text, candidateName);

                return new DocumentValidationDetail
                {
                    DocumentName = "Comprovante de Endereço",
                    Score = score,
                    Status = score >= 60 ? "OK" : "Inconsistente",
                    Message = score >= 60
                        ? $"Comprovante de Endereço válido e {nameResult.Message}"
                        : $"Comprovante de Endereço pode estar incorreto e {nameResult.Message}"
                };
            }
            finally
            {
                File.Delete(path);
            }
        }

        //=============================================== PIS ===============================================

        private DocumentValidationDetail ValidatePis(IFormFile file, string candidateName)
        {
            var extension = Path.GetExtension(file.FileName).ToLower();

            if (extension != ".png" && extension != ".jpg" && extension != ".jpeg")
            {
                return new DocumentValidationDetail
                {
                    DocumentName = "PIS",
                    Score = 0,
                    Status = "Formato inválido",
                    Message = "PIS deve ser enviado como imagem"
                };
            }

            var path = SaveTempFile(file);

            try
            {
                var text = _ocrService.ExtractText(path);
                var score = _pisValidator.CalculateScore(text);

                var nameResult = CheckNameMatch(text, candidateName);

                return new DocumentValidationDetail
                {
                    DocumentName = "PIS",
                    Score = score,
                    Status = score >= 60 ? "OK" : "Inconsistente",
                    Message = score >= 60
                        ? $"PIS válido e {nameResult.Message}"
                        : $"PIS pode estar incorreto e {nameResult.Message}"
                };
            }
            finally
            {
                File.Delete(path);
            }
        }

        //=============================================== Titulo de Eleitor ===============================================

        private DocumentValidationDetail ValidateVoterCard(IFormFile file, string candidateName)
        {
            var extension = Path.GetExtension(file.FileName).ToLower();

            if (extension != ".png" && extension != ".jpg" && extension != ".jpeg")
            {
                return new DocumentValidationDetail
                {
                    DocumentName = "Titulo de Eleitor",
                    Score = 0,
                    Status = "Formato inválido",
                    Message = "Titulo de Eleitor deve ser enviado como imagem"
                };
            }

            var path = SaveTempFile(file);

            try
            {
                var text = _ocrService.ExtractText(path);
                var score = _voterValidator.CalculateScore(text);

                var nameResult = CheckNameMatch(text, candidateName);

                return new DocumentValidationDetail
                {
                    DocumentName = "Titulo de Eleitor",
                    Score = score,
                    Status = score >= 60 ? "OK" : "Inconsistente",
                    Message = score >= 60
                        ? $"Titulo de Eleitor válido e {nameResult.Message}"
                        : $"Titulo de Eleitor pode estar incorreto e {nameResult.Message}"
                };
            }
            finally
            {
                File.Delete(path);
            }
        }

        private NameMatchResult CheckNameMatch(string text, string candidateName)
        {
            var normalizedText = text.ToUpper();
            var nameParts = candidateName.ToUpper().Split(" ");

            int matches = nameParts.Count(part => normalizedText.Contains(part));

            var result = new NameMatchResult
            {
                Matches = matches,
                TotalParts = nameParts.Length,
                IsMatch = matches >= nameParts.Length / 2
            };

            if (result.IsMatch)
            {
                result.Message = "Nome parcialmente compatível com o candidato.";
            }
            else
            {
                result.Message = "Nome não corresponde ao candidato.";
            }

            return result;
        }

        public List<DocumentValidationDetail> ValidateDocuments(DocumentUploadRequest request)
        {
            var results = new List<DocumentValidationDetail>();

            if (request.RG != null)
                results.Add(ValidateRG(request.RG, request.CandidateName));

            if (request.CPF != null)
                results.Add(ValidateCPF(request.CPF, request.CandidateName));

            return results;
        }
    }
}