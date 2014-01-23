Bifrost.namespace("Bifrost.views", {
    View: Bifrost.Type.extend(function (viewLoader, path) {
        var self = this;
        this.viewLoader = viewLoader;

        this.path = path;
        this.content = "[CONTENT NOT LOADED]";
        this.element = null;


        this.load = function (region) {
            var promise = Bifrost.execution.Promise.create();
            self.viewLoader.load(self.path, region).continueWith(function (html) {
                self.content = html;
                promise.signal(self);
            });

            return promise;
        };
    })
});