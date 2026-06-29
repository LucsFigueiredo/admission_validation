import Children from "./Children.js";
import PreviousJobs from "./PreviousJobs.js";

export default function MultiStepForm() {
    return `
    <form id="multiStepForm" enctype="multipart/form-data">

        <!-- ETAPA 1 -->
        <div class="step" id="step-1">
            <h3>Dados Pessoais</h3>

            <label>Nome (sem abreviações):</label>
            <input type="text" name="FullName" required>

            <label>Data de nascimento:</label>
            <input type="date" name="BirthDate" required>

            <label>Estado civil:</label>
            <input type="text" name="MaritalStatus" required>

            <label>Grau de instrução:</label>
            <select name="Degree" id="degree" required>
                <option value="">Selecione</option>
                <option value="Fundamental">Ensino Fundamental Completo</option>
                <option value="Medio">Ensino Médio Completo</option>
                <option value="Superior">Ensino Superior Completo</option>
                <option value="Pos">Pós-Graduação Completa</option>
                <option value="Mestre">Mestrado Completo</option>
                <option value="Doutor">Doutorado Completo</option>
            </select>

            <label>Nacionalidade:</label>
            <input type="text" name="Nationality" required>

            <label>Sexo:</label>
            <select name="Gender" id="gender" required>
                <option value="">Selecione</option>
                <option value="Masculino">Masculino</option>
                <option value="Feminino">Feminino</option>
            </select>

            <label>Naturalidade:</label>
            <input type="text" name="BirthPlace" required>

            <label>UF:</label>
            <input type="text" name="UF" required>

            <label>Telefone Residencial:</label>
            <input type="number" name="TelRes" required>

            <label>Endereço:</label>
            <input type="text" name="Adress" required>

            <label>N:</label>
            <input type="number" name="Num" required>

            <label>Bairro:</label>
            <input type="text" name="Neighbor" required>

            <label>Complemento:</label>
            <input type="text" name="Comp" required>

            <label>CEP:</label>
            <input type="number" name="CEP" required>

            <label>Cidade:</label>
            <input type="text" name="City" required>

            <label>Celular:</label>
            <input type="number" name="Cel" required>

            <label>Telefone para recado:</label>
            <input type="number" name="TelRec">

            <label>Recado com:</label>
            <input type="text" name="Rec">

            <label>Email:</label>
            <input type="email" name="Email" required>

            <button type="button" onclick="nextStep()">Avançar</button>
        </div>


        <!-- ETAPA 2 -->
        <div class="step" id="step-2" style="display:none">
            <h3>Documentos e Outros Dados</h3>

            <label>RG:</label>
            <input type="number" name="RG" required>

            <label>Data de Expedição:</label>
            <input type="date" name="RgExpedition" required>

            <label>UF:</label>
            <input type="text" name="RGUF" required>

            <label>CTPS n°:</label>
            <input type="number" name="CTPS required">

            <label>Série:</label>
            <input type="number" name="CTPSSr" required>

            <label>UF:</label>
            <input type="text" name="CTPSUF" required>

            <label>CPF:</label>
            <input type="number" name="CPF" required>

            <label>Título de Eleitor n°:</label>
            <input type="number" name="VoterCard" required>

            <label>Zona:</label>
            <input type="number" name="Zone" required>

            <label>Seção:</label>
            <input type="number" name="Section" required>

            <label>PIS:</label>
            <input type="number" name="PIS" required>

            <label>Banco Santander - Agência 4338 (Ganha Tempo) - Conta Corrente n°</label>
            <input type="number" name="Bank" required>

            <label>Raça/Cor:</label>
            <select name="Race" id="race" required>
                <option value="">Selecione</option>
                <option value="Branca">Branca</option>
                <option value="Negra">Negra</option>
                <option value="Amarela">Amarela</option>
                <option value="Parda">Parda</option>
                <option value="Indigena">Indigena</option>
            </select>

            <div name="DeficiencyDiv">
            <label>Deficiência:</label>
            <select name="Deficiency" id="deficiency">
                <option value="">Selecione</option>
                <option value="Mental">Mental</option>
                <option value="Auditivo">Auditivo</option>
                <option value="Física">Física</option>
                <option value="Parda">Parda</option>
                <option value="Indigena">Indigena</option>
            </select>
            <label>Outros:</label>
            <input type="text" name="DeficiencyOthers">
            </div>

            <label>Primeiro Emprego:</label>
            <select name="FirstJob" id="firstjob" required>
                <option value="">Selecione</option>
                <option value="Sim">Sim</option>
                <option value="Não">Não</option>
            </select>

            <label>Você já foi funcionário(a) público?</label>
            <select name="HasPublicJob" id="haspublicjob" required>
                <option value="">Selecione</option>
                <option value="Sim">Sim</option>
                <option value="Não">Não</option>
            </select>

            <div name="BarueriJobDiv">
            <label>Você já foi funcionário(a) público municipal em Barueri?</label>
            <select name="BarueriJob" id="baruerijob" required>
                <option value="">Selecione</option>
                <option value="Sim">Sim</option>
                <option value="Não">Não</option>
            </select>
            <label>Onde?</label>
            <input type="text" name="BarueriJob2">
            <label>Quando?</label>
            <input type="text" name="BarueriJob3">
            </div>

            <button type="button" onclick="prevStep()">Voltar</button>
            <button type="button" onclick="nextStep()">Avançar</button>
        </div>


        <!-- ETAPA 3 -->
        <div class="step" id="step-3" style="display:none">
            <h3>Familiares</h3>

            <div name="FatherDiv">
            <label>Nome do pai:</label>
            <input type="text" name="FatherName">
            <label>Dependente para fins de imposto de renda?</label>
            <select name="Father" id="father">
                <option value="">Selecione</option>
                <option value="Sim">Sim</option>
                <option value="Não">Não</option>
            </select>
            </div>

            <div name="MotherDiv">
            <label>Nome da mãe:</label>
            <input type="text" name="MotherName">
            <label>Dependente para fins de imposto de renda?</label>
            <select name="Mother" id="mother">
                <option value="">Selecione</option>
                <option value="Sim">Sim</option>
                <option value="Não">Não</option>
            </select>
            </div>

            <div name="GrandFatherDiv">
            <label>Avós Paternos:</label>
            <input type="text" name="GrandFatherName">
            <label>Dependente para fins de imposto de renda?</label>
            <select name="GrandFather" id="grandfather">
                <option value="">Selecione</option>
                <option value="Sim">Sim</option>
                <option value="Não">Não</option>
            </select>
            </div>

            <div name="GrandMotherDiv">
            <label>Avós Maternos:</label>
            <input type="text" name="GrandMotherName">
            <label>Dependente para fins de imposto de renda?</label>
            <select name="GrandMother" id="grandmother">
                <option value="">Selecione</option>
                <option value="Sim">Sim</option>
                <option value="Não">Não</option>
            </select>
            </div>

            <div name="PartnerDiv">
            <label>Cônjuge:</label>
            <input type="text" name="Partner">
            <label>Dependente para fins de imposto de renda?</label>
            <select name="Partner" id="partner">
                <option value="">Selecione</option>
                <option value="Sim">Sim</option>
                <option value="Não">Não</option>
            </select>
            <label>Trabalha na municipalidade?</label>
            <select name="Partner2" id="partner2">
                <option value="">Selecione</option>
                <option value="Sim">Sim</option>
                <option value="Não">Não</option>
            </select>
            <label>CPF:</label>
            <input type="text" name="PartnerCPF">
            </div>

            ${Children()}

            <button type="button" onclick="prevStep()">Voltar</button>
            <button type="button" onclick="nextStep()">Avançar</button>
        </div>


        <!-- ETAPA 4 -->
        <div class="step" id="step-4" style="display:none">
            <h3>Histórico Profissional</h3>

            ${PreviousJobs()}

            <button type="button" onclick="prevStep()">Voltar</button>
            <button type="button" onclick="nextStep()">Avançar</button>
        </div>


        <!-- ETAPA 5 -->
        <div class="step" id="step-5" style="display:none">
            <h3>Upload de Documentos</h3>

            <div id="documentsContainer"></div>

            <button type="button" onclick="prevStep()">Voltar</button>
            <button type="submit">Enviar</button>
        </div>

    </form>
    `;
}