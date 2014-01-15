Bifrost.namespace("Bifrost");
Bifrost.Uri = (function(window, undefined) {
	/* parseUri JS v0.1, by Steven Levithan (http://badassery.blogspot.com)
	Splits any well-formed URI into the following parts (all are optional):
	----------------------
	• source (since the exec() method returns backreference 0 [i.e., the entire match] as key 0, we might as well use it)
	• protocol (scheme)
	• authority (includes both the domain and port)
	    • domain (part of the authority; can be an IP address)
	    • port (part of the authority)
	• path (includes both the directory path and filename)
	    • directoryPath (part of the path; supports directories with periods, and without a trailing backslash)
	    • fileName (part of the path)
	• query (does not include the leading question mark)
	• anchor (fragment)
	*/
	function parseUri(sourceUri){
	    var uriPartNames = ["source","protocol","authority","domain","port","path","directoryPath","fileName","query","anchor"];
	    var uriParts = new RegExp("^(?:([^:/?#.]+):)?(?://)?(([^:/?#]*)(?::(\\d*))?)?((/(?:[^?#](?![^?#/]*\\.[^?#/.]+(?:[\\?#]|$)))*/?)?([^?#/]*))?(?:\\?([^#]*))?(?:#(.*))?").exec(sourceUri);
	    var uri = {};

	    for(var i = 0; i < 10; i++){
	        uri[uriPartNames[i]] = (uriParts[i] ? uriParts[i] : "");
	    }

	    // Always end directoryPath with a trailing backslash if a path was present in the source URI
	    // Note that a trailing backslash is NOT automatically inserted within or appended to the "path" key
	    if(uri.directoryPath.length > 0){
	        uri.directoryPath = uri.directoryPath.replace(/\/?$/, "/");
	    }

	    return uri;
	}	
	
	
	function Uri(location) {
		var self = this;
		this.setLocation = function(location) {
			self.fullPath = location;
			location = location.replace("#!","/");
		
			var result = parseUri(location);
		
			if( !result.protocol || typeof result.protocol == "undefined" ) {
				throw new Bifrost.InvalidUriFormat("Uri ('"+location+"') was in the wrong format");
			}

			self.scheme = result.protocol;
			self.host = result.domain;
			self.path = result.path;
			self.anchor = result.anchor;

			self.queryString = result.query;
			self.port = parseInt(result.port);
			self.parameters = Bifrost.hashString.decode(result.query);
			self.parameters = Bifrost.extend(Bifrost.hashString.decode(result.anchor), self.parameters);
			
			self.isSameAsOrigin = (window.location.protocol == result.protocol+":" &&
				window.location.hostname == self.host); 
		}
		
		this.setLocation(location);
	}
	
	function throwIfLocationNotSpecified(location) {
		if( !location || typeof location == "undefined" ) throw new Bifrost.LocationNotSpecified();
	}
	
	
	return {
		create: function(location) {
			throwIfLocationNotSpecified(location);
		
			var uri = new Uri(location);
			return uri;
		},

		isAbsolute: function (url) {
		    // Based on http://stackoverflow.com/questions/10687099/how-to-test-if-a-url-string-is-absolute-or-relative
		    var expression = new RegExp('^(?:[a-z]+:)?//', 'i');
		    return expression.test(url);
		}
	};
})(window);