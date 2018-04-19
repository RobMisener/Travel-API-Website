<newsearch>
    <a class="home" href="/"><h1>ROUTA</h1></a>

	<input class="searchBox" type="text" name="place" placeholder="enter a city" />

	<input id="enter" class="searchButton" type="button" onclick="{search}" value="SEARCH" />

	<div id="service-helper"></div>

	<div class="advanceSearchContainer">
		<button onclick="{collapse}" class="collapsible">+ ADVANCED SETTINGS</button>

		<div class="advanceSearch">
			<p>CATEGORY:</p>
			<select class="categories" name="category">
				<option value="point of interest">Other</option>
				<option value="airport">Airport</option>
				<option value="amusement_park">Amusement Park</option>
				<option value="aquarium">Aquarium</option>
				<option value="art_gallery">Art Gallery</option>
				<option value="bakery">Bakery</option>
				<option value="bank">Bank</option>
				<option value="bar">Bar</option>
				<option value="book_store">Book Store</option>
				<option value="bowling_alley">Bowling Alley</option>
				<option value="bus_station">Bus Station</option>
				<option value="cafe">Cafe</option>
				<option value="campground">Campground</option>
				<option value="casino">Casino</option>
				<option value="cemetery">Cemetery</option>
				<option value="church">Church</option>
				<option value="city_hall">City Hall</option>
				<option value="embassy">Embassy</option>
				<option value="fire_station">Fire Station</option>
				<option value="funeral_home">Funeral Home</option>
				<option value="gym">Gym</option>
				<option value="hindu_temple">Hindu Temple</option>
				<option value="jewelry_store">Jewelry Store</option>
				<option value="library">Library</option>
				<option value="lodging">Lodging</option>
				<option value="meal_takeaway">Meal Takeaway</option>
				<option value="mosque">Mosque</option>
				<option value="movie_theaher">Movie Theater</option>
				<option value="museum">Museum</option>
				<option value="night_club">Night Club</option>
				<option value="park">Park</option>
				<option value="post_office">Post Office</option>
				<option value="restaurant">Restaurant</option>
				<option value="school">School</option>
				<option value="shopping_mall">Shopping Mall</option>
				<option value="spa">Spa</option>
				<option value="stadium">Stadium</option>
				<option value="store">Store</option>
				<option value="subway_station">Subway Station</option>
				<option value="synagogue">Synagogue</option>
				<option value="train_station">Train Station</option>
				<option value="transit_station">Transit Station</option>
				<option value="zoo">Zoo</option>
			</select>
			<input id="openNow_Checkbox" type="checkbox" name="" value="Bike" checked="checked">OPEN NOW
		</div>
	</div>
	

	<script>

		var open = true;


		/*collapsable*/
		this.collapse = function () {
			var coll = document.getElementsByClassName("collapsible");
			var i;

			for (i = 0; i < coll.length; i++) {
				coll[i].addEventListener("click", function () {
					this.classList.toggle("active");
					var content = this.nextElementSibling;
					if (content.style.display === "block") {
						content.style.display = "none";
					} else {
						content.style.display = "block";
					}
				});
			}
		}


		this.search = function () {

			const location = this.root.querySelector("input[name='place']").value;
			//boolIsOpen = true;
			var checkbox = document.querySelector('#openNow_Checkbox');
			if (checkbox.checked) {
				boolIsOpen = true;
			}
			else {
				boolIsOpen = false;
			}

			const category = this.root.querySelector("select[name='category']").value;

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
	
					let placeRequest = {};
					if (category) {
						placeRequest = {
							location: latlng,
							query: `${category} in ${location}`
						};
					}
					else {
						 placeRequest = {
							location: latlng,
							query: `point of interest in ${location}`
						}
					}

					// Use the places service to send a request
					const service = new google.maps.places.PlacesService(this.root.querySelector("#service-helper"));
					// call this function when receiving a response
					service.textSearch(placeRequest, (results, status) => {
						if (status == google.maps.places.PlacesServiceStatus.OK) {
							this.opts.bus.trigger('searchresult', { location: latlng, results: results, isOpen: boolIsOpen });
						}
					});
				}
			});
		}
	</script>

</newsearch>


