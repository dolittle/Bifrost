describe("when asking to resolve element without data view attribute on it", function() {
	var resolver = Bifrost.views.DataAttributeViewResolver.create();

	var element = document.createElement("div");
	var canResolve = resolver.canResolve(element);

	it("should be not able to resolve", function() {
		expect(canResolve).toBe(false);
	});
});