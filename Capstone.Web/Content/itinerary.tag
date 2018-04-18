﻿<itinerary>

    <div class="itineraryContainer">
        <form action="" method="post">
            <input class="itineraryName" value="{itinerary.ItinName}" type="text" placeholder="Itinerary Name" name="ItinName" />
            <input class="itineraryDate" value="{itinerary.StartDate}" type="date" placeholder="Itinerary Date" name="itineraryDate" />
        </form>
        <button class="saveButton" onclick={ save }>Save Itinerary</button>
        <button class="deleteButton">Delete Itinerary</button>
		<p class="hide" id="savedConfirm">Saved!</p>
		<p class="hide" id="deleteConfirm">Deleted Succesfully!</p>

        <div each={stop in itinerary.Stops} class="itineraryList">
            <p class="landmarkName">{stop.Name}</p>
            <input type="hidden" name="placeId" value="{stop.PlaceId}" />
            <button class="removeButton" onclick={remove}>Remove</button>
        </div>

    </div>



    <script>


        this.itinerary = {
            ItinId: 0,
            ItinName: "ItinName",
            StartDate: '4/16/2018',
            Stops: []
		};

		this.on("mount", () => {
			if (this.opts.id != undefined) {
				fetch(`http://localhost:55900/api/itinerary/${this.opts.id}`, {
					method: 'GET',
					headers: {
						'Accept': 'application/json',
						'Content-Type': 'application/json'
					},
					credentials: 'include',
				}).then(response => response.json())
					.then(json => {
						this.itinerary = json;
						this.itinerary.StartDate = new Date(this.itinerary.StartDate).yyyymmdd();
						this.update();
					});
			}
		})
		


		this.opts.bus.on('addPlace', data => {

			const stop = {
				PlaceId: data.place.place_id,
				Name: data.place.name,
				Address: data.place.formatted_address,
				Category: data.place.types[0],
				Latitude: data.place.geometry.location.lat(),
				Longitude: data.place.geometry.location.lng()


			};


            this.itinerary.Stops.push(stop);
            this.update();
        });

		this.save = (event) => {

			let saved = document.getElementById("savedConfirm");
			saved.classList.toggle("hide");

            for (let i = 0; i < this.itinerary.Stops.length; i++) {

				this.itinerary.Stops[i].Order = i + 1;
            }
            this.itinerary.ItinName = document.querySelector("input[name=ItinName]").value;
            this.itinerary.StartDate = document.querySelector("input[name=itineraryDate]").value;

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

            let index = this.itinerary.Stops.map(m => m.name).indexOf(toBeRemoved.Name);

            this.itinerary.Stops.splice(index, 1);
		}


		Date.prototype.yyyymmdd = function () {

			var yyyy = this.getFullYear().toString();
			var mm = (this.getMonth() + 1).toString(); // getMonth() is zero-based         
			var dd = this.getDate().toString();

			return yyyy + '-' + (mm[1] ? mm : "0" + mm[0]) + '-' + (dd[1] ? dd : "0" + dd[0]);
		};

    </script>

</itinerary>
