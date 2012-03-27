(function() {
	if(typeof History === "undefined" || typeof History.Adapter === "undefined") return;
	
	var container = $("[data-navigation-container]")[0];

	History.Adapter.bind(window,"statechange", function() {
		var state = History.getState();
		var featureName = state.data.feature;

		$(container).html("");
		var feature = Bifrost.features.featureManager.get(featureName);
		feature.renderTo(container);
	});

	$(function () {
		var state = History.getState();
		var hash = Bifrost.hashString.decode(state.hash);
		var featureName = hash.feature;
		if( typeof featureName !== "undefined") {
			$(window).trigger("statechange");
		} else {
			var optionString = $(container).data("navigation-container");
			var optionsDictionary = ko.jsonExpressionRewriting.parseObjectLiteral(optionString);
			$.each(optionsDictionary, function(index, item) {
				if( item.key === "default") {
					var feature = Bifrost.features.featureManager.get(item.value);
					feature.renderTo(container);
					return;
				}
			});
		}
	});
})();