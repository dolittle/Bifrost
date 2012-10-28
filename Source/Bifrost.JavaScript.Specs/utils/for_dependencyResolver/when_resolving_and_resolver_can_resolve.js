describe("when resolving and resolver can resolve", function () {
    var resolveCalled = false;
    var systemReceived = null;
    var system = { blah: "something" };

    beforeEach(function () {
        Bifrost.dependencyResolvers = {
            resolver: {
                canResolve: sinon.stub().returns(true),
                resolve: function (callback) {
                    resolveCalled = true;
                    callback(system);
                }
            }
        };
        Bifrost.dependencyResolver.resolve("something", function (system) {
            systemReceived = system;
        });
    });

    afterEach(function () {
        Bifrost.dependencyResolvers = {};
    });

    it("should call resolve", function () {
        expect(resolveCalled).toBe(true);
    });

    it("should call the callback with result from resolve", function () {
        expect(systemReceived).toBe(system);
    });
});