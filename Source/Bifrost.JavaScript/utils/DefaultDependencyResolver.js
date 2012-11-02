Bifrost.namespace("Bifrost", {
    DefaultDependencyResolver: function () {

        this.canResolve = function (namespace, name) {
            var current = namespace;
            while (current != null) {
                if (current.hasOwnProperty(name)) {
                    return true;
                }
                current = current.parent;
            }

            return false;
        }

        this.resolve = function (namespace, name) {

            var current = namespace;
            while (current != null) {
                if (current.hasOwnProperty(name)) {
                    return current[name];
                }
                current = current.parent;
            }

            return null;
        }
    }
});
