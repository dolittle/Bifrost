Bifrost.namespace("Bifrost", {
    isNullOrUndefined: function (value) {
        return Bifrost.isUndefined(value) || Bifrost.isNull(value);
    }
});