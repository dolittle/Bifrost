Bifrost.namespace("Bifrost.views", {
    typeConverters: Bifrost.Singleton(function () {
        var convertersByType = {};

        var typeConverterTypes = Bifrost.views.TypeConverter.getExtenders();
        typeConverterTypes.forEach(function (type) {
            var converter = type.create();
            convertersByType[converter.supportedType] = converter;
        });
		
        this.convertFrom = function (value, type) {
            if (Bifrost.isString(type)) {
                type = eval(type);
            }
            if (convertersByType.hasOwnProperty(type)) {
                return convertersByType[type].convertFrom(value);
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
})