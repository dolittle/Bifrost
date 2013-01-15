Bifrost.namespace("Bifrost", {
    InternalDependencyResolver: function () {
        this.canResolve = function (namespace, name) {
            return false;
        },
        this.resolve = function (namespace, name) {
        }
    }
});
