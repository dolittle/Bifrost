Bifrost.namespace("Bifrost.values", {
    valueConsumers: Bifrost.Singleton(function () {

        this.getFor = function (instance, propertyName) {
            var consumer = Bifrost.values.DefaultValueConsumer.create({
                target: instance,
                property: propertyName
            });
            return consumer;
        };

    })
});
Bifrost.WellKnownTypesDependencyResolver.types.valueConsumers = Bifrost.values.valueConsumers;