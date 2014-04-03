Bifrost.namespace("Bifrost.views", {
    TypeConverter: Bifrost.Type.extend(function () {
        this.supportedType = null;

        this.convertFrom = function (value) {
            return value;
        };

        this.convertTo = function (value) {
            return value;
        };
    })
});