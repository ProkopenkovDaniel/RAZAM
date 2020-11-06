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
}
function ModalWindowHide() {
    var modal = document.getElementById("modDialog");
    modal.style.display = "none"
    modal.style.opacity = 0;
}