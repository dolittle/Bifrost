describe("when beginning to resolve and resolvers can not resolve", function () {
    var resolver = {
        canResolve: sinon.stub().returns(false)
    };
    var exception;
    var readyCallback;

    var configure = null;
    beforeEach(function () {
        configure = Bifrost.configure;
        Bifrost.configure = {
            ready: function (callback) {
                readyCallback = callback;
            }
        };
        Bifrost.dependencyResolvers = {
            getAll: function () {
                return [resolver];
            }
        };

        try {
            Bifrost.dependencyResolver.beginResolve("Something").onFail(function (e) {
                exception = e;
            });

            readyCallback();
        } catch (e) {
            exception = e;
        }
    });

    afterEach(function () {
        Bifrost.configure = configure;
    });


    it("should throw unresolved dependencies exception", function () {
        expect(exception instanceof Bifrost.UnresolvedDependencies).toBeTruthy();
    });

});