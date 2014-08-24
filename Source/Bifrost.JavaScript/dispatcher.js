Bifrost.namespace("Bifrost", {
    dispatcher: Bifrost.Singleton(function () {
        this.schedule = function (milliseconds, callback) {
            setTimeout(callback, milliseconds);
        };
    })
});