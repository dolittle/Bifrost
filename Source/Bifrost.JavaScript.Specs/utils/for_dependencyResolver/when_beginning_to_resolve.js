describe("when beginning to resolve", function () {
    var ns = {};
    var result;

	beforeEach(function () {
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

	it("should return a promise", function() {
		expect(result instanceof Bifrost.execution.Promise).toBe(true);
	});
});