Bifrost.namespace("Bifrost.views", {
    typeConverterExtender: Bifrost.Singleton(function (typeConverters) {
        this.extend = function (target, typeAsString) {
            if (ko.isObservable(target)) {
                target.subscribe(function (newValue) {
                    var converted = typeConverters.convertFrom(newValue, typeAsString);
                    target(converted);
                });
            }
        };
    })
});
ko.extenders.typeConverter = Bifrost.views.typeConverterExtender.create().extend;