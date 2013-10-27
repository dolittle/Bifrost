Bifrost.namespace("Bifrost.validation", {
    required: Bifrost.validation.Rule.extend(function () {
        var self = this;

        this.validate = function (value) {
            return !(Bifrost.isUndefined(value) || Bifrost.isNull(value) || value == "");
        }
    })
});

