describe("when asking if can renderer and there are two renderers and first renderer can not render", function() {
	var result = false;
	var element = document.createElement("div");
	var canRenderSpy = sinon.spy();
	beforeEach(function() {
		Bifrost.views.viewRenderer.firstRenderer = Bifrost.views.ViewRenderer.extend(function() {
			this.canRender = function() { return false; };
		});
		Bifrost.views.viewRenderer.secondRenderer = Bifrost.views.ViewRenderer.extend(function() {
			this.canRender = canRenderSpy;
		});

		var viewRenderers = Bifrost.views.viewRenderers.create();
		result = viewRenderers.canRender(element);
	});

	afterEach(function() {
		Bifrost.views.viewRenderers.firstRenderer = undefined;
		Bifrost.views.viewRenderers.secondRenderer = undefined;
	});

	it("should ask the second renderer", function() {
		expect(canRenderSpy.calledWith(element)).toBe(true);
	});
});