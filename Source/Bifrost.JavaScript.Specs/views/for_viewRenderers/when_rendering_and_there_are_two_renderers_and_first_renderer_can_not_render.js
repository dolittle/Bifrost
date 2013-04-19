describe("when rendering and there are two renderers and first renderer can not render", function() {
	var result = false;
	var element = document.createElement("div");

	var firstRendererRenderStub = sinon.stub();
	var secondRendererRenderSpy = sinon.spy();

	beforeEach(function() {
		
		Bifrost.views.viewRenderers.firstResolver = Bifrost.views.ViewRenderer.extend(function() {
			this.canRender = function() { return false; };
			this.render = firstRendererRenderStub;
		});
		Bifrost.views.viewRenderers.secondResolver = Bifrost.views.ViewRenderer.extend(function() {
			this.canRender = function() { return true; };
			this.render = secondRendererRenderSpy;
		});

		var viewRenderers = Bifrost.views.viewRenderers.create();
		result = viewRenderers.render(element);
	});

	afterEach(function() {
		Bifrost.views.viewRenderers.firstRenderer = undefined;
		Bifrost.views.viewRenderers.secondRenderer = undefined;
	});

	it("should not use first renderer for rendering", function() {
		expect(firstRendererRenderStub.called).toBe(false);
	});

	it("should use the second renderer for rendering", function() {
		expect(secondRendererRenderSpy.calledWith(element)).toBe(true);
	});
});