using admission_validation.Models;

namespace admission_validation.Data
{
    public static class Documents
    {
        public static readonly List<DocumentDefinition> All =
        [
            new("RG (Frente)", true),
            new("RG (Verso)", true),
            new("CPF", true),
            new("PIS ou PASEP", true),
            new("Comprovante de Endereço", true),
            new("Ficha Admissional", true),
            new("Extrato Santander", true),
            new("Certidão de Nascimento ou Casamento", true),
            new("Certidão de Antecedentes Criminais", true),
            new("Título de Eleitor", true),
            new("Declaração de Bens", true),
            new("Declaração de Proventos decorrentes de aposentadoria ou pensão", true),
            new("Declaração de Acúmulo", true),
            new("Diploma ou Certificado Escolar", true),
            new("Histórico Escolar", true),
            new("Certificado de Reservista", false),
            new("CNH", false),
            new("Certidão de Nascimento dos Filhos menores de 18 anos", false),
            new("CPF dos Filhos menores de 18 anos", false),
            new("Vacinação dos Filhos menores de 7 anos", false),
            new("Matrícula Escolar dos Filhos maiores de 6 anos", false)
        ];
    }
}