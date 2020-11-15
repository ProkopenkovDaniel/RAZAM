window.addEventListener("load", function () {
    var addButton = this.document.getElementById('AddEventButton');
    addButton.addEventListener('click', AddClickEvent);
})

function AddClickEvent(e) {
    var p = e.target.parentElement;
    var modalBody = p.parentElement;
    var inputs = modalBody.querySelectorAll('.box');
    inputs.forEach(function (item, index) {
        if (item.value == "") {
            item.style = "border: 2px solid #d53838;";
            e.preventDefault();
        } else {
            item.style = "border: 1px solid #222222;"
        }
    });
}