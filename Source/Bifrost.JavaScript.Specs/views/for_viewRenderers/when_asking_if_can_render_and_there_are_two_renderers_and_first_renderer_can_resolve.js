describe("when asking if can render and there are two renderers and first renderer can render", function() {
	var result = false;
	var element = document.createElement("div");
	var canRendererStub = sinon.stub();
	beforeEach(function() {
		Bifrost.views.viewRenderers.firstRenderer = Bifrost.views.ViewRenderer.extend(function() {
			this.canRenderer = function() { return true; };
		});
		Bifrost.views.viewRenderers.secondRenderer = Bifrost.views.ViewRenderer.extend(function() {
			this.canRender = canRendererStub;
		});

		var viewRenderers = Bifrost.views.viewRenderers.create();
		result = viewRenderers.canRender(element);
	});

	afterEach(function() {
		Bifrost.views.viewRenderers.firstRenderer = undefined;
		Bifrost.views.viewRenderers.secondRenderer = undefined;
	});

	it("should not ask the second renderer", function() {
		expect(canRenderStub.called).toBe(false);
	});
});