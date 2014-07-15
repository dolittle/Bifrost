describe("when resolving and resolvers can not resolve", function () {
    var resolver = {
        canResolve: sinon.stub().returns(false)
    };
    var exception;

    var dependencyResolvers;
    beforeEach(function () {
        dependencyResolvers = Bifrost.dependencyResolvers;

        Bifrost.dependencyResolvers = {
            getAll: function () {
                return [resolver];
            }
        };
        try {
            Bifrost.dependencyResolver.resolve("Something");
        } catch (e) {
            exception = e;
        }
    });

    afterEach(function () {
        Bifrost.dependencyResolvers = dependencyResolvers;
    });

    it("should throw unresolved dependencies exception", function () {
        expect(exception instanceof Bifrost.UnresolvedDependencies).toBeTruthy();
    });

});