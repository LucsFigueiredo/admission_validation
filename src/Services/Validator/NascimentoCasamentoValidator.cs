using System.Text.RegularExpressions;
using admission_validation.Models;
using admission_validation.Services;

public class NascimentoCasamentoValidator
{
    private readonly FileStorageService _storageService;
    private readonly OcrService _ocrService;
    private readonly TextHelper _helperService;

    public NascimentoCasamentoValidator(OcrService ocrService, FileStorageService storageService, TextHelper helperService)
    {
        _ocrService = ocrService;
        _storageService = storageService;
        _helperService = helperService;
    }

    private string SaveTempFile(IFormFile file)
    {
        return _storageService.SaveTemp(file);
    }

    public DocumentValidationDetail ValidateNascimentoCasamento(IFormFile file, string candidateName)
    {
        var extension = Path.GetExtension(file.FileName).ToLower();

        if (extension != ".png" && extension != ".jpg" && extension != ".jpeg")
        {
            return new DocumentValidationDetail
            {       
                DocumentName = "Certidão de Nascimento ou Casamento",
                Score = 0,
                Status = "Formato inválido",
                Message = "Certidão de Nascimento ou Casamento deve ser enviada como imagem"
            };
        }

        var path = SaveTempFile(file);

        try
        {
            var text = _ocrService.ExtractText(path);
            var score = CalculateScore(text);

            var nameResult = _helperService.CheckNameMatch(text, candidateName);

            return new DocumentValidationDetail
            {
                DocumentName = "Certidão de Nascimento ou Casamento",
                Score = score,
                Status = score >= 60 ? "OK" : "Inconsistente",
                Message = score >= 60
                    ? $"Certidão de Nascimento ou Casamento válida e {nameResult.Message}"
                    : $"Certidão de Nascimento ou Casamento pode estar incorreta e {nameResult.Message}"
            };
        }
        finally
        {
            File.Delete(path);
        }
    }

    public int CalculateScore(string text)
    {
        var normalized = Normalize(text);

        int score = 0;

        if (normalized.Contains("CERTIDAODENASCIMENTO") || normalized.Contains("CERTIDAODECASAMENTO"))
            score += 20;

        if (normalized.Contains("NASCIMENTO") || normalized.Contains("CASAMENTO"))
            score += 20;

        return score;
    }

    private string Normalize(string text)
    {
        return text
            .ToUpper()
            .Replace("/", ".")
            .Replace("-", "")
            .Replace(".", "")
            .Replace(" ", "")
            .Replace("\n", "")
            .Replace("\r", "");
    }
}