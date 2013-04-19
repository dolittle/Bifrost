Bifrost.namespace("Bifrost.views", {
	viewRenderers: Bifrost.Singleton(function() {
		var self = this;

		function getRenderers() {
			var renderers = [];
			for( var property in Bifrost.views.viewRenderers ) {
				if( Bifrost.views.viewRenderers.hasOwnProperty(property)) {
					var value = Bifrost.views.viewRenderers[property];
					if( typeof value == "function" && 
						typeof value.create == "function")  {
						var renderer = value.create();
						if( typeof renderer.canRender == "function") renderers.push(renderer);
					}
				}
			}
			return renderers;
		}

		this.canRender = function(element) {
		    var renderers = getRenderers();
		    for (var rendererIndex = 0; rendererIndex < renderers.length; rendererIndex++) {
		        var renderer = renderers[rendererIndex];
		        var result = renderer.canRender(element);
				if( result == true ) return true;
			}

			return false;
		};

		this.render = function(element) {
		    var renderers = getRenderers();
		    for (var rendererIndex = 0; rendererIndex < renderers.length; rendererIndex++) {
		        var renderer = renderers[rendererIndex];
		        if (renderer.canRender(element)) return renderer.render(element);
			}

			return null;
		};

	})
});