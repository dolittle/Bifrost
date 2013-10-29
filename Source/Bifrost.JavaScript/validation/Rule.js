Bifrost.namespace("Bifrost.validation", {
    Rule: Bifrost.Type.extend(function (options) {
        var self = this;
        options = options || {};
        this.message = options.message || "";
        this.options = {};
        Bifrost.extend(this.options, options);

        this.validate = function (value) {
            return true;
        };
    })
});