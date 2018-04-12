<newlist>

    <div class="landmark" each={place in places}>
        <img class="landmarkImg" src={getPhotoUrl(place)} />
		<div class="landmarkInfo">
			<p class="landmarkName">{place.name}</p>
			<p class="landmarkOpen">{isOpen(place)}</p>
			<p class="landmarkCategory">{getCategoryType(place.types)}</p>
		</div>
    </div>

    <script>

        this.places = [];

        // When a searchresult message arrives, look at the data attached to it
        this.opts.bus.on('searchresult', data => {

            this.places = data.results;
            this.update();

        });

        this.getPhotoUrl = (place) => {
            if(place.photos[0] !== undefined) {
                return place.photos[0].getUrl({maxWidth: 200, maxHeight: 200});
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
		


    </script>
</newlist>