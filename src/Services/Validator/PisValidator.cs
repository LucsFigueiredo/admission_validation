using System.Text.RegularExpressions;
using admission_validation.Models;
using admission_validation.Services;

public class PisValidator
{
    private readonly FileStorageService _storageService;
    private readonly OcrService _ocrService;
    private readonly TextHelper _helperService;

    public PisValidator(OcrService ocrService, FileStorageService storageService, TextHelper helperService)
    {
        _ocrService = ocrService;
        _storageService = storageService;
        _helperService = helperService;
    }

    private string SaveTempFile(IFormFile file)
    {
        return _storageService.SaveTemp(file);
    }

    public DocumentValidationDetail ValidatePis(IFormFile file, string candidateName)
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
            var score = CalculateScore(text);

            var nameResult = _helperService.CheckNameMatch(text, candidateName);

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

    public int CalculateScore(string text)
    {
        var normalized = Normalize(text);

        int score = 0;

        if (normalized.Contains("PIS"))
            score += 40;

        if (normalized.Contains("CARTAOCIDADAO"))
            score += 40;

        if (HasPisNumber(normalized))
            score += 60;

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

    private bool HasPisNumber(string text)
    {
        return Regex.IsMatch(text, @"\d{3}\.\d{5}\.\d{2}\.\d{1}") ||
            Regex.IsMatch(text, @"\d{14}");
    }
}