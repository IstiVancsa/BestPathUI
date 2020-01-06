var map, cityAutoComplete, restaurantAutoComplete, museumAutoComplete, mapCounter = 0, locationAutocompleteCounter = 0, restaurantAutocompleteCounter = 0, museumAutocompleteCounter = 0;

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

function initializeLocationAutocomplete() {
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
            console.log(place.geometry.location.lat());
            console.log(place.geometry.location.lng());
            input.value = place.name;
        });

        console.log('initializeLocationAutocomplete finished');
    }
    locationAutocompleteCounter++;
}

function initializeRestaurantAutocomplete() {
    //if (locationAutocompleteCounter > 1 && locationAutocompleteCounter % 2 == 0) {
    if (restaurantAutocompleteCounter == 2) {
        console.log('initializeRestaurantAutocomplete ' + restaurantAutocompleteCounter);
        var defaultBounds = new google.maps.LatLngBounds(
            new google.maps.LatLng(47.124258, 27.453625),
            new google.maps.LatLng(47.166224, 27.720687));

        var options = {
            bounds: defaultBounds,
            types: ['establishment']
        }

        var input = document.getElementById('restaurant_search');

        restaurantAutoComplete = new google.maps.places.Autocomplete(input, options);

        google.maps.event.addListener(restaurantAutoComplete, 'place_changed', function () {
            var place = restaurantAutoComplete.getPlace()
            console.log(place.geometry.viewport);
            document.getElementById('restaurant_search').value = place.name;
        });

        console.log('initializeRestaurantAutocomplete finished');
    }
    restaurantAutocompleteCounter++;
}

function initializeMuseumAutocomplete() {
    //if (locationAutocompleteCounter > 1 && locationAutocompleteCounter % 2 == 0) {
    if (museumAutocompleteCounter == 2) {
        console.log('initializeMuseumAutocomplete ' + museumAutocompleteCounter);
        var defaultBounds = new google.maps.LatLngBounds(
            new google.maps.LatLng(47.124258, 27.453625),
            new google.maps.LatLng(47.166224, 27.720687));

        var options = {
            bounds: defaultBounds,
            types: ['establishment']
        }

        var input = document.getElementById('museum_search');

        museumAutoComplete = new google.maps.places.Autocomplete(input, options);

        google.maps.event.addListener(museumAutoComplete, 'place_changed', function () {
            var place = museumAutoComplete.getPlace()
            console.log(place.geometry.viewport);
            document.getElementById('museum_search').value = place.name;
        });

        console.log('initializeRestaurantAutocomplete finished');
    }
    museumAutocompleteCounter++;
}

function enableTextbox(chkId, txtId) {

    if (document.getElementById(chkId).checked)
        document.getElementById(txtId).disabled = false;
    else
        document.getElementById(txtId).disabled = true;
}