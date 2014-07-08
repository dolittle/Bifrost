Bifrost.namespace("Bifrost.types", {
    TypeInfo: Bifrost.Type.extend(function () {
        this.properties = [];
    })
});
Bifrost.types.TypeInfo.createFrom = function (instance) {
    var typeInfo = Bifrost.types.TypeInfo.create();
    for (var property in instance) {
        var value = instance[property];
        if (!Bifrost.isNullOrUndefined(value)) {

            var type = value.constructor;

            if (!Bifrost.isNullOrUndefined(instance[property]._type)) {
                type = instance[property]._type;
            }

            var propertyInfo = Bifrost.types.PropertyInfo.create({
                name: property,
                type: type
            });
        }
        typeInfo.properties.push(propertyInfo);
    }
    return typeInfo;
};
