<newlist>

        <div each={places}>
            <img src={getPhotoUrl(place)} />
            <p>{name}</p>
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
            console.log(data);
        
            this.places = data;
            this.update();
        });
        
        </script>
        </newlist>