Bifrost.namespace("Bifrost", {
    systemClock: Bifrost.Singleton(function () {
        this.nowInMilliseconds = function () {
            return window.performance.now();
        };
    })
});