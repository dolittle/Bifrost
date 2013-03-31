Bifrost.namespace("Bifrost.views", {
    viewBindingHandler: Bifrost.Type.extend(function(viewManager) {
        var self = this;

        this.viewManager = viewManager;

        this.init = function (element, valueAccessor, allBindingAccessor, viewModel) {
        };
        this.update = function(element, valueAccessor, allBindingAccessor, viewModel) {
            var path = ko.utils.unwrapObservable(valueAccessor());
            self.render(element, path);
        };

        this.render = function(element, path) {
            $(element).data("view", path);
            self.viewManager.render(element).continueWith(function (view) {
                promise.signal(view);
            });
        }
    })
});
ko.bindingHandlers.view = Bifrost.views.viewBindingHandler.create();
