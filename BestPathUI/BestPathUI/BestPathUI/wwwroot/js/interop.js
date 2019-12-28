
function createMap() {
    alert("Hello ");
    google.maps.event.addDomListener(window, 'load', initialize);
}

function initialize() {
    var mapOptions = {
        center: new google.maps.LatLng(48.209331, 16.381302),
        zoom: 15
    };
    map = new google.maps.Map(document.getElementById("map-canvas"),
        mapOptions);
}

function setElementTextById(id, text) {
    document.getElementById(id).innerText = text;
}