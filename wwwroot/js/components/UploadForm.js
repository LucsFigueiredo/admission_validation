import documents from "../data/documents.js";
import { filesAccepted } from "../data/documents.js";

export default function UploadForm() {
    return `
    <section class="forms">
        <h2 class="forms__title">Formulário de Envio de Documentos</h2>
        <form class="forms__form" id="document-form">
            ${documents.map(document => `
                <div class="forms__form-group">
                    <label for="${document.id}" class="forms__form-label">${document.name}${document.required ? " *" : ""}</label>
                    <input type="file" id="${document.id}" name="${document.id}" class="forms__form-input" ${document.required ? "required" : ""} accept="${filesAccepted.join(", ")}">
                </div>
            `).join("")}
        </form>
    </section>
    `;
}