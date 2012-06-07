Bifrost.namespace("Bifrost.navigation", {
	navigationManager: {
		hookup: function(parent) {
            $("a", parent).each(function (index, item) {
				var href = item.href;
				if( href.length == 0 ) {
					href = "/";
				}
				var targetUri = Bifrost.Uri.create(href);
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