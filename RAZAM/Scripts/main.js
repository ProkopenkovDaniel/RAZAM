//Reload page for test
setTimeout(function () {
    if (!document.getElementById("modDialog").classList.contains("Showed")) {
        window.location.reload();
    }
}, 100000);
window.addEventListener("load", function () {
    var addButton = document.getElementById("AddButton");
    var closeButton = document.getElementById("CloseAddButton");
    addButton.onclick = ModalWindowShow;
    closeButton.onclick = ModalWindowHide;
});
function ModalWindowShow() {
    var modal = document.getElementById("modDialog");
    modal.style.display = "block"
    modal.style.opacity = 1;
    modal.classList.add("Showed");
}
function ModalWindowHide() {
    var modal = document.getElementById("modDialog");
    modal.style.display = "none"
    modal.style.opacity = 0;
    modal.classList.remove("Showed");
}