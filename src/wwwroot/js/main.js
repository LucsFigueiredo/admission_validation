import Home from "./pages/Home.js";

function Main() {
    console.log("App initialized!");
    const app = document.getElementById("app");

    app.innerHTML = Home();
}

Main();