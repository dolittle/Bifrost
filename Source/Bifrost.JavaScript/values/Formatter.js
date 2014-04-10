Bifrost.namespace("Bifrost.values", {
    Formatter: Bifrost.Type.extend(function () {
        this.supportedType = null;

        this.format = function (value, format) {
            return value;
        };
    })
});