if (typeof ko !== 'undefined') {
    ko.bindingHandlers.navigateTo = {
        init: function (element, valueAccessor, allBindingAccessor, viewModel) {
            ko.applyBindingsToNode(element, { 
				click: function() {
					var featureName = valueAccessor()();
					History.pushState({feature:featureName},$(element).attr("title"),"?feature="+featureName);
				} 
			}, viewModel);
        }
    };
}
