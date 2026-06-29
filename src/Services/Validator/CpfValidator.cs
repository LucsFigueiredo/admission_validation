using System.Text.RegularExpressions;
using admission_validation.Models;
using admission_validation.Services;

public class CpfValidator
{
    private readonly FileStorageService _storageService;
    private readonly OcrService _ocrService;
    private readonly TextHelper _helperService;

    public CpfValidator(OcrService ocrService, FileStorageService storageService, TextHelper helperService)
    {
        _ocrService = ocrService;
        _storageService = storageService;
        _helperService = helperService;
    }

    private string SaveTempFile(IFormFile file)
    {
        return _storageService.SaveTemp(file);
    }

    public DocumentValidationDetail ValidateCPF(IFormFile file, string candidateName)
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
            var score = CalculateScore(text);

            var nameResult = _helperService.CheckNameMatch(text, candidateName);

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

    public int CalculateScore(string text)
    {
        var normalized = Normalize(text);

        int score = 0;

        if (normalized.Contains("CPF"))
            score += 40;

        if (normalized.Contains("CADASTRODEPESSOASFISICAS"))
            score += 40;

        if (HasCpfNumber(normalized))
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

    private bool HasCpfNumber(string text)
    {
        return Regex.IsMatch(text, @"\d{3}\.\d{3}\.\d{3}-\d{2}") ||
            Regex.IsMatch(text, @"\d{11}");
    }
}