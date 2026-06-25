using System.Text.RegularExpressions;

public class CpfValidator
{
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