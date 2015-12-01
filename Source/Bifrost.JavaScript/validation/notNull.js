Bifrost.namespace("Bifrost.validation", {
    notNull: Bifrost.validation.Rule.extend(function () {
        this.validate = function (value) {
            return !(Bifrost.isUndefined(value) || Bifrost.isNull(value));
        };
    })
});

