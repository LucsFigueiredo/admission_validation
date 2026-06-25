using System.Text.RegularExpressions;

public class AdressValidator
{
    public int CalculateScore(string text)
    {
        var normalized = Normalize(text);

        int score = 0;

        if (normalized.Contains("CEP"))
            score += 20;

        if (normalized.Contains("RUA") || normalized.Contains("AVENIDA"))
            score += 5;

        if (normalized.Contains("BAIRRO"))
            score += 5;

        if (normalized.Contains("CIDADE"))
            score += 5;

        if (HasCepNumber(normalized))
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

    private bool HasCepNumber(string text)
    {
        return Regex.IsMatch(text, @"\d{5}-\d{3}") ||
            Regex.IsMatch(text, @"\d{8}");
    }
}