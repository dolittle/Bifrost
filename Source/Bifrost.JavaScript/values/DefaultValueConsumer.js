Bifrost.namespace("Bifrost.values", {
    DefaultValueConsumer: Bifrost.values.ValueConsumer.extend(function (target, property) {
        this.consume = function(value) {
            target[property] = value;
        };
    })
});