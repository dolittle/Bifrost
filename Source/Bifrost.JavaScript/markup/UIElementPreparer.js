Bifrost.namespace("Bifrost.markup", {
    UIElementPreparer: Bifrost.Singleton(function () {
        this.prepare = function (element, instance) {
            var result = instance.prepare(instance._type, element);
            if (result instanceof Bifrost.execution.Promise) {
                result.continueWith(function () {

                    if (!Bifrost.isNullOrUndefined(instance.template)) {
                        var UIManager = Bifrost.views.UIManager.create();

                        UIManager.handle(instance.template);

                        ko.applyBindingsToNode(instance.template, {
                            "with": instance
                        });

                        element.parentElement.replaceChild(instance.template, element);
                    }
                });
            }
        };
    })
});