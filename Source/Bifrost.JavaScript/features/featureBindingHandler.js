if (typeof ko !== 'undefined') {
    ko.bindingHandlers.feature = {
        init: function (element, valueAccessor, allBindingAccessor, viewModel) {
        },
        update: function (element, valueAccessor, allBindingAccessor, viewModel) {
			var featureName = valueAccessor()();
			var feature = Bifrost.features.featureManager.get(featureName);
			
			$(element).empty();
			
			var container = $("<div/>");
			$(element).append(container);
			
			feature.renderTo(container[0]);
        }
    };
}
