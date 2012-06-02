Bifrost.namespace("Bifrost.navigation", {
	navigationManager: {
		hookup: function(parent) {
            $("a", parent).each(function (index, item) {
				var target = item.href.replace("file://","");
				if( target.indexOf("/") === 0 ) {
					while( target.indexOf("/") == 0 ) {
						target = target.substr(1);
					}
				
					$(this).attr("href","/"+target);
				
					$(this).bind("click", function(e) {
						e.preventDefault();
						History.pushState({},"NO TITLE AT THE MOMENT","/"+target);
					});
				}
			});
		}
	}
});