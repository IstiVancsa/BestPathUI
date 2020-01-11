var map, cityAutoComplete, mapCounter = 0;
var city_initialized = false;

function createMap() {
    google.maps.event.addDomListener(window, 'load', initializeMap);
}

function createLocationAutocomplete() {
    console.log("create autocomplete");
    google.maps.event.addDomListener(window, 'load', initializeLocationAutocomplete);
    console.log('create autocomplete finished');
}

function initializeMap() {

    if (mapCounter > 0) {
        var mapOptions = {
            center: new google.maps.LatLng(47.151726, 27.587914),
            zoom: 15
        };
        map = new google.maps.Map(document.getElementById('map_canvas'), mapOptions);
        var marker = new google.maps.Marker({ position: new google.maps.LatLng(47.151726, 27.587914), map: map });
    }
    mapCounter++;

    var directionsService = new google.maps.DirectionsService();
    var directionsRenderer = new google.maps.DirectionsRenderer({
        draggable: true,
        map: map,
        panel: document.getElementById('right-panel')
    });

    calculateAndDisplayRoute(new google.maps.LatLng(47.151726, 27.587914),
        new google.maps.LatLng(46.563195, 26.909888),
        directionsService,
        directionsRenderer,
        [new google.maps.LatLng(47.1673674, 27.578843), new google.maps.LatLng(47.2088961, 26.988773)])
}

function initializeAutocompletes() {
    console.log('initializeLocationAutocomplete started');

    var options = {
        types: ['establishment']
    }

    var input = document.getElementById('city_search');
    cityAutoComplete = new google.maps.places.Autocomplete(input, options);

    google.maps.event.addListener(cityAutoComplete, 'place_changed', function () {
        var place = cityAutoComplete.getPlace();

        var current_lat = parseFloat(place.geometry.location.lat(), 10);
        var current_long = parseFloat(place.geometry.location.lng(), 10);

        var lat_hidden = document.getElementById('lat_hidden');
        var lng_hidden = document.getElementById('lng_hidden');

        lat_hidden.value = current_lat;
        lng_hidden.value = current_long;

        var location = {
            lat: current_lat,
            lng: current_long
        };

        DotNet.invokeMethodAsync('BestPathUI', 'SetLocation', location);

        input.value = place.name;
        city_initialized = true;
    });

    console.log('initializeLocationAutocomplete finished');
}

function enableTextbox(chkId, txtId) {
    if (document.getElementById(chkId).checked) {
        if (city_initialized == true && document.getElementById('city_search').value != '')
            document.getElementById(txtId).disabled = false;
    }
    else
        document.getElementById(txtId).disabled = true;
}

function calculateAndDisplayRoute(origin, destination, service, display, waypts) {
    var wp = []

    for (var i = 0; i < waypts.length; i++) {
        wp.push({
            location: waypts[i]
        })
        console.log(wp[i])
    }

    service.route({
        origin: origin,
        destination: destination,
        waypoints: wp,
        optimizeWaypoints: true,
        travelMode: 'DRIVING'
    }, function (response, status) {
        if (status == "OK") {
            display.setDirections(response);
        } else {
            window.alert('Directions request failed due to ' + status);
        }
    })
}

