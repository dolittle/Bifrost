describe("when resolving asynchronous and resolver returns a promise", function() {
	var ns = {};
	var innerPromise = Bifrost.execution.Promise.create();
	var result = null;

	beforeEach(function () {
	    Bifrost.dependencyResolvers = {
	        getAll: function () {
	            return [{
	                canResolve: function () {
	                    return true;
	                },
	                resolve: function () {
	                    return innerPromise;
	                }
	            }];
	        }
	    };

	    Bifrost.configure.reset();
	    Bifrost.configure.onReady();

	    
	    Bifrost.dependencyResolver
            .beginResolve(ns, "something")
            .continueWith(function (arg, nextPromise) {
                result = arg;

            });
	    innerPromise.signal("Hello");
	});


	it("should continue with inner promise parameter when inner promise continues", function() {
		expect(result).toBe("Hello");
	});
});