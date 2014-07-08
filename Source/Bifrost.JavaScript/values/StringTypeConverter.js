Bifrost.namespace("Bifrost.values", {
    StringTypeConverter: Bifrost.values.TypeConverter.extend(function () {
        this.supportedType = String;

        this.convertFrom = function (value) {
            return value.toString();
        };
    })
});