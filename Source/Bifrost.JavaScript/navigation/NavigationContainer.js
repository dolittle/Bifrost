Bifrost.namespace("Bifrost.navigation");
Bifrost.navigation.NavigationContainer = (function() {
	function NavigationContainer() {
	}
	
	return {
		createForElement: function(element) {
			var container = new NavigationContainer();
			element.navigation = container;
			$(element).children().empty();
			return container;
		}
	}
})();

$(function() {
	var containers = $("[data-navigation]");
	$.each(containers, function(index, element) {
		Bifrost.navigation.NavigationContainer.createForElement(element);
	});
});

