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