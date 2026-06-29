export default function PreviousJobs() {
    return `
    <label>Quantidade de empregos anteriores:</label>
    <input type="number" id="jobCount" min="0" max="5">
    <div id="jobDocs"></div>
    `;
}

document.addEventListener("input", (e) => {
    if (e.target.id === "jobCount") {
        const count = parseInt(e.target.value || 0);
        const container = document.getElementById("jobDocs");

        container.innerHTML = "";

        for (let i = 1; i <= count; i++) {
            container.innerHTML += `
                <h4>Emprego ${i}</h4>

                <div name="jobDiv ${i}">
                <label>Empresa:</label>
                <input type="text" name="Job ${i}">
                <label>Previdência:</label>
                <input type="text" name="JobPrev ${i}">
                <label>Admissão:</label>
                <input type="text" name="JobAdm ${i}">
                <label>Demissão:</label>
                <input type="text" name="JobDem ${i}">
                </div>
            `;
        }
    }
});