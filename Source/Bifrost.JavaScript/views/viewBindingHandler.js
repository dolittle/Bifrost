Bifrost.namespace("Bifrost.views", {
    viewBindingHandler: Bifrost.Type.extend(function (viewRenderers) {
        var self = this;
        this.viewRenderers = viewRenderers;

        this.init = function (element, valueAccessor, allBindingAccessor, parentViewModel, bindingContext) {
        };
        this.update = function (element, valueAccessor, allBindingAccessor, parentViewModel, bindingContext) {
            var uri = ko.utils.unwrapObservable(valueAccessor());
            
            $(element).data("view", uri);

            viewRenderers.render(element);
        };
    })
});
Bifrost.views.viewBindingHandler.initialize = function () {
    ko.bindingHandlers.view = Bifrost.views.viewBindingHandler.create();
};

