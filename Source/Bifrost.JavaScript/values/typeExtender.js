Bifrost.namespace("Bifrost.values", {
    typeExtender: Bifrost.Singleton(function () {
        this.extend = function (target, typeAsString) {
            target._typeAsString = typeAsString;
        };
    })
});
ko.extenders.type = Bifrost.values.typeExtender.create().extend;
