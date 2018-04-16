<newlist>


    <div class="landmark" each={place in places}>

		<div if={checkIfOpen(place)}>
			<img class="landmarkImg" src={getPhotoUrl(place)} />
			<div class="landmarkInfo">
				<p class="landmarkName">{place.name}</p>
				<p class="landmarkAddress">{place.formatted_address}</p>
				<p class="landmarkOpen">{isOpen(place)}</p>
				<p class="landmarkCategory">{getCategoryType(place.types)}</p>
				<a target="_blank" class="googleLink" href={nameSplitting(place)}>Learn more...</a>
			</div>
			<a onclick="{ addPlace }" class="addButton" href="#">+</a>
			<hr class="breaker" />
		</div>
    </div>

    <script>
		let placeId;

		this.addPlace = (e) => {
		
			this.opts.bus.trigger('addPlace', { place: e.item.place });
		}

        this.places = [];
        // When a searchresult message arrives, look at the data attached to it
        this.opts.bus.on('searchresult', data => {
            this.places = data.results;
			this.checkBox = data.isOpen;
			this.placeId = placeId;
            this.update();
		});

		

        this.checkIfOpen = (place) => {
            // if openstatus equals false means the checkbox is unchecked.
            if (this.checkBox === false) {
                return true;
            }
            // let result = '';
            // console.log(place.opening_hours);
            if (place.opening_hours.open_now !== undefined) {
                if (place.opening_hours.open_now) {
                    return true;
                } else {
                    return false;
                }
            }
            else {
                return false;
            }
        }

        this.getPhotoUrl = (place) => {
            if (place.photos[0] !== undefined) {
                return place.photos[0].getUrl({ maxWidth: 200, maxHeight: 200 });
            }

        }

        this.getCategoryType = (types) => {
            const mapping = {
                "point of interest": "Other",
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

        this.nameSplitting = (place) => {
            var str = place.name;
            var replaced = str.split(' ').join('+');
            let url = "http://www.google.com/search?q=";
            return (url + replaced);
            console.log(replaced);



            //let link;

            //for (let i = 0; i < place.name.length-1; i++) {
            //    link = place.name.replace(' ', '+');
            //}

            //console.log(link);

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