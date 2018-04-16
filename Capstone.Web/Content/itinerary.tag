<itinerary>

	<div class="itineraryContainer">
		<div each={place in places} class="itineraryList">
			<p class="landmarkName">{place.name}</p>
			<input type="hidden" name="placeId" value="{place.place_id}" />
			<button onclick={remove}>Remove</button>
		</div>
		<button onclick={ save }>Save</button>
		<button>Create Itinerary</button>
		<button>Delete Itinerary</button>
	</div>
	

	<script>

		this.places = [];
		this.itinerary = {
			ItinId: 0,
			ItinName: 'Test',
			StartDate: '4/16/2018',
			Stops: []
		};

		this.opts.bus.on('addPlace', data => {
			this.places.push(data.place);
			
			this.update();
		});

		this.save = (event) => {

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

			console.log(this.itinerary);

			fetch('http://localhost:55900/api/itinerary', {
				method: 'POST',
				headers: {
					'Accept': 'application/json',
					'Content-Type': 'application/json'
				},
				body: JSON.stringify( this.itinerary )
			}).then(response => console.log(response));
			



		}

		this.remove = function (event) {
			let toBeRemoved = event.item;

			let index = this.places.map(m => m.name).indexOf(toBeRemoved.name);

			this.places.splice(index, 1);
		}

	</script>

</itinerary>
