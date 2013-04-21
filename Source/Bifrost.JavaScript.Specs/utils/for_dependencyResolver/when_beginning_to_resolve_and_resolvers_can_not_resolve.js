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

        Bifrost.configure.reset();
        Bifrost.configure.onReady();
        try {
            Bifrost.dependencyResolver.beginResolve("Something").onFail(function (e) {
                exception = e;
            });
        } catch (e) {
            exception = e;
        }
    });

    afterEach(function () {
        Bifrost.configure.reset();
    });


    it("should throw unresolved dependencies exception", function () {
        expect(exception instanceof Bifrost.UnresolvedDependencies).toBeTruthy();
    });

});