<newmap class="map">

    <div id="map" style="height: 300px; width: 300px;"></div>



    <script>
        var map;
        var infoWindow;
        var service;
        

        this.markers = [];

        this.opts.bus.on('searchresult', data => {

            this.places = data.results;

            const map = new google.maps.Map(this.root.querySelector('#map'), {
                center: { lat: data.location.lat(), lng: data.location.lng() },
                zoom: 13
            });

            infoWindow = new google.maps.InfoWindow({
                content: name
            });

            //service = new google.maps.places.PlacesService(map);

            //service.nearbySearch(request, callback);

            this.places.forEach(place => this.markers.push(this.createMarker(place, map)));

            this.update();

        });

        function callback(results, status) {
            if (status == google.maps.places.PlacesServiceStatus.OK) {
                for (var i = 0; i < results.length; i++) {
                    markers.push(createMarker(results[i]));
                }
            }
        }

        this.createMarker = function (place, map) {
            var name = '<div id="content"><strong>' + place.name + '</strong><p>' + place.formatted_address + '</p><p>' + place. + '</p></div>';
          

            var placeLoc = place.geometry.location;
            var marker = new google.maps.Marker({
                map: map,
                position: place.geometry.location
            });

            google.maps.event.addListener(marker, 'click', function () {
              infoWindow.setContent(name);
              infoWindow.open(map, this);
           });
            return marker;
        }


    </script>

</newmap>