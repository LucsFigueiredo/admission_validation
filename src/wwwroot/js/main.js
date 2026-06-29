import Home from "./pages/Home.js";
import { documentsConfig } from "./data/DocumentsConfig.js";

function Main() {
    console.log("App initialized!");
    const app = document.getElementById("app");

    app.innerHTML = Home();

    const form = document.querySelector("form");

    form.addEventListener("submit", async (e) => {
    e.preventDefault();

    const resultDiv = document.getElementById("resultMessage");

    resultDiv.innerHTML = `<p>⏳ Processando documentos...</p>`;

    const button = form.querySelector("button");

    button.disabled = true;

    const formData = new FormData(form);

    const response = await fetch("/api/documents/upload", {
        method: "POST",
        body: formData
    });

    const result = await response.json();

    showResult(result);
});
}

function showResult(result) {
    const resultDiv = document.getElementById("resultMessage");
    resultDiv.innerHTML = ""; // limpa anterior

    const form = document.querySelector("form");
    const button = form.querySelector("button");

    if (result.status === "Approved") {
        resultDiv.innerHTML = `
            <p style="color: green;">
                ✅ Documentos enviados com sucesso!
            </p>
        `;
        button.disabled = false;

        form.reset();

        return;
    }

    let html = `<p style="color: red;">⚠️ Problemas encontrados:</p><ul>`;

    result.documents.forEach(doc => {
        if (doc.status !== "OK") {  
            html += `
            <li>
                <strong>${doc.documentName}</strong><br/>
                <span>${doc.message}</span>
            </li>
            `;
        }
    });

    html += "</ul>";

    resultDiv.innerHTML = html;

    button.disabled = false;
    form.reset();
}

let currentStep = 1;
const totalSteps = 5;

window.nextStep = function () {

    if (!validateStep(currentStep)) return;

    document.getElementById(`step-${currentStep}`).style.display = "none";
    currentStep++;
    document.getElementById(`step-${currentStep}`).style.display = "block";

    if (currentStep === 5) {
        renderDocuments();
    }
};

window.prevStep = function () {
    document.getElementById(`step-${currentStep}`).style.display = "none";
    currentStep--;
    document.getElementById(`step-${currentStep}`).style.display = "block";
};

function validateStep(step) {
    const stepElement = document.getElementById(`step-${step}`);

    const inputs = stepElement.querySelectorAll("input, select, textarea");

    for (let input of inputs) {
        if (!input.checkValidity()) {
            input.reportValidity(); // mostra erro padrão do navegador
            return false;
        }
    }

    return true;
}

window.renderDocuments = function () {
    const container = document.getElementById("documentsContainer");

    const gender = document.querySelector("[name='Gender']")?.value;
    const hasPublicJob = document.querySelector("[name='HasPublicJob']")?.value;

    const data = { gender, hasPublicJob };

    container.innerHTML = "";

    documentsConfig.forEach(doc => {
        if (doc.condition && !doc.condition(data)) return;

        container.innerHTML += `
            <div class="doc-item">
                <label>${doc.label}</label>
                <p class="description">${doc.description}</p>
                <input type="file" name="${doc.name}" ${doc.optional ? "" : "required"}>
            </div>
        `;
    });
};




Main();