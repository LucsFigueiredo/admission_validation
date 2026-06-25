using System.Text.RegularExpressions;

public class PisValidator
{
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