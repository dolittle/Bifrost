Bifrost.namespace("Bifrost.mapping", {
    PropertyMap: Bifrost.Type.extend(function (sourceProperty) {
        var self = this;

        this.strategy = null;

        function throwIfMissingPropertyStrategy() {
            if (Bifrost.isNullOrUndefined(self.strategy)) {
                throw Bifrost.mapping.MissingPropertyStrategy.create();
            }
        }

        this.to = function (targetProperty) {
            self.strategy = function (source, target) {
                target[targetProperty] = source[sourceProperty];
            };
        };

        this.map = function (source, target) {
            throwIfMissingPropertyStrategy();

            self.strategy(source, target);
        };
    })
});