describe("when asking if can resolve and there are two resolvers and first resolver can resolve", function() {
	var result = false;
	var element = document.createElement("div");
	var canResolveStub = sinon.stub();
	beforeEach(function() {
		Bifrost.views.viewResolver = Bifrost.Type.extend(function() {});

		Bifrost.views.viewResolvers.firstResolver = Bifrost.views.viewResolver.extend(function() {
			this.canResolve = function() { return true; };
		});
		Bifrost.views.viewResolvers.secondResolver = Bifrost.views.viewResolver.extend(function() {
			this.canResolve = canResolveStub;
		});

		var viewResolvers = Bifrost.views.viewResolvers.create();
		result = viewResolvers.canResolve(element);
	});

	afterEach(function() {
		Bifrost.views.viewResolvers.firstResolver = undefined;
		Bifrost.views.viewResolvers.secondResolver = undefined;
		Bifrost.views.viewResolver = undefined;
	});

	it("should not ask the second resolver", function() {
		expect(canResolveStub.called).toBe(false);
	});
});