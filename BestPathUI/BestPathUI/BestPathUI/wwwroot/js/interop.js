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
            var place = cityAutoComplete.getPlace()
            current_lat = parseInt(place.geometry.location.lat(), 10);
            current_long = parseInt(place.geometry.location.lng(), 10);
            var lat_hidden = document.getElementById('lat_hidden');
            var lng_hidden = document.getElementById('lng_hidden');
            console.log(lat_hidden.value);
            lat_hidden.value = place.geometry.location.lat();
            lng_hidden.value = place.geometry.location.lng();
            console.log(lat_hidden.value);
            input.value = place.name;
            city_initialized = true;
        });

        console.log('initializeLocationAutocomplete finished');
    }
    locationAutocompleteCounter++;
}

//function initializeRestaurantAutocomplete() {
//    //if (locationAutocompleteCounter > 1 && locationAutocompleteCounter % 2 == 0) {
//        console.log('initializeRestaurantAutocomplete ' + restaurantAutocompleteCounter);
//        var defaultBounds = new google.maps.LatLngBounds(
//            new google.maps.LatLng(current_lat - 0.5, current_long - 0.5),
//            new google.maps.LatLng(current_lat + 0.5, current_long + 0.5));

//        var options = {
//            bounds: defaultBounds,
//            types: ['establishment']
//        }

//        var input = document.getElementById('restaurant_search');

//        restaurantAutoComplete = new google.maps.places.Autocomplete(input, options);

//        google.maps.event.addListener(restaurantAutoComplete, 'place_changed', function () {
//            var place = restaurantAutoComplete.getPlace()
//            console.log(place.geometry.viewport);
//            document.getElementById('restaurant_search').value = place.name;
//        });

//        console.log('initializeRestaurantAutocomplete finished');
//    restaurantAutocompleteCounter++;
//}

//function initializeMuseumAutocomplete() {
//    //if (locationAutocompleteCounter > 1 && locationAutocompleteCounter % 2 == 0) {
//        console.log('initializeMuseumAutocomplete ' + museumAutocompleteCounter);
//        var defaultBounds = new google.maps.LatLngBounds(
//            new google.maps.LatLng(current_lat - 0.5, current_long - 0.5),
//            new google.maps.LatLng(current_lat + 0.5, current_long + 0.5));

//        var options = {
//            bounds: defaultBounds,
//            types: ['establishment']
//        }

//        var input = document.getElementById('museum_search');

//        museumAutoComplete = new google.maps.places.Autocomplete(input, options);

//        google.maps.event.addListener(museumAutoComplete, 'place_changed', function () {
//            var place = museumAutoComplete.getPlace()
//            console.log(place.geometry.viewport);
//            document.getElementById('museum_search').value = place.name;
//        });

//        console.log('initializeRestaurantAutocomplete finished');
//    museumAutocompleteCounter++;
//}

function enableTextbox(chkId, txtId) {

    if (document.getElementById(chkId).checked)
        document.getElementById(txtId).disabled = false;
    else
        if (city_initialized == true && document.getElementById('city_search').value != '')
            document.getElementById(txtId).disabled = true;
}