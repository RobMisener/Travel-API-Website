<itinerary>

	<div class="itineraryContainer">
		<div each={place in places} class="itineraryList">
			<p class="landmarkName">{place}</p>
			<input type="hidden" name="placeId" value="{place}" />
		</div>
		<button>Save</button>
		<button>Delete</button>
	</div>
	

	<script>

		this.places = [];
		this.opts.bus.on('placeid', data => {
			this.places.push(data.placeId);
			this.update();
		});

	</script>

</itinerary>
