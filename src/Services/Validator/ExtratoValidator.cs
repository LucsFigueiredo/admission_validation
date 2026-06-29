using System.Text.RegularExpressions;
using admission_validation.Models;
using admission_validation.Services;

public class ExtratoValidator
{
    private readonly FileStorageService _storageService;
    private readonly OcrService _ocrService;
    private readonly TextHelper _helperService;

    public ExtratoValidator(OcrService ocrService, FileStorageService storageService, TextHelper helperService)
    {
        _ocrService = ocrService;
        _storageService = storageService;
        _helperService = helperService;
    }

    private string SaveTempFile(IFormFile file)
    {
        return _storageService.SaveTemp(file);
    }

    public DocumentValidationDetail ValidateExtrato(IFormFile file, string candidateName)
    {
        var extension = Path.GetExtension(file.FileName).ToLower();

        if (extension != ".png" && extension != ".jpg" && extension != ".jpeg")
        {
            return new DocumentValidationDetail
            {       
                DocumentName = "Extrato - Santander",
                Score = 0,
                Status = "Formato inválido",
                Message = "Extrato do Santander deve ser enviado como imagem"
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
                DocumentName = "Extrato",
                Score = score,
                Status = score >= 60 ? "OK" : "Inconsistente",
                Message = score >= 60
                    ? $"Extrato válido e {nameResult.Message}"
                    : $"Extrato pode estar incorreto e {nameResult.Message}"
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

        if (normalized.Contains("SANTANDER"))
            score += 20;

        if (normalized.Contains("4338"))
            score += 20;

        if (normalized.Contains("EXTRATO"))
            score += 5;

        if (HasAccountNumber(normalized))
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

    private bool HasAccountNumber(string text)
    {
        return Regex.IsMatch(text, @"\d{8}-\d{1}") ||
            Regex.IsMatch(text, @"\d{9}");
    }
}