describe("when asking if can resolve and there is one resolver", function() {
	var result = false;
	var element = document.createElement("div");
	var canResolveSpy = sinon.spy();

	beforeEach(function() {
		Bifrost.views.viewResolver = Bifrost.Type.extend(function() {});

		Bifrost.views.viewResolvers.myResolver = Bifrost.views.viewResolver.extend(function() {
			this.canResolve  = canResolveSpy;
		});
		var viewResolvers = Bifrost.views.viewResolvers.create();
		result = viewResolvers.canResolve(element);
	});

	afterEach(function() {
		Bifrost.views.viewResolvers.myResolver = undefined;
		Bifrost.views.viewResolver = undefined;
	});

	it("should ask the concrete resolver", function() {
		expect(canResolveSpy.calledWith(element)).toBe(true);
	});
});