using System.Data.SqlTypes;
using System.Text.RegularExpressions;
using admission_validation.Models;
using admission_validation.Services;

public class RgValidator
{
    private readonly FileStorageService _storageService;
    private readonly OcrService _ocrService;
    private readonly TextHelper _helperService;

    public RgValidator(OcrService ocrService, FileStorageService storageService, TextHelper helperService)
    {
        _ocrService = ocrService;
        _storageService = storageService;
        _helperService = helperService;
    }

    private string SaveTempFile(IFormFile file)
    {
        return _storageService.SaveTemp(file);
    }

    public DocumentValidationDetail ValidateRG(IFormFile file, string candidateName)
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
            var score = CalculateScore(text);

            var nameResult = _helperService.CheckNameMatch(text, candidateName);

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
    public int CalculateScore(string text)
    {
        var normalized = Normalize(text);
        int score = 0;

        // RG clássico
        if (normalized.Replace("6", "G").Contains("RG"))
            score += 20;

        if (normalized.Contains("REGISTRO"))
            score += 20;

        if (normalized.Contains("GERAL"))
            score += 20;

        // CIN / CPF
        if (normalized.Contains("CPF"))
            score += 20;

        if (normalized.Contains("CARTEIRADEIDENTIDADE"))
            score += 20;

        if (HasCpfNumber(normalized))
            score += 30;

        if (HasRgNumber(normalized))
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

    private bool HasCpfNumber(string text)
    {
        return Regex.IsMatch(text, @"\d{3}\.\d{3}\.\d{3}-\d{2}") ||
            Regex.IsMatch(text, @"\d{11}");
    }

    private bool HasRgNumber(string text)
    {
        return Regex.IsMatch(text, @"\d{1,2}\.\d{3}\.\d{3}") ||
           Regex.IsMatch(text, @"\d{7,9}");

    }
}