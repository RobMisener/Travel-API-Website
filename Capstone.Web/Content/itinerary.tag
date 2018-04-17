<itinerary>

    <div class="itineraryContainer">
        <form action="" method="post">
            <input class="itineraryName" value="{itinerary.ItinName}" type="text" placeholder="Itinerary Name" name="ItinName" />
            <input class="itineraryDate" type="text" placeholder="Itinerary Date" name="itineraryDate" />
        </form>
        <button class="saveButton" onclick={ save }>Save Itinerary</button>
        <button class="deleteButton">Delete Itinerary</button>

        <div each={place in places} class="itineraryList">
            <p class="landmarkName">{place.name}</p>
            <input type="hidden" name="placeId" value="{place.place_id}" />
            <button class="removeButton" onclick={remove}>Remove</button>
        </div>

    </div>



    <script>


        this.places = [];
        this.itinerary = {
            ItinId: 0,
            ItinName: "ItinName",
            StartDate: '4/16/2018',
            Stops: []
        };

        this.opts.bus.on('addPlace', data => {
            this.places.push(data.place);
            this.update();
        });

        this.save = (event) => {

            this.itinerary.stops = [];

            for (let i = 0; i < this.places.length; i++) {
                const place = this.places[i];

                this.itinerary.Stops.push({
                    PlaceId: place.place_id,
                    Order: i + 1,
                    Name: place.name,
                    Address: place.formatted_address,
                    Category: place.types[0],
                    Latitude: place.geometry.location.lat(),
                    Longitude: place.geometry.location.lng()
                });

            }
            this.itinerary.ItinName = document.querySelector("input[name=ItinName]").value;
            this.itinerary.StartDate = document.querySelector("input[name=itineraryDate]").value;
            console.log(this.itinerary);

            fetch('http://localhost:55900/api/itinerary', {
                method: 'POST',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                },
                credentials: 'include',
                body: JSON.stringify(this.itinerary)
            }).then(response => response.json())
                .then(json => {
                    this.itinerary.ItinId = json.ItinId
                });

        }

        this.remove = function (event) {
            let toBeRemoved = event.item;

            let index = this.places.map(m => m.name).indexOf(toBeRemoved.place.name);

            this.places.splice(index, 1);
        }

    </script>

</itinerary>
