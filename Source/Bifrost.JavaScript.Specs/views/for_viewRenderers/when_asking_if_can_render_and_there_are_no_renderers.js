describe("when asking if can render and there are no renderers", function() {
	var viewRenderers = Bifrost.views.viewRenderers.create();
	var result = viewRenderers.canRender(document.createElement("div"));

	it("should not be able to render", function() {
		expect(result).toBe(false);
	});
});