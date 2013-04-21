describe("when resolving asynchronously and system is type", function () {
    var type = Bifrost.Type.extend(function (dependency) {
        this.something = "Hello";
        this.dependency = dependency;
    });
    var ns = {};

    beforeEach(function () {
        Bifrost.dependencyResolvers = {
            getAll: function () {
                return [{
                    canResolve: function () {
                        return true;
                    },
                    resolve: function (namespace, name) {
                        var promise = Bifrost.execution.Promise.create();
                        var system = type;
                        if (name === "dependency") {
                            system = name;
                        }
                        promise.signal(system);
                        return promise;
                    }
                }];
            }
        };

        Bifrost.configure.onReady();
        Bifrost.dependencyResolver
				.beginResolve(ns, "something")
				.continueWith(function (param, next) {
				    result = param;
				});
    });

    afterEach(function () {
        Bifrost.configure.reset();
    });


    it("should create instance of type and resolve dependencies", function () {
        expect(result.dependency).toBe("dependency");
    });
});