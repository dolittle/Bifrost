describe("when asking to resolve element with data view attribute on it", function() {
	var resolver = Bifrost.views.DataAttributeViewResolver.create();

	var element = document.createElement("div");
	element.setAttribute("data-view","something");
	var canResolve = resolver.canResolve(element);

	it("should be able to resolve", function() {
		expect(canResolve).toBe(true);
	});
});