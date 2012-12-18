describe("when resolving asynchronous but resolver returns a promise", function() {
	var ns = {};
	var innerPromise = Bifrost.execution.Promise.create();
	Bifrost.dependencyResolvers = {
		getAll: function() {
			return [{
				canResolve: function() {
					return true;
				},
				resolve: function() {
					return innerPromise;
				}
			}];
		}
	};

	var result = null;
	Bifrost.dependencyResolver
		.beginResolve(ns, "something")
		.continueWith(function(arg, nextPromise) {
			result = arg;

		});
	innerPromise.signal("Hello");

	it("should continue with inner promise parameter when inner promise continues", function() {
		expect(result).toBe("Hello");
	});
});