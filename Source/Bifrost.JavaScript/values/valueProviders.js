Bifrost.namespace("Bifrost.values", {
    valueProviders: Bifrost.Singleton(function () {

        this.isKnownValueProvider = function (name) {
        };

        this.getInstanceOf = function (name) {
        };
    })
});
Bifrost.WellKnownTypesDependencyResolver.types.valueProviders = Bifrost.values.valueProviders;