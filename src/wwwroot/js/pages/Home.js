import Header from "../components/Header.js";
import UploadForm from "../components/UploadForm.js";

export default function Home() {
    return `
    ${Header()}
    <main class="home">
        <h2 class="home__title">Bem-vindo ao sistema de envio de documentos da FIEBTECH!</h2>
        <p class="home__description">Aqui você pode enviar os documentos necessários para o processo de admissão. Por favor, preencha o formulário abaixo com os documentos solicitados.</p>
        ${UploadForm()}
    </main>
    `;
}