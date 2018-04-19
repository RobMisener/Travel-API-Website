<itinerary>

    <div class="itineraryContainer">
        <form action="" method="post">
            <input class="itineraryName" value="{itinerary.ItinName}" type="text" placeholder="city name" name="ItinName" />
            <input class="itineraryDate" value="{itinerary.StartDate}" type="date" placeholder="date of visit" name="itineraryDate" />
            <input type="hidden" value="{itinerary.ItinId}" name="itinId" />
        </form>

        <button class="saveButton" onclick={ save }>Save Itinerary</button>
        <button class="deleteButton" onclick={ delete }>Delete Itinerary</button>
        <p class="hide" id="savedConfirm">Saved!</p>
        <p class="hide" id="deleteConfirm">Deleted Succesfully!</p>

        <div id="sortable">
            <div each={stop, index in itinerary.Stops} class="itineraryList">
                <input name="position" type="hidden" value="{index}" />
                <p class="landmarkName">{stop.Name}</p>
                <p><img class="landmarkImg" src={stop.Image}/></p>
                <input type="hidden" name="placeId" value="{stop.PlaceId}" />
                <button class="removeButton" onclick={ remove }>Remove</button>
            </div>
        </div>
    </div>


    <script>

		this.getPhotoUrl = (place) => {
			if (place.photos[0] !== undefined) {
				return place.photos[0].getUrl({ maxWidth: 200, maxHeight: 200 });
			}
		}

        this.itinerary = {
            ItinId: 0,
            ItinName: "city name",
            StartDate: '4/16/2018',
            Stops: []
        };




        this.on("mount", () => {

            $("#sortable").sortable({
                update: (event, ui) => {
                    // Create a new array temp
                    const temp = [];

                    // Loop through the DOM elements
                    const inputs = document.querySelectorAll("input[name=position]")
                    // For each element
                    for (let i = 0; i < inputs.length; i++) {
                        // find its associated stop in the stops aray
                        const newIndex = i;
                        const oldIndex = inputs[i].value;


                        temp[newIndex] = this.itinerary.Stops[oldIndex];
                        // Add it to the temp array
                    }

                    // Assign temp to the itinerary stops
                    this.itinerary.Stops = temp;
                }
            });
            $("#sortable").disableSelection();


            if (this.opts.id != undefined) {
                fetch(`http://localhost:55900/api/itinerary/${this.opts.id}`, {
                    method: 'GET',
                    headers: {
                        'Accept': 'application/json',
                        'Content-Type': 'application/json'
                    },
                    credentials: 'include',
                })
                    .then(response => response.json())
                    .then(json => {
                        this.itinerary = json;
                        this.itinerary.StartDate = new Date(this.itinerary.StartDate).yyyymmdd();



                        this.update();                        
                    });
            }
        })

        this.opts.bus.on('addPlace', data => {
            let photoUrl;
           
                if (data.place.photos != undefined) {
                    photoUrl = data.place.photos[0].getUrl({ maxWidth: 200, maxHeight: 200 });
                    
                } else {
                    photoUrl = "http://localhost:55900/Content/img/default_activity_image.jpg";
                   
                }

            
               console.log(data.place.photos);

            const stop = {
                PlaceId: data.place.place_id,
                Name: data.place.name,
                Address: data.place.formatted_address,
                Category: data.place.types[0],
                Latitude: data.place.geometry.location.lat(),
                Longitude: data.place.geometry.location.lng(),
				Image: photoUrl
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

        this.delete = (event) => {
            let deleted = document.getElementById("deleteConfirm");
            deleted.classList.toggle("hide");

            this.itinerary.ItinId = document.querySelector("input[name=itinId]").value;
            

            fetch(`http://localhost:55900/api/itinerary/${this.itinerary.ItinId}`, {
                method: 'POST',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                },
                credentials: 'include',
                body: JSON.stringify(this.itinerary.ItinId)
            }).then(response => response.json())
                .then(json => {
                    this.itinerary.ItinId = json.ItinId
                });
            setTimeout(function () {
                window.location.href = ("http://localhost:55900/Manage") //will redirect to your blog page (an ex: blog.html)
            }, 2000);

            setTimeout(function () {
                window.location.href = ("http://localhost:55900/Manage") //will redirect to your blog page (an ex: blog.html)
            }, 2000);

        }

        

        this.remove = function (event) {
            let toBeRemoved = event.item;


            let index = this.itinerary.Stops.map(m => m.Name).indexOf(toBeRemoved.stop.Name);

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
