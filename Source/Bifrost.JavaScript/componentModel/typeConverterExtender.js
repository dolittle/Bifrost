Bifrost.namespace("Bifrost.componentModel", {
    typeConverterExtender: Bifrost.Singleton(function () {
        this.extend = function (target, typeAsString) {
            target._typeAsString = typeAsString;
        };
    })
});
ko.extenders.typeConverter = Bifrost.componentModel.typeConverterExtender.create().extend;
