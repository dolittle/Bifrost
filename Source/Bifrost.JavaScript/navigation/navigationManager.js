Bifrost.namespace("Bifrost.navigation", {
	navigationManager: {
		hookup: function(parent) {
            $("a", parent).each(function (index, item) {
				var targetUri = Bifrost.Uri.create(item.href);
				if( targetUri.isSameAsOrigin ) {
					var target = targetUri.path;
					while( target.indexOf("/") == 0 ) {
						target = target.substr(1);
					}
				
					$(this).attr("href","/"+target);
				
					$(this).bind("click", function(e) {
						e.preventDefault();
						History.pushState({},"","/"+target);
					});
				}
			});
		}
	}
});