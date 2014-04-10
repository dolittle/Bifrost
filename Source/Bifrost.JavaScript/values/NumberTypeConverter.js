Bifrost.namespace("Bifrost.values", {
    NumberTypeConverter: Bifrost.values.TypeConverter.extend(function () {
        this.supportedType = Number;

        this.convertFrom = function (value) {
            var result = 0;
            if (value.indexOf(".") >= 0 ) result = parseFloat(value);
            else result = parseInt(value);
            return result;
        };
    })
});