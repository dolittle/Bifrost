describe("when asking if can resolve and there are no resolvers", function() {

	var viewResolvers = Bifrost.views.viewResolvers.create();

	var result = viewResolvers.canResolve(document.createElement("div"));

	it("should not be able to resolve", function() {
		expect(result).toBe(false);
	});
});