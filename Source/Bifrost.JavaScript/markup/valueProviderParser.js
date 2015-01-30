Bifrost.namespace("Bifrost.markup", {
    valueProviderParser: Bifrost.Singleton(function (valueProviders, valueConsumers, typeConverters) {
        var self = this;
        var regex = new RegExp("{{([a-z ,:{{}}}]*)}}", "g");

        function handleValue(instance, property, value) {
            var consumer = valueConsumers.getFor(instance, property);

            if (self.hasValueProvider(value)) {
                var providers = self.parseFor(instance, property, value);
                providers.forEach(function (provider) {
                    provider.provide(consumer);
                });
            } else {
                consumer.consume(value);
            }
        }


        this.hasValueProvider = function (value) {
            var result = value.match(regex);
            if (result) {
                return true;
            }

            return false;
        };

        this.parseFor = function (instance, name, value) {
            var providers = [];
            var result = value.match(regex);
            var expression = result[0].substr(2, result[0].length - 4);

            var rx = new RegExp("([a-z]*) +", "g");
            result = expression.match(rx);
            if (result.length === 1) {
                var valueProviderName = result[0].trim();

                if (valueProviders.isKnown(valueProviderName)) {
                    var provider = valueProviders.getInstanceOf(valueProviderName);
                    providers.push(provider);

                    if (expression.length > result[0].length) {
                        var configurationString = expression.substr(result[0].length);
                        var elements = configurationString.split(",");

                        elements.forEach(function (element) {
                            element = element.trim();

                            var keyValuePair = element.split(":");
                            if (keyValuePair.length === 0) {
                                // something is wrong
                            }
                            if (keyValuePair.length === 1) {
                                // Value only

                                // Only valid if value provider has default property and that property exist

                                var value = keyValuePair[0];
                                handleValue(provider, provider.defaultProperty, value);
                                
                            } else if (keyValuePair.length === 2) {
                                // Property and value

                                // Invalid if property does not exist

                                handleValue(provider, keyValuePair[0], keyValuePair[1]);
                            } else {
                                // something is wrong - there are too many
                            }
                        });
                    }
                }
            }
            return providers;
        };
    })
});