<newlist>

    <div each={place in places}>
        <img src={getPhotoUrl(place)} />
        <p>{name}</p>
    </div>

<div class="thumbsUp">
        <i onclick="myFunction(this)" class="fa fa-thumbs-up"></i>



</div>
<div class="thumbsDown">
        <i onclick="myFunction(this)" class="fa fa-thumbs-down"></i>
</div>

    <style>
        img {
            display: block;
            height: 200px;
        }
    </style>



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

    </script>
</newlist>