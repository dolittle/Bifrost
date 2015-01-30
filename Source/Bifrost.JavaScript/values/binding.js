Bifrost.namespace("Bifrost.values", {
    Binding: Bifrost.values.ValueProvider.extend(function () {

        this.defaultProperty = "path";

        this.path = "";
        this.mode = null;
        this.converter = null;
        this.format = null;

        this.provide = function (consumer) {

        };
    })
});