Bifrost.namespace("Bifrost.values", {
    DateFormatter: Bifrost.values.Formatter.extend(function () {
        this.supportedType = Date;

        this.format = function (value, format) {
            return value.format(format);
        };
    })
});