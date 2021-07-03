function displayBusyIndicator() {
    document.getElementById("loading").style.display = "block";
    document.getElementById("page-content").style.display = "none";
    document.body.style.opacity = ".5";
}