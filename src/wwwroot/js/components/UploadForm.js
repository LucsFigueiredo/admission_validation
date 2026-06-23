export default function UploadForm() {
    return `
    <section class="forms">
        <h2 class="forms__title">Formulário de Envio de Documentos</h2>
        <form action="/api/documents/upload" method="post" enctype="multipart/form-data">

            <label>Nome:</label>
            <input type="text" name="CandidateName" />

            <label>RG (Frente):</label>
            <input type="file" name="RGFront" />

            <label>RG (Verso):</label>
            <input type="file" name="RGBack" />

            <label>CPF:</label>
            <input type="file" name="CPF" />

            <label>É homem?</label>
            <input type="checkbox" name="IsMale" value="true" />

            <label>Certificado de reservista:</label>
            <input type="file" name="MilitaryCertificate" />

            <button type="submit">Enviar</button>

        </form>
    </section>
    `;
}