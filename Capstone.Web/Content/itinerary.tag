<itinerary>

	<div class="itineraryContainer">
		<div class="itineraryList">
			<p class="landmarkName">{placeId}</p>
		</div>
		<button>Save</button>
		<button>Delete</button>
	</div>
	

	<script>

		this.placeId = "";
		this.opts.bus.on('placeid', data => {

			this.placeId = data.placeId;
			this.update();
		});

	</script>

</itinerary>
