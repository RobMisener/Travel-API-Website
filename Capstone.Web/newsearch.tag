<newsearch>


    <input class="searchBox" type="text" name="place" placeholder="enter a location" />

    <input class="searchButton" type="button" onclick="{search}" value="SEARCH" />

    <div id="service-helper"></div>


    <style>
    </style>


    <script>


        this.search = function () {

            const location = this.root.querySelector("input[name='place']").value;

            const geocoder = new google.maps.Geocoder();
            const geocodeRequest = {
                address: location
            };
            // call this function when done geocoding
            geocoder.geocode(geocodeRequest, (results, status) => {
                // if the geocoding worked successfully
                if (status == google.maps.GeocoderStatus.OK) {

                    // Create a LatLng representing the coordinates
                    const latlng = new google.maps.LatLng(results[0].geometry.location.lat(), results[0].geometry.location.lng());

                    // Create a Places Request with location and a query
                    const placeRequest = {
                        location: latlng,
                        query: `point of interest in ${location}`
                    };

                    // Use the places service to send a request
                    const service = new google.maps.places.PlacesService(this.root.querySelector("#service-helper"));
                    // call this function when receiving a response
                    service.textSearch(placeRequest, (results, status) => {
                        if (status == google.maps.places.PlacesServiceStatus.OK) {
                            this.opts.bus.trigger('searchresult', { location: latlng, results: results });
                        }
                    });
                }
            });
        }
    </script>

</newsearch>
