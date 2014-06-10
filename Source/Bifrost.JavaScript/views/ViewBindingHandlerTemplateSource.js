Bifrost.namespace("Bifrost.views", {
    ViewBindingHandlerTemplateSource: Bifrost.Type.extend(function (viewFactory, UIManager) {
        var self = this;

        var content = "<div>Not Loaded</div>";


        this.loadFor = function (element, view, region) {
            var promise = Bifrost.execution.Promise.create();

            view.load(region).continueWith(function (loadedView) {
                console.log("Loaded : " + view.path);

                var wrapper = document.createElement("div");
                wrapper.innerHTML = loadedView.content;
                UIManager.handle(wrapper);

                content = wrapper.innerHTML;
                
                if (Bifrost.isNullOrUndefined(loadedView.viewModelType)) {
                    promise.signal(loadedView);
                } else {
                    Bifrost.views.Region.current = region;
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