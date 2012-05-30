Bifrost.namespace("Bifrost.navigation", {
	navigationManager: {
		hookup: function(parent) {
			var result = $("[data-navigate-to]", parent);
            $("*[data-navigate-to]", parent).each(function () {
				var target = $(this).data("navigate-to");
				
				while( target.indexOf("/") == 0 ) {
					target = target.substr(1);
				}
				
				$(this).attr("href","/"+target);
				
				$(this).bind("click", function(e) {
					e.preventDefault();
					History.pushState({},"NO TITLE AT THE MOMENT","/"+target);
				});
			});
		}
	}
});