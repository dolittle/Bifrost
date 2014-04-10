Bifrost.namespace("Bifrost.values", {
    typeConverters: Bifrost.Singleton(function () {
        var convertersByType = {};

        var typeConverterTypes = Bifrost.values.TypeConverter.getExtenders();
        typeConverterTypes.forEach(function (type) {
            var converter = type.create();
            convertersByType[converter.supportedType] = converter;
        });

        this.convertFrom = function (value, type) {
            var actualType = null;
            if (Bifrost.isString(type)) {
                actualType = eval(type);
            } else {
                actualType = type;
            }
            if (convertersByType.hasOwnProperty(actualType)) {
                return convertersByType[actualType].convertFrom(value);
            }

            return value;
        };

        this.convertTo = function (value) {
            for (var converter in convertersByType) {
                if (value.constructor == converter) {
                    return convertersByType[converter].convertTo(value);
                }
            }

            return value;
        };
    })
});
Bifrost.WellKnownTypesDependencyResolver.types.typeConverters = Bifrost.values.typeConverters;
