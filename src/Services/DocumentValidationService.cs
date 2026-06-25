using admission_validation.Models;

namespace admission_validation.Services
{
    public class DocumentValidationService
    {
        private readonly FileStorageService _storageService;
        private readonly OcrService _ocrService;
        private readonly RgValidator _rgValidator;
        private readonly CpfValidator _cpfValidator;

        public DocumentValidationService(FileStorageService storageService, OcrService ocrService, RgValidator rgValidator, CpfValidator cpfValidator)
        {
            _storageService = storageService;
            _ocrService = ocrService;
            _rgValidator = rgValidator;
            _cpfValidator = cpfValidator;
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

            return results;
        }

        //=============================================== RG ===============================================

        private DocumentValidationDetail ValidateRG(IFormFile file, string candidateName)
        {
            if (file == null)
            {
                return new DocumentValidationDetail
                {
                    DocumentName = "RG",
                    Score = 0,
                    Status = "Não enviado",
                    Message = "RG não foi enviado"
                };
            }

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
            if (file == null)
            {
                return new DocumentValidationDetail
                {
                    DocumentName = "CPF",
                    Score = 0,
                    Status = "Não enviado",
                    Message = "CPF não foi enviado"
                };
            }

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