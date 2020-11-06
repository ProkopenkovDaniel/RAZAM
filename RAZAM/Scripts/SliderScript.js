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
setTimeout(function () {
    window.location.reload();
    }, 30000);
//the main method
window.addEventListener("load", function () {
    var leftSenderButton = document.getElementById("leftSenderButton");
    var rightSenderButton = document.getElementById("rightSenderButton");
    var rightReceiverButton = document.getElementById("rightReceiverButton");
    var leftReceiverButton = document.getElementById("leftReceiverButton");
    leftSenderButton.addEventListener('click', controlClick);
    rightSenderButton.addEventListener('click', controlClick);
    leftReceiverButton.addEventListener('click', controlClick);
    rightReceiverButton.addEventListener('click', controlClick);
});

window.addEventListener("load", function () {
    var sliderItemsStyle = document.querySelectorAll('.slider_item');
    CheckNotesStatus(sliderItemsStyle)
});

function CheckNotesStatus(sliderItemsStyle) {
    sliderItemsStyle.forEach(function (item, index) {
        var card = item.querySelector('.card');
        var cardBody = card.querySelector('.card-body');
        var inputStatus = cardBody.querySelector("#Status");
        var status = inputStatus.value;
        if (status != null) {
            card.classList.add(status);
        }
    });
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