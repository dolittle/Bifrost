Bifrost.namespace("Bifrost.views", {
    View: Bifrost.Type.extend(function (viewLoader, viewModelManager, viewManager) {
        var self = this;
        this.path = "";
        this.content = "[CONTENT NOT LOADED]";
        
        this.viewLoader = viewLoader;
        this.viewModelManager = viewModelManager;
        this.viewManager = viewManager;


        function applyViewModelsByAttribute(path, container) {
            var viewModelApplied = false;

            $("[data-viewmodel]", container).each(function () {
                viewModelApplied = true;
                var target = $(this)[0];
                var viewModelName = $(this).attr("data-viewmodel");
                self.viewModelManager.get(viewModelName, path).continueWith(function (instance) {
                    ko.applyBindings(instance, target);
                });
            });

            return viewModelApplied;
        }

        function applyViewModelByConventionFromPath(path, container) {
            if (self.viewModelManager.hasForView(path)) {
                self.viewModelManager.getForView(path).continueWith(function (instance) {
                    ko.applyBindings(instance, container);
                });
            }
        }


        this.load = function (path) {
            var promise = Bifrost.execution.Promise.create();
            self.path = path;
            self.viewLoader.load(path).continueWith(function (html) {
                var container = $("<div/>").html(html)[0];
                
                var viewModelApplied = applyViewModelsByAttribute(path, container);
                if (viewModelApplied == false) {
                    applyViewModelByConventionFromPath(path, container);
                }

                self.viewManager.expandFor(container[0]);
                self.content = html;

                promise.signal(self);
            });

            return promise;
        };
    })
});