import Home from "./pages/Home.js";

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

Main();