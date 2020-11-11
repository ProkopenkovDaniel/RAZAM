//variables
var mainElement;
var sliderWrapper;
var sliderItems;
var sliderControls;
var sliderControlLeft;
var sliderControlRight;
var wrapperWidth;
var itemWidth;
var positionLeftItem = 0;
var transform = 0;
var step;

//the main method
window.addEventListener("load", AddListenerForSlider);
window.addEventListener("load", RefreshStatuses);
window.addEventListener("load", CardButtonClickEvent);
window.addEventListener("load", DeleteButtonClickEvent);

function AddListenerForSlider() {
    var leftSenderButton = document.getElementById("leftSenderButton");
    var rightSenderButton = document.getElementById("rightSenderButton");
    var rightReceiverButton = document.getElementById("rightReceiverButton");
    var leftReceiverButton = document.getElementById("leftReceiverButton");
    leftSenderButton.addEventListener('click', controlClick);
    rightSenderButton.addEventListener('click', controlClick);
    leftReceiverButton.addEventListener('click', controlClick);
    rightReceiverButton.addEventListener('click', controlClick);
}

function RefreshStatuses() {
    var slider = document.querySelector('#ReceiverSlider');
    var sliderItemsStyle = slider.querySelectorAll('.slider_item');
    RefrechNotesStatus(sliderItemsStyle)
}

function RefrechNotesStatus(sliderItemsStyle) {
    sliderItemsStyle.forEach(function (item, index) {
        var card = item.querySelector('.card');
        var cardBody = card.querySelector('.card-body');
        var inputStatus = cardBody.querySelector("#Status");
        var status = inputStatus.value;
        if (status == 'accepted') {
            var acceptButton = cardBody.querySelector('.acceptLink');
            AcceptNote(acceptButton);
        }
    });
}

function DeleteButtonClickEvent() {
    var slider = document.querySelector('#SenderSlider');
    var sliderItemsStyle = slider.querySelectorAll('.slider_item');
    sliderItemsStyle.forEach(function (item, index) {
        var card = item.querySelector('.card');
        var cardBody = card.querySelector('.card-body');
        var button = cardBody.querySelector('.button');
        button.addEventListener('click', DeleteNoteClick);
    });
}

function DeleteNoteClick(e) {
    var cardBody = e.target.parentElement;
    var card = cardBody.parentElement;
    var item = card.parentElement;
    DeleteNoteView(item);
    if (e.target.value == 'delete') {
        var id = item.id;
        DeleteNoteBase(item, id);
    }

}
function DeleteNoteView(item) {
    item.remove();
}
function DeleteNoteBase(item, id) {
    var card = item.querySelector('.card');
    var cardBody = card.querySelector('.card-body');
    var UrlLink = cardBody.querySelector("#UrlLink");
    var url = UrlLink.href;
    $.ajax({
        url: url,
        type: "POST",
        data: { id: id },
        success: function () {
            console.log("Note has deleted successfully");
        },
        error: function () {
            console.log("Note hasn't deleted successfully");
        }
    });
}

function CardButtonClickEvent() {
    var slider = document.querySelector('#ReceiverSlider');
    var sliderItemsStyle = slider.querySelectorAll('.slider_item');
    sliderItemsStyle.forEach(function (item, index) {
        var card = item.querySelector('.card');
        var cardBody = card.querySelector('.card-body');
        var buttons = cardBody.querySelectorAll('.button');
        buttons.forEach(function (item, index) {
            item.addEventListener('click', CardButtonClick)
            if (item.classList.contains("acceptLink")) {
                item.addEventListener('click', AcceptNoteClick);
            } else {
                item.addEventListener('click', DeleteNoteClick);
            }
        });
    });
}

function CardButtonClick(e) {
    if (e.target.classList.contains('button')) {
        e.preventDefault();
        var item = e.target;
        var status = item.value;
    }
    ChangeNoteStatus(item, status);
}

function ChangeNoteStatus(item, status) {
    var cardBody = item.parentElement;
    var card = cardBody.parentElement;
    if (status != null) {
        card.classList.forEach(function (item, intex) {
            if (item != 'card') {
                card.classList.remove(item);
            }
        })
        card.classList.add(status);
        //hardCode --
        var inputStatus = cardBody.querySelector("#Status");
        inputStatus.value = status
        //--
        var UrlLink = cardBody.querySelector("#UrlLink");
        var url = UrlLink.href;
        var slider_item = card.parentElement;
        var id = slider_item.id;
        var NoteStatus = {
            noteId: id,
            status: status
        };
        $.ajax({
            url: url,
            type: "POST",
            data: NoteStatus,
            success: function () {
                console.log("Status has changed successfully");
            },
            error: function(){
                console.log("Status hasn't changed successfully");
            }
        });
    }

}

function AcceptNoteClick(e) {
    var cardBody = e.target.parentElement;
    var card = cardBody.parentElement;
    AcceptNote(e.target);
}

function AcceptNote(item) {
    item.classList.toggle('none');
    item.nextElementSibling.classList.toggle('none');
}

//functions
function TransformItem(direct, idOfSlider) {

    mainElement = document.getElementById(idOfSlider);
    sliderWrapper = mainElement.querySelector('.slider_wrapper');
    sliderItems = mainElement.querySelectorAll('.slider_item');
    sliderControls = mainElement.querySelectorAll('.slider_control');
    sliderControlLeft = mainElement.querySelector('.slider_control_left');
    sliderControlRight = mainElement.querySelector('.slider_control_right');
    wrapperWidth = parseFloat(getComputedStyle(sliderWrapper).width);
    itemWidth = parseFloat(getComputedStyle(sliderItems[0]).width);
    step = itemWidth / wrapperWidth * 100;
    var items = [];
    sliderItems.forEach(function (item, index) {
        items.push({ item: item, position: index, transform: 0 });
    });

    var position = {
        getMin: 0,
        getMax: items.length - 1,
    }

    if (direct === 'right') {
        if ((positionLeftItem + wrapperWidth / itemWidth - 1) >= position.getMax) {
            return;
        }
        if (!sliderControlLeft.classList.contains('slider_control_show')) {
            sliderControlLeft.classList.add('slider_control_show');
        }
        if (sliderControlRight.classList.contains('slider_control_show')
            && (positionLeftItem + wrapperWidth / itemWidth) >= position.getMax) {
            sliderControlRight.classList.remove('slider_control_show');
        }
        positionLeftItem++;
        transform -= step;
    }

    if (direct === 'left') {
        if (positionLeftItem <= position.getMin) {
            return;
        }
        if (!sliderControlRight.classList.contains('slider_control_show')) {
            sliderControlRight.classList.add('slider_control_show');
        }
        if (sliderControlLeft.classList.contains('slider_control_show')
            && (positionLeftItem - 1 <= position.getMin)) {
            sliderControlLeft.classList.remove('slider_control_show');
        }
        positionLeftItem--;
        transform += step;
    }
    sliderWrapper.style.transform = 'translateX(' + transform + '%)';
}

function controlClick(e) {
    if (e.target.classList.contains('slider_control')) {
        e.preventDefault();
        var direction = e.target.classList.contains('slider_control_right')
            ? 'right' : 'left';
        var sliderId = e.target.classList.contains('SenderSliderControl')
            ? 'SenderSlider' : 'ReceiverSlider';
        TransformItem(direction, sliderId);
    }
}