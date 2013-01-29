Bifrost.namespace("Bifrost.read", {
	queryService: Bifrost.Singleton(function() {
		var self = this;

		this.execute = function(query) {
			var descriptor = {
	                nameOfQuery: this.name,
	                parameters: {}
	        };

	        for( var property in query ) {
	        	if( ko.isObservable(query[property]) == true ) {
	        		descriptor.parameters[property] = query[property]();
	        	}
	        }

	        var methodParameters = {
	            descriptor: JSON.stringify(descriptor)
	        };

	        $.ajax({
	            url: "/Bifrost/Query/Execute",
	            type: 'POST',
	            dataType: 'json',
	            data: JSON.stringify(methodParameters),
	            contentType: 'application/json; charset=utf-8',
	            complete: function (result) {
	                var items = $.parseJSON(result.responseText);
	                //self.allObservable(items);
	            }
	        });
    	}
	})
});