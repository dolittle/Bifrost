Bifrost.namespace("Bifrost.validation", {
    validationService: Bifrost.Singleton(function () {
        this.getForCommand = function (name) {
            var promise = Bifrost.execution.Promise.create();

            $.getJSON("/Validation/GetForCommand?name=" + name, function (e) {
                promise.signal(e.properties);
            });
            return promise;
        }
    })
});
Bifrost.WellKnownTypesDependencyResolver.types.validationService = Bifrost.validation.validationService;