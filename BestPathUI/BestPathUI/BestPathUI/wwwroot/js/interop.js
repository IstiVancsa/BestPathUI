
function createMap() {
    google.maps.event.addDomListener(window, 'load', initializeMap);
}

var map;

function initializeMap() {
    var mapOptions = {
        center: new google.maps.LatLng(48.209331, 16.381302),
        zoom: 15
    };
    map = new google.maps.Map(document.getElementById('map_canvas'), mapOptions);
}