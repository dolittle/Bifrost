Bifrost.dependencyResolvers.DOMRootDependencyResolver = {
    canResolve: function (namespace, name) {
        return name === "DOMRoot";
    },

    resolve: function (namespace, name) {
        if (document.body != null && typeof document.body !== "undefined") {
            return document.body;
        }

        var promise = Bifrost.execution.Promise.create();
        Bifrost.dependencyResolvers.DOMRootDependencyResolver.promises.push(promise);
        return promise;
    }
};

Bifrost.dependencyResolvers.DOMRootDependencyResolver.promises = [];
Bifrost.dependencyResolvers.DOMRootDependencyResolver.documentIsReady = function () {
    Bifrost.dependencyResolvers.DOMRootDependencyResolver.promises.forEach(function (promise) {
        promise.signal(document.body);
    });
};