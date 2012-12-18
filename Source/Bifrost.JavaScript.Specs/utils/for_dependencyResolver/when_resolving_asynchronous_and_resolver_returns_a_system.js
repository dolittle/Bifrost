describe("when resolving asynchronous and resolver returns a system", function() {
	var ns = {};
	Bifrost.dependencyResolvers = {
		getAll: function() {
			return [{
				canResolve: function() {
					return true;
				},
				resolve: function() {
					return "The result";
				}
			}];
		}
	};

	var result = null;
	Bifrost.dependencyResolver
		.beginResolve(ns, "something")
		.continueWith(function(parameter, nextPromise) {
			result = parameter;
		});

	it("should continue with system from resolver as parameter", function() {
		expect(result).toBe("The result");
	});
});