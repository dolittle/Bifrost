describe("when beginning to resolve", function() {
	var ns = {};

	Bifrost.dependencyResolvers = {
		getAll: function() {
			return [];
		}
	};
	var result = Bifrost.dependencyResolver.beginResolve(ns, "something");

	it("should return a promise", function() {
		expect(result instanceof Bifrost.execution.Promise).toBe(true);
	});
});