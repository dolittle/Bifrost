Bifrost.namespace("Bifrost.features");
(function ($) {
	
	$(function() {
		$("*[data-feature]").each(function() {
			var target = $(this);
			var feature = $(this).attr("data-feature");
			var isAdministration = $(this).attr("data-admin") != undefined ? true : false;

			var container = feature;
			var name = feature;
			var root = Bifrost.features;
			
			if( feature.indexOf('/') > 0 ) {
				var elements = feature.split('/');
				container = elements[0];
				name = elements[1];
				root = Bifrost.features.addOrGetContainer(container);
			}
			
			root.addFeature(isAdministration, name, function(f) {
				f.renderTo(target[0]);
			});
		});
	});
})(jQuery);


