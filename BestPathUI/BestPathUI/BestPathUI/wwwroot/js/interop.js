function createAlert() {
    alert("Buna!");
}

function setElementTextById(id, text) {
    document.getElementById(id).innerText = text;
}
 <script type="text/javascript"
    src="https://maps.googleapis.com/maps/api/js?key=AIzaSyAIztt-WDuygbYykfzV8akJ3DyR-jrhJRY">
</script>

<script>
    var map;
function initialize() {
    var mapOptions = {
        center: new google.maps.LatLng(48.209331, 16.381302),
        zoom: 15
    };
    map = new google.maps.Map(document.getElementById("map-canvas"),
        mapOptions);
}
google.maps.event.addDomListener(window, 'load', initialize);

    </script>


