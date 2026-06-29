using System.Text.RegularExpressions;
using admission_validation.Models;
using admission_validation.Services;

public class AntecedentesValidator
{
    private readonly FileStorageService _storageService;
    private readonly OcrService _ocrService;
    private readonly TextHelper _helperService;

    public AntecedentesValidator(OcrService ocrService, FileStorageService storageService, TextHelper helperService)
    {
        _ocrService = ocrService;
        _storageService = storageService;
        _helperService = helperService;
    }

    private string SaveTempFile(IFormFile file)
    {
        return _storageService.SaveTemp(file);
    }

    public DocumentValidationDetail ValidateAntecedentes(IFormFile file, string candidateName)
    {
        var extension = Path.GetExtension(file.FileName).ToLower();

        if (extension != ".png" && extension != ".jpg" && extension != ".jpeg")
        {
            return new DocumentValidationDetail
            {       
                DocumentName = "Antecedentes Criminais",
                Score = 0,
                Status = "Formato inválido",
                Message = "Antecedentes Criminais deve ser enviado como imagem"
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
                DocumentName = "Antecedentes Criminais",
                Score = score,
                Status = score >= 60 ? "OK" : "Inconsistente",
                Message = score >= 60
                    ? $"Antecedentes Criminais válido e {nameResult.Message}"
                    : $"Antecedentes Criminais pode estar incorreto e {nameResult.Message}"
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

        if (normalized.Contains("ANTECEDENTESCRIMINAIS"))
            score += 20;

        if (normalized.Contains("NÃOCONSTA"))
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