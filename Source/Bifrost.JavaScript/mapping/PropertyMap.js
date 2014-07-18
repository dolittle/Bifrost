Bifrost.namespace("Bifrost.mapping", {
    PropertyMap: Bifrost.Type.extend(function (sourceProperty, typeConverters) {
        var self = this;

        this.strategy = null;

        function throwIfMissingPropertyStrategy() {
            if (Bifrost.isNullOrUndefined(self.strategy)) {
                throw Bifrost.mapping.MissingPropertyStrategy.create();
            }
        }

        this.to = function (targetProperty) {
            self.strategy = function (source, target) {
                var value = ko.unwrap(source[sourceProperty]);
                var targetValue = ko.unwrap(target[targetProperty]);

                var typeAsString = null;
                if (!Bifrost.isNullOrUndefined(value)) {
                    if (!Bifrost.isNullOrUndefined(targetValue)) {
                        if (value.constructor != targetValue.constructor) {
                            typeAsString = targetValue.constructor.name.toString();
                        }

                        if (!Bifrost.isNullOrUndefined(target[targetProperty]._typeAsString)) {
                            typeAsString = target[targetProperty]._typeAsString;
                        }
                    }

                    if (!Bifrost.isNullOrUndefined(typeAsString) ) {
                        value = typeConverters.convertFrom(value.toString(), typeAsString);
                    }
                }

                if (ko.isObservable(target[targetProperty])) {
                    target[targetProperty](value);
                } else {
                    target[targetProperty] = value;
                }
            };
        };

        this.map = function (source, target) {
            throwIfMissingPropertyStrategy();

            self.strategy(source, target);
        };
    })
});