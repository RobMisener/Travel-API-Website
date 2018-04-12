<newsearch>
    <h1>ROUTA</h1>

    <input class="searchBox" type="text" name="place" placeholder="enter a location" />

    <input class="searchButton" type="button" onclick="{search}" value="SEARCH" />

    <div id="service-helper"></div>



    <style>
    </style>


    <script>
        /*collapsable*/
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


        this.search = function () {

            const location = this.root.querySelector("input[name='place']").value;
            console.log(location);

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










<!--<places>

	<input type="text" name="location" />
	<button onclick={search}>Search</button>

	<table>
		<tr>
			<th></th>
			<th>Name</th>
			<th>Open/Closed</th>
			<th>Category</th>

		</tr>
		<tr each="{place in places}" data-id="{Id}">
			<td>
				<img src={getPhotoUrl(place)} />	
			</td>
			<td>{place.name}</td>
			<td>{isOpen(place)}</td>
			<td>{getCategoryType(place.types)}</td>
			
		</tr>
		<tr show="{newPlace}">
			<td>
				<input type="image" name="photo" placeholder="Photos" />
			</td>
			<td>
				<input type="text" name="name" placeholder="Name" />
			</td>
			<td>
				<input type="text" name="open_now" placeholder="Open/Closed" />
			</td>
			<td>
				<input type="text" name="types" placeholder="Category" />
			</td>
			<td>
				<button onclick="{save}">Save</button>
			</td>
		</tr>
	</table>
	<div id="service-helper"></div>
	<button hide="{newPlace}" onclick="{add}">Add </button>

	<script>

		this.places = [];

		this.getPhotoUrl = (place) => {
			if (place.photos[0] !== undefined) {
				return place.photos[0].getUrl({ maxWidth: 100, maxHeight: 100 });
			}
		}
		this.isOpen = (place) => {
			const bools = {
				"true": "OPEN",
				"false": "CLOSED"
			};
			// let result = '';
			// console.log(place.opening_hours);
			if (place.opening_hours.open_now !== undefined) {
				if (place.opening_hours.open_now) {
					return "OPEN";
				} else {
					return "CLOSED";
				}
			}
		}

		this.getCategoryType = (types) => {
			const mapping = {
				"airport": "Airport",
				"amusement_park": "Amusement Park",
				"aquarium": "Aquarium",
				"art_gallery": "Art Gallery",
				"bakery": "Bakery",
				"bank": "Bank",
				"bar": "Bar",
				"book_store": "Book Store",
				"bowling_alley": "Bowling Alley",
				"bus_station": "Bus Station",
				"cafe": "Cafe",
				"campground": "Campground",
				"casino": "Casino",
				"cemetery": "Cemetery",
				"church": "Church",
				"city_hall": "City Hall",
				"embassy": "Embassy",
				"fire_station": "Fire Station",
				"funeral_home": "Funeral Home",
				"gym": "Gym",
				"hindu_temple": "Hindu Temple",
				"jewelry_store": "Jewelry Store",
				"library": "Library",
				"lodging": "Lodging",
				"meal_takeaway": "Meal Takeaway",
				"mosque": "Mosque",
				"movie_theater": "Movie Theater",
				"museum": "Museum",
				"night_club": "Night Club",
				"park": "Park",
				"post_office": "Post Office",
				"restaurant": "Restaurant",
				"school": "School",
				"shopping_mall": "Shopping Mall",
				"spa": "Spa",
				"stadium": "Stadium",
				"store": "Store",
				"subway_station": "Subway Station",
				"synagogue": "Synagogue",
				"train_station": "Train Station",
				"transit_station": "Transit Station",
				"zoo": "Zoo"
			};

			let result = '';
			let appendCharacter = '';

			types.forEach(element => {
				if (mapping[element] !== undefined) {
					result += appendCharacter + mapping[element];
					appendCharacter = ', '
				}
			});

			return result;
		}

		this.search = () => {

			const location = this.root.querySelector("input[name='location']").value;
			console.log(location);

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
							console.log(results);
							this.places = results;
							this.update();


						}
					});

				}
			});

			// fetch(`https://maps.googleapis.com/maps/api/place/textsearch/json?query=\'point of interest\'+in+${location}&key=AIzaSyAnDomiUz3vcKkLHCi1YiytTZ7SHtyQuB0`, {
			//     "headers": {
			//         "Cache-Control": "no-cache",
			//     },
			//     "crossDomain": true,
			// })
			//     .then(response => response.json())
			//     .then(json => {
			//         console.log(json.results);
			//         this.places = json.results;
			//         this.update();
			//     })
		}

		this.photo = () => {
			const photoRef = photos.photo_reference;
			const photo = this.root.querySelector("input[name='photo']").value;
			console.log(photo);

			fetch(`https://maps.googleapis.com/maps/api/place/photo?maxwidth=400&photoreference=${photo}&key=AIzaSyAnDomiUz3vcKkLHCi1YiytTZ7SHtyQuB0`, {
				"headers": {
					"Cache-Control": "no-cache",
				},
				"crossDomain": true,
			})
				.then(response => response.json())
				.then(json => {
					console.log(json.results);
					this.places = json.results;
					this.update();
				})
		}

		//this.remove = function (event) {
		//	const place = event.item;

		//	const index = this.places.map(m => m.id).indexOf(place.id);

		//	this.movies.splice(index, 1);

		//	const url = 'https://maps.googleapis.com/maps/api/place/textsearch/json?query=\'point of interest\'+in+Cleveland&key=AIzaSyAnDomiUz3vcKkLHCi1YiytTZ7SHtyQuB0'
		//	const settings = {
		//		method: 'DELETE'
		//	};

		//	fetch(url, settings)
		//		.then(response => {
		//			this.update();
		//		});
		//}

		//this.add = function () {
		//	this.newPlace = {};
		//}
		//ASK JOSH ABOUT THIS
		//   this.save = function() {
		//       this.newPlace.name = this.root.querySelector('input[]')
		//  }


		const url = ``;
	</script>
</places>-->