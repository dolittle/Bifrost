Bifrost.namespace("Bifrost.views", {
    ViewBindingHandlerTemplateSource: Bifrost.Type.extend(function (viewFactory, pathResolvers, UIManager, viewUri, region) {
        var self = this;

        var content = "<div>Not Loaded</div>";


        this.loadFor = function (element) {
            var promise = Bifrost.execution.Promise.create();

            var actualPath = pathResolvers.resolve(element, viewUri);
            var view = viewFactory.createFrom(actualPath)

            view.load(region).continueWith(function (loadedView) {
                console.log("Loaded : " + viewUri);

                var wrapper = document.createElement("div");
                wrapper.innerHTML = loadedView.content;
                UIManager.handle(wrapper);

                content = wrapper.innerHTML;
                
                if (Bifrost.isNullOrUndefined(loadedView.viewModelType)) {
                    promise.signal(loadedView);
                } else {
                    view.viewModelType.ensure().continueWith(function () {
                        promise.signal(loadedView);
                    });
                }
            });

            return promise;
        };

        this.data = function (key, value) { };

        this.text = function (value) {
            console.log("Get view text");
            return content;
        };
    })
});