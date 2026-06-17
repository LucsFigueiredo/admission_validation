const documents = [
  {
    name: "RG (Frente)",
    id: "rg-frente",
    required: true,
  },
  {
    name: "RG (Verso)",
    id: "rg-verso",
    required: true,
  },
  {
    name: "CPF",
    id: "cpf",
    required: true,
  },
  {
    name: "PIS ou PASEP",
    id: "pis",
    required: true,
  },
  {
    name: "Comprovante de Endereço",
    id: "comprovante",
    required: true,
  },
  {
    name: "Ficha Admissional",
    id: "ficha-admissional",
    required: true,
  },
  {
    name: "Extrato Santander",
    id: "extrato-bancario",
    required: true,
  },
  {
    name: "Certidão de Nascimento ou Casamento",
    id: "certidao-nascimento",
    required: true,
  },
  {
    name: "Certidão de Antecedentes Criminais",
    id: "antescedentes-criminais",
    required: true,
  },
  {
    name: "Título de Eleitor",
    id: "titulo-eleitor",
    required: true,
  },
  {
    name: "Declaração de Bens",
    id: "declaração-bens",
    required: true,
  },
  {
    name: "Declaração de Proventos decorrentes de aposentadoria ou pensão",
    id: "declaração-proventos",
    required: true,
  },
  {
    name: "Declaração de Acúmulo",
    id: "declaração-acumulo",
    required: true,
  },
  {
    name: "Diploma ou Certificado Escolar",
    id: "diploma",
    required: true,
  },
  {
    name: "Histórico Escolar",
    id: "historico-escolar",
    required: true,
  },
  {
    name: "Certificado de Reservista",
    id: "certificado-reservista",
    required: false,
  },
  {
    name: "CNH",
    id: "cnh",
    required: false,
  },
  {
    name: "Certidão de Nascimento dos Filhos menores de 18 anos",
    id: "certidão-nascimento-filhos",
    required: false,
  },
  {
    name: "CPF dos Filhos menores de 18 anos",
    id: "cpf-filhos",
    required: false,
  },
  {
    name: "Vacinação dos Filhos menores de 7 anos",
    id: "vacinação-filhos",
    required: false,
  },
  {
    name: "Matrícula Escolar dos Filhos maiores de 6 anos",
    id: "matricula-filhos",
    required: false,
  },
];

const filesAccepted = [
  "image/jpeg",
  "image/png",
  "image/jpg",
  "application/pdf",
];

export default documents;
export { filesAccepted };