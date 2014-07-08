describe("when resolving asynchronous and resolver returns a promise", function() {
	var ns = {};
	var innerPromise = Bifrost.execution.Promise.create();
	var result = null;

	var configure = null;

	beforeEach(function () {
	    configure = Bifrost.configure;

	    Bifrost.configure = {
	        ready: function (callback) {
	            callback();
	        }
	    };


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

	    
	    Bifrost.dependencyResolver
            .beginResolve(ns, "something")
            .continueWith(function (arg, nextPromise) {
                result = arg;

            });
	    innerPromise.signal("Hello");
	});

	afterEach(function () {
	    Bifrost.configure = configure;
	});


	it("should continue with inner promise parameter when inner promise continues", function() {
		expect(result).toBe("Hello");
	});
});