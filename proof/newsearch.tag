<newsearch>


        <input type="text" name="place" placeholder="enter a location" />
    
        <input type="button" onclick="{search}" value="SEARCH" />
    
    
        <style>
    
    
        </style>
    
    
        <script>
    
    
            this.search = function() { 
    
                // Get the movie title they are searching for
                const title = this.root.querySelector('input[type=text]').value;
                //console.log(searchTitle);
    
                // Build the URL to send our Fetch Request to
                const url = `https://maps.googleapis.com/maps/api/place/textsearch/json?query=\'point of interest\'+in+${location}&key=AIzaSyAnDomiUz3vcKkLHCi1YiytTZ7SHtyQuB0`
    
                // Call the API
                fetch(url)
                    .then(response => response.json())
                    .then(json => this.opts.bus.trigger('searchresult', json.Search));
    
                    // log the json
    
            }
    
        </script>
    
</newsearch>