<newmap>

    <div id="map" style="height: 300px; width: 300px;"></div>



    <script>

        this.markers = [];

        this.opts.bus.on('searchresult', data => {

            this.places = data.results;

            const map = new google.maps.Map(this.root.querySelector('#map'), {
                center: { lat: data.location.lat(), lng: data.location.lng() },
                zoom: 13
            });

            this.places.forEach(place => this.markers.push(this.createMarker(place, map)));

            this.update();

        });

        this.createMarker = function(place, map) {
            var placeLoc = place.geometry.location;
            var marker = new google.maps.Marker({
                map: map,
                position: place.geometry.location
            });

            // google.maps.event.addListener(marker, 'click', function () {
            //     infoWindow.setContent(place.name);
            //     infoWindow.open(map, this);
            // });
            return marker;
        }

        
    </script>

</newmap>



<!-- <newmap>

    

<script>
    this.opts.bus.on('searchresult', data => {
        console.log(data);

        this.places = data;
        this.update();
    });

    ///my old map code


    var map;
    var infoWindow;

    var request;
    var service;
    var markers = [];

    function initialize() {
        var center = new google.maps.LatLng(37.422, -122.084058);
        map = new google.maps.Map(document.getElementById('map'), {
            center: center,
            zoom: 13
        });
        request = {
            location: center,
            radius: 8047,
            types: ['cafe']
        };
        infoWindow = new google.maps.InfoWindow();

        service = new google.maps.places.PlacesService(map);

        service.nearbySearch(request, callback);

        google.maps.event.addListener(map, 'rightclick', function (event) {
            map.setCenter(event.latLng)
            clearResults(markers)

            var request = {
                location: event.latLng,
                radius: 8047,
                types: ['cafe']
            };
            service.nearbySearch(request, callback);
        })
    }

    function callback(results, status) {
        if (status == google.maps.places.PlacesServiceStatus.OK) {
            for (var i = 0; i < results.length; i++) {
                markers.push(createMarker(results[i]));
            }
        }
    }

    function createMarker(place) {
        var placeLoc = place.geometry.location;
        var marker = new google.maps.Marker({
            map: map,
            position: place.geometry.location
        });

        google.maps.event.addListener(marker, 'click', function () {
            infoWindow.setContent(place.name);
            infoWindow.open(map, this);
        });
        return marker;
    }

    function clearResults(markers) {
        for (var m in markers) {
            markers[m].setMap(null)
        }
        markers = []
    }
    google.maps.event.addDomListener(window, 'load', initialize);
</script>



</newmap> -->