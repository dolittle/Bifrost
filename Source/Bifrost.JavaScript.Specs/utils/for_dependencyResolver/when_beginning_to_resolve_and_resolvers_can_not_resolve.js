describe("when beginning to resolve and resolvers can not resolve", function () {
    var resolver = {
        canResolve: sinon.stub().returns(false)
    };
    var exception;

    beforeEach(function () {
        Bifrost.dependencyResolvers = {
            getAll: function () {
                return [resolver];
            }
        };
        try {
            Bifrost.dependencyResolver.beginResolve("Something");
        } catch (e) {
            exception = e;
        }
    });

    it("should throw unresolved dependencies exception", function () {
        expect(exception instanceof Bifrost.UnresolvedDependencies).toBeTruthy();
    });

});