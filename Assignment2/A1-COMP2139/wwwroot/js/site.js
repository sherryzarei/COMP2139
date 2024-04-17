// Get the flight modal
var flightModal = document.getElementById("flightModal");
// Get the car modal
var carModal = document.getElementById("carModal");
// Get the hotel modal
var hotelModal = document.getElementById("hotelModal");

// Get the buttons that open the modals
var flightBtn = document.getElementById("flightBtn");
var carBtn = document.getElementById("carBtn");
var hotelBtn = document.getElementById("hotelBtn");

// Get the <span> elements that close the modals
var closeBtns = document.getElementsByClassName("close");

// When the user clicks the flight button, open the flight modal
flightBtn.onclick = function () {
    flightModal.style.display = "block";
}

// When the user clicks the car button, open the car modal
carBtn.onclick = function () {
    carModal.style.display = "block";
}

// When the user clicks the hotel button, open the hotel modal
hotelBtn.onclick = function () {
    hotelModal.style.display = "block";
}

// When the user clicks on <span> (x) of any modal, close the modal
for (var i = 0; i < closeBtns.length; i++) {
    closeBtns[i].onclick = function () {
        this.parentElement.parentElement.style.display = "none";
    }
}

// When the user clicks anywhere outside of any modal, close it
window.onclick = function (event) {
    if (event.target == flightModal || event.target == carModal || event.target == hotelModal) {
        event.target.style.display = "none";
    }
}
