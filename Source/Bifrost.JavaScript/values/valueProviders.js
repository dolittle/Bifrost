Bifrost.namespace("Bifrost.values", {
    valueProviders: Bifrost.Singleton(function () {

        this.isKnown = function (name) {
            var found = false;
            var valueProviders = Bifrost.values.ValueProvider.getExtenders();
            valueProviders.forEach(function (valueProviderType) {
                if (valueProviderType._name.toLowerCase() === name) {
                    found = true;
                    return;
                }
            });
            return found;
        };

        this.getInstanceOf = function (name) {
            var instance = null;
            var valueProviders = Bifrost.values.ValueProvider.getExtenders();
            valueProviders.forEach(function (valueProviderType) {
                if (valueProviderType._name.toLowerCase() === name) {
                    instance = valueProviderType.create();
                    return;
                }
            });

            return instance;
        };
    })
});
Bifrost.WellKnownTypesDependencyResolver.types.valueProviders = Bifrost.values.valueProviders;