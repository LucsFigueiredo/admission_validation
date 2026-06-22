namespace admission_validation.Tests;

public class UnitTest1
{
    [Fact]
    public void Should_Require_Military_Certificate_When_Male()
    {
        var service = new DocumentValidationService();

        var request = new DocumentUploadRequest
        {
            IsMale = true,
            RGFront = FakeFile(),
            RGBack = FakeFile(),
            CPF = FakeFile()
        };

        var result = service.Validate(request);

        Assert.Contains("Certificado de reservista é obrigatório", result);
    }
}
