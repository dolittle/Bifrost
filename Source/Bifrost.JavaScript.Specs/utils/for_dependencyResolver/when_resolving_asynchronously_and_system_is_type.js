describe("when resolving asynchronously and system is type", function () {
    var type = Bifrost.Type.extend(function (dependency) {
        this.something = "Hello";
        this.dependency = dependency;
    });
    var ns = {
        something: type
    };
    var readyCallback;
    var configure = null;

    var dependencyResolvers;

    beforeEach(function () {
        configure = Bifrost.configure;
        Bifrost.configure = {
            ready: function (callback) {
                readyCallback = callback;
            }
        };

        dependencyResolvers = Bifrost.dependencyResolvers;
        Bifrost.dependencyResolvers = {
            getAll: function () {
                return [{
                    canResolve: function (namespace, name) {
                        print("CanResolve : " + name);
                        return true;
                    },
                    resolve: function (namespace, name) {
                        print("Resolve : " + name);
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

        
        print("\n\n*** beginResolve ***\n\n");
        
        Bifrost.dependencyResolver
				.beginResolve(ns, "something")
				.continueWith(function (param, next) {
				    result = param;
				});

        readyCallback();

        print("\n\n*** end beginResolve ***\n\n");
    });

    afterEach(function () {
        print("DONE");
        Bifrost.dependencyResolvers = dependencyResolvers;
        Bifrost.configure = configure;
    });


    it("should create instance of type and resolve dependencies", function () {
        print("So... how are we?");
        expect(result.dependency).toBe("dependency");
    });
});