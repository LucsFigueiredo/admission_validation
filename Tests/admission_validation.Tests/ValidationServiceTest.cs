using admission_validation.Services;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

public class ValidationServiceTest
{

    [Fact]
    public void ValidateDocument_ReturnsTrue_WhenAllRequiredDocumentsArePresent()
    {
        var service = new ValidationService();

        // Arrange
        var uploadedDocuments = new List<string>
        {
            "RG (Frente)",
            "RG (Verso)",
            "CPF",
            "PIS ou PASEP",
            "Comprovante de Endereço",
            "Ficha Admissional",
            "Extrato Santander",
            "Certidão de Nascimento ou Casamento",
            "Certidão de Antecedentes Criminais",
            "Título de Eleitor",
            "Declaração de Bens",
            "Declaração de Proventos decorrentes de aposentadoria ou pensão",
            "Declaração de Acúmulo",
            "Diploma ou Certificado Escolar",
            "Histórico Escolar",
        };

        // Act
        var result = service.ValidateDocument(uploadedDocuments);

        // Assert
        Assert.True(result.IsValid);
    }
}