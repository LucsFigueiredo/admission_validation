using admission_validation.Models;
using admission_validation.Services;
using admission_validation.Tests.Helpers;

namespace admission_validation.Tests;

public class RequireMilitaryCertificate
{
    [Fact]
    public void Should_Require_Military_Certificate_When_Male()
    {
        var service = new DocumentValidationService();

        var request = new DocumentUploadRequest
        {
            IsMale = true,
            RGFront = FileHelper.CreateFakeFile(),
            RGBack = FileHelper.CreateFakeFile(),
            CPF = FileHelper.CreateFakeFile()
        };

        var result = service.Validate(request);

        Assert.Contains("Certificado de reservista é obrigatório", result);
    }
}
