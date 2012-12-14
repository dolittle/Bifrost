if (typeof ko !== 'undefined') {
    ko.bindingHandlers.feature = {
        init: function (element, valueAccessor, allBindingAccessor, viewModel) {
        },
        update: function (element, valueAccessor, allBindingAccessor, viewModel) {
        	var value = valueAccessor();
			var featureName = ko.utils.unwrapObservable(value);
			var feature = Bifrost.features.featureManager.get(featureName);
			
			$(element).empty();

			var container = $("<div/>").attr("data-feature", featureName);
			$(element).append(container);
			
			feature.renderTo(container[0]);
        }
    };
}
