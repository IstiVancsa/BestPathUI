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
        var defaultBounds = new google.maps.LatLngBounds(
            new google.maps.LatLng(47.224476, 27.522081),
            new google.maps.LatLng(47.086967, 27.650483));

        var options = {
            bounds: defaultBounds,
            types: ['establishment']
        }

        var input = document.getElementById('city_search');
        //map.controls[google.maps.ControlPosition.TOP_LEFT].push(input);

        cityAutoComplete = new google.maps.places.Autocomplete(input, options);

        google.maps.event.addListener(cityAutoComplete, 'place_changed', function () {
            console.log(cityAutoComplete.getPlace());
            console.log("changed");
        })
        console.log('initializeLocationAutocomplete finished');
    }
    locationAutocompleteCounter++;
}

function initializeRestaurantAutocomplete() {
    //if (locationAutocompleteCounter > 1 && locationAutocompleteCounter % 2 == 0) {
    if (restaurantAutocompleteCounter == 2) {
        console.log('initializeRestaurantAutocomplete ' + restaurantAutocompleteCounter);
        var defaultBounds = new google.maps.LatLngBounds(
            new google.maps.LatLng(47.224476, 27.522081),
            new google.maps.LatLng(47.086967, 27.650483));

        var options = {
            bounds: defaultBounds,
            types: ['restaurant']
        }

        var input = document.getElementById('restaurant_search');
        //map.controls[google.maps.ControlPosition.TOP_LEFT].push(input);

        restaurantAutoComplete = new google.maps.places.Autocomplete(input, options);
        console.log('initializeRestaurantAutocomplete finished');
    }
    restaurantAutocompleteCounter++;
}

function initializeMuseumAutocomplete() {
    //if (locationAutocompleteCounter > 1 && locationAutocompleteCounter % 2 == 0) {
    if (museumAutocompleteCounter == 2) {
        console.log('initializeMuseumAutocomplete ' + museumAutocompleteCounter);
        var defaultBounds = new google.maps.LatLngBounds(
            new google.maps.LatLng(47.224476, 27.522081),
            new google.maps.LatLng(47.086967, 27.650483));

        var options = {
            bounds: defaultBounds,
            types: ['museum']
        }

        var input = document.getElementById('museum_search');
        //map.controls[google.maps.ControlPosition.TOP_LEFT].push(input);

        museumAutoComplete = new google.maps.places.Autocomplete(input, options);
        console.log('initializeRestaurantAutocomplete finished');
    }
    museumAutocompleteCounter++;
}
