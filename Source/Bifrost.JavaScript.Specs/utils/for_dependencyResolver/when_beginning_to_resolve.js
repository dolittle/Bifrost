describe("when beginning to resolve", function () {
    var ns = {};
    var result;

    var configure = null;
    var dependencyResolvers;

    beforeEach(function () {
        configure = Bifrost.configure;
        Bifrost.configure = {
            ready: sinon.stub()
        };

        dependencyResolvers = Bifrost.dependencyResolvers;

        Bifrost.dependencyResolvers = {
            getAll: function () {
                return [{
                    canResolve: function () { return true; },
                    resolve: function () {

                        var promise = Bifrost.execution.Promise.create();
                        return promise;
                    }
                }];
            }
        };
        result = Bifrost.dependencyResolver.beginResolve(ns, "something");
    });

    afterEach(function () {
        Bifrost.dependencyResolvers = dependencyResolvers;
        Bifrost.configure = configure;
    });


	it("should return a promise", function() {
		expect(result instanceof Bifrost.execution.Promise).toBe(true);
	});
});