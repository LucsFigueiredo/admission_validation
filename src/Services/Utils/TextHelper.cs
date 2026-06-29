public class TextHelper()
{    
    public class NameMatchResult
    {
        public int Matches { get; set; }
        public int TotalParts { get; set; }
        public string Message { get; set; } = "";
        public bool IsMatch { get; set; }
    }

    public NameMatchResult CheckNameMatch(string text, string candidateName)
    {
        var normalizedText = text.ToUpper();
        var nameParts = candidateName.ToUpper().Split(" ");

        int matches = nameParts.Count(part => normalizedText.Contains(part));

        var result = new NameMatchResult
        {
                Matches = matches,
                TotalParts = nameParts.Length,
                IsMatch = matches >= nameParts.Length / 2
        };

        if (result.IsMatch)
        {
            result.Message = "Nome parcialmente compatível com o candidato.";
        }
        else
        {
            result.Message = "Nome não corresponde ao candidato.";
        }

        return result;
    }
}