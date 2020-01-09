var map, cityAutoComplete, mapCounter = 0, locationAutocompleteCounter = 0;
var city_initialized = false, current_lat, current_long;

function createMap() {
    google.maps.event.addDomListener(window, 'load', initializeMap);
}

function createLocationAutocomplete() {
    if (locationAutocompleteCounter == 0) {
        console.log("create autocomplete");
        google.maps.event.addDomListener(window, 'load', initializeLocationAutocomplete);
        console.log('create autocomplete finished');
    }
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
}

function initializeAutocompletes() {
    //if (locationAutocompleteCounter > 1 && locationAutocompleteCounter % 2 == 0) {
    if (locationAutocompleteCounter == 2) {
        console.log('initializeLocationAutocomplete ' + locationAutocompleteCounter);

        var options = {
            types: ['establishment']
        }

        var input = document.getElementById('city_search');

        cityAutoComplete = new google.maps.places.Autocomplete(input, options);

        google.maps.event.addListener(cityAutoComplete, 'place_changed', function () {
            var place = cityAutoComplete.getPlace();

            current_lat = parseFloat(place.geometry.location.lat(), 10);
            current_long = parseFloat(place.geometry.location.lng(), 10);

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
    locationAutocompleteCounter++;
}

function enableTextbox(chkId, txtId) {
    if (document.getElementById(chkId).checked) {
        if (city_initialized == true && document.getElementById('city_search').value != '')
            document.getElementById(txtId).disabled = false;
    }
    else
        document.getElementById(txtId).disabled = true;
}