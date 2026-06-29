export const documentsConfig = [
    {
        label: "RG ou CIN",
        description: "Deve constar frente e verso.",
        name: "RGFile"
    },
    {
        label: "CPF",
        description: "Será aceito o CIN ou RG caso conste.",
        name: "CPFFile"
    },
    {
        label: "Certificado de reservista",
        description: "Obrigatório para candidatos masculinos.",
        name: "MilitaryCertificateFile",
        condition: (data) => data.gender === "Masculino"
    },
    {
        label: "Comprovante de Endereço",
        description: "Água, luz, telefone, etc.",
        name: "AddressProofFile"
    },
    {
        label: "PIS ou PASEP",
        description: "Caso não possua, gerar junto ao banco.",
        name: "PisFile"
    },
    {
        label: "Extrato - Santander",
        description: "Deve conter agência 4338.",
        name: "ExtratoFile"
    },
    {
        label: "Certidão de Nascimento ou Casamento",
        description: "Enviar verso se houver.",
        name: "NascimentoCasamentoFile"
    },
    {
        label: "Certidão de Antecedentes Criminais",
        description: "Preferencialmente estadual.",
        name: "AntecedentesFile"
    },
    {
        label: "Diploma ou Certificado",
        description: "Frente e verso.",
        name: "DiplomaFile"
    },
    {
        label: "Histórico Escolar",
        description: "Frente e verso.",
        name: "HistoricoFile"
    },
    {
        label: "Declaração de Bens",
        description: "Aceita IR completo.",
        name: "BensFile"
    },
    {
        label: "Declaração de Proventos",
        description: "Se aplicável.",
        name: "ProventosFile"
    },
    {
        label: "Declaração de Acúmulo",
        description: "Caso acumule cargo público.",
        name: "AcumuloFile"
    },
    {
        label: "Título de Eleitor",
        description: "Com comprovante da última eleição.",
        name: "VoterCardFile"
    },
    {
        label: "CNH",
        description: "Opcional. Não pode estar vencida.",
        name: "CNHFile",
        optional: true
    },
    {
        label: "Declaração Processo Administrativo",
        description: "Somente se trabalhou em órgão público.",
        name: "ProcessoAdmFile",
        condition: (data) => data.hasPublicJob === "Sim"
    }
];
``