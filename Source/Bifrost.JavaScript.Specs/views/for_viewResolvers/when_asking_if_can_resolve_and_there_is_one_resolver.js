describe("when asking if can resolve and there is one resolver", function() {
	var result = false;
	var element = document.createElement("div");
	var canResolveSpy = sinon.spy();

	beforeEach(function() {
		Bifrost.views.viewResolvers.myResolver = Bifrost.views.viewResolver.extend(function() {
			this.canResolve  = canResolveSpy;
		});
		var viewResolvers = Bifrost.views.viewResolvers.create();
		result = viewResolvers.canResolve(element);
	});

	afterEach(function() {
		Bifrost.views.viewResolvers.myResolver = undefined;
	});

	it("should ask the concrete resolver", function() {
		expect(canResolveSpy.calledWith(element)).toBe(true);
	});
});