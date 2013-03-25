describe("when asking if can render and there is one renderer", function() {
	var result = false;
	var element = document.createElement("div");
	var canRenderSpy = sinon.spy();

	beforeEach(function() {
		Bifrost.views.viewRenderers.myRenderer = Bifrost.views.ViewRenderer.extend(function() {
			this.canRender  = canRenderSpy;
		});
		var viewRenderers = Bifrost.views.viewRenderers.create();
		result = viewRenderers.canRender(element);
	});

	afterEach(function() {
		Bifrost.views.viewRenderers.myRenderer = undefined;
	});

	it("should ask the concrete renderer", function() {
		expect(canRenderSpy.calledWith(element)).toBe(true);
	});
});