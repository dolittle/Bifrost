Bifrost.namespace("Bifrost.componentModel", {
    DateTypeConverter: Bifrost.componentModel.TypeConverter.extend(function() {
        this.supportedType = Date;

        this.convertFrom = function (value) {
            var date = new Date(value);
            return date;
        };

        this.convertTo = function (value) {
            return value.format("yyyy-MM-dd");
        };
    })
});