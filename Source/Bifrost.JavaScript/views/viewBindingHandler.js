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
            

            $(element).empty();

            var container = $("<div/>");
            $(container).data("view", path);
            $(element).append(container);

            self.viewManager.render(container[0]).continueWith(function (view) {
                promise.signal(view);
            });
        }
    })
});
ko.bindingHandlers.view = Bifrost.views.viewBindingHandler.create();
