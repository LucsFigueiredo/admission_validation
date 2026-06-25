export default function UploadForm() {
    return `
    <section class="forms">
        <h2 class="forms__title">Formulário de Envio de Documentos</h2>
        <form action="/api/documents/upload" method="post" enctype="multipart/form-data">

            <label>Nome:</label>
            <input type="text" name="CandidateName" />

            <label>RG ou CIN:</label>
            <input type="file" name="RG" />

            <label>CPF:</label>
            <input type="file" name="CPF" />

            <label>Certificado de reservista:</label>
            <input type="file" name="MilitaryCertificate" />

            <select name="IsMale">
                <option value="true">Masculino</option>
                <option value="false">Feminino</option>
            </select>

            <select name="EmployeeType">
                <option value="Efetivo">Efetivo</option>
                <option value="Comissionado">Comissionado</option>
                <option value="Temporario">Temporario</option>
            </select> 

            <button type="submit">Enviar</button>

        </form>
    </section>
    `;
}