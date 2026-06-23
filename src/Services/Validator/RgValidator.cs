public class RgValidator
{
    public bool Validate(string text)
    {
        var normalized = Normalize(text);

        return normalized.Contains("RG") ||
               normalized.Contains("REGISTRO") ||
               normalized.Contains("GERAL");
    }

    private string Normalize(string text)
    {
        return text
            .ToUpper()
            .Replace(" ", "")
            .Replace("\n", "")
            .Replace("\r", "");
    }
}