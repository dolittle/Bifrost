Bifrost.namespace("Bifrost.views", {
	viewResolvers: Bifrost.Singleton(function() {
		var self = this;

		function getResolvers() {
			var resolvers = [];
			for( var property in Bifrost.views.viewResolvers ) {
				if( Bifrost.views.viewResolvers.hasOwnProperty(property)) {
					var value = Bifrost.views.viewResolvers[property];
					if( typeof value == "function" && 
						typeof value.create == "function")  {
						var resolver = value.create();
						if( typeof resolver.canResolve == "function") resolvers.push(resolver);
					}
				}
			}
			return resolvers;
		}

		this.canResolve = function(element) {
			var resolvers = getResolvers();
			for( var resolverIndex=0; resolverIndex<resolvers.length; resolverIndex++ ) {
				var resolver = resolvers[resolverIndex];
				var result = resolver.canResolve(element);
				if( result == true ) return true;
			}

			return false;
		};

		this.resolve = function(element) {
			var resolvers = getResolvers();
			for( var resolverIndex=0; resolverIndex<resolvers.length; resolverIndex++ ) {
				var resolver = resolvers[resolverIndex];
				if( resolver.canResolve(element)) return resolver.resolve(element);
			}

			return null;
		};

	})
});