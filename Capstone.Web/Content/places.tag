<places>

	<input type="text" name="location" />
	<button onclick={search}>Search</button>

	<table>
		<tr>
			<th></th>
			<th>Name</th>
			<th>Open Now</th>
			<th>Category</th>

		</tr>
		<tr each="{places}" data-id="{Id}">
			<td>
				<img src="https://maps.googleapis.com/maps/api/place/photo?maxwidth=400&photoreference={photos[0].photo_reference}&key=AIzaSyAnDomiUz3vcKkLHCi1YiytTZ7SHtyQuB0" />
			</td>
			<td>{name}</td>
			<td>{open_now}</td>
			<td>{getCategoryType(types)}</td>
			<td>
				<button onclick="{remove}">Delete</button>
			</td>
		</tr>
		<tr show="{newPlace}">
			<td>
				<input type="image" name="photo" placeholder="Photos" />
			</td>
			<td>
				<input type="text" name="name" placeholder="Name" />
			</td>
			<td>
				<input type="text" name="opening_hours" placeholder="Opening Hours" />
			</td>
			<td>
				<input type="text" name="types" placeholder="Category" />
			</td>
			<td>
				<button onclick="{save}">Save</button>
			</td>
		</tr>
	</table>

	<button hide="{newPlace}" onclick="{add}">Add </button>

	<script>

		this.places = [];


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
				"point_of_interest": "Point of Interest",
				"post_office": "Post Office",
				"restaurant": "Restaurant",
				"school": "School",
				"shopping_mall": "Shopping Mall",
				"spa": "Spa",
				"staduim": "Stadium",
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

			fetch(`https://maps.googleapis.com/maps/api/place/textsearch/json?query=\'point of interest\'+in+${location}&key=AIzaSyAnDomiUz3vcKkLHCi1YiytTZ7SHtyQuB0`, {
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

		this.remove = function (event) {
			const place = event.item;

			const index = this.places.map(m => m.id).indexOf(place.id);

			this.movies.splice(index, 1);

			const url = 'https://maps.googleapis.com/maps/api/place/textsearch/json?query=\'point of interest\'+in+Cleveland&key= AIzaSyAnDomiUz3vcKkLHCi1YiytTZ7SHtyQuB0'
			const settings = {
				method: 'DELETE'
			};

			fetch(url, settings)
				.then(response => {
					this.update();
				});
		}

		this.add = function () {
			this.newPlace = {};
		}
		//ASK JOSH ABOUT THIS
		//   this.save = function() {
		//       this.newPlace.name = this.root.querySelector('input[]')
		//  }

		const url = ``;
	</script>
</places>