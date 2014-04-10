Bifrost.namespace("Bifrost.values", {
    typeConverterExtender: Bifrost.Singleton(function () {
        this.extend = function (target, typeAsString) {
            target._typeAsString = typeAsString;
        };
    })
});
ko.extenders.typeConverter = Bifrost.values.typeConverterExtender.create().extend;
