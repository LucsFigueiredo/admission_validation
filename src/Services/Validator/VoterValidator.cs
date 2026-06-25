using System.Text.RegularExpressions;

public class VoterValidator
{
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