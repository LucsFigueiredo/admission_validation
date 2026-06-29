using System.Text.RegularExpressions;
using admission_validation.Models;
using admission_validation.Services;


public class VoterValidator
{
    private readonly FileStorageService _storageService;
    private readonly OcrService _ocrService;
    private readonly TextHelper _helperService;

    public VoterValidator(OcrService ocrService, FileStorageService storageService, TextHelper helperService)
    {
        _ocrService = ocrService;
        _storageService = storageService;
        _helperService = helperService;
    }

    private string SaveTempFile(IFormFile file)
    {
        return _storageService.SaveTemp(file);
    }

    public DocumentValidationDetail ValidateVoterCard(IFormFile file, string candidateName)
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
            var score = CalculateScore(text);

            var nameResult = _helperService.CheckNameMatch(text, candidateName);

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

    public int CalculateScore(string text)
    {
        var normalized = Normalize(text);
        int score = 0;

        if (normalized.Contains("TITULOELEITORAL"))
            score += 20;

        if (HasVoterNumber(normalized))
            score += 40;

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

    private bool HasVoterNumber(string text)
    {
        return Regex.IsMatch(text, @"\d{4}\.\d{4}\.\d{4}") ||
           Regex.IsMatch(text, @"\d{12}");

    }
}