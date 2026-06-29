export default function Children() {
    return `
    <label>Quantidade de filhos dependentes e/ou menores de 18 anos:</label>
    <input type="number" id="childrenCount" min="0" max="5">
    <div id="childrenDocs"></div>
    `;
}

document.addEventListener("input", (e) => {
    if (e.target.id === "childrenCount") {
        const count = parseInt(e.target.value || 0);
        const container = document.getElementById("childrenDocs");

        container.innerHTML = "";

        for (let i = 1; i <= count; i++) {
            container.innerHTML += `
                <h4>Filho ${i}</h4>

                <div name="ChildDiv ${i}">
                <label>Filho(a):</label>
                <input type="text" name="Child ${i}">
                <label>Data de nascimento:</label>
                <input type="text" name="ChildBirth ${i}">
                <label>Local:</label>
                <input type="text" name="ChildLocal ${i}">
                <label>Registro n°:</label>
                <input type="text" name="ChildReg ${i}">
                <label>Livro:</label>
                <input type="text" name="ChildBook ${i}">
                <label>Folha:</label>
                <input type="text" name="ChildPage ${i}">
                <label>Cartório:</label>
                <input type="text" name="ChildCat ${i}">
                <label>Dependente para fins de imposto de renda?</label>
                <select name="ChildOpt ${i}" id="child ${i}">
                    <option value="">Selecione</option>
                    <option value="Sim">Sim</option>
                    <option value="Não">Não</option>
                </select>
                <label>CPF:</label>
                <input type="text" name="ChildCPF ${i}">
                </div>
            `;
        }
    }
});