describe("when resolving and there are two resolvers and first resolver can not resolve", function() {
	var result = false;
	var element = document.createElement("div");

	var firstResolverResolveStub = sinon.stub();
	var secondResolverResolveSpy = sinon.spy();

	beforeEach(function() {
		Bifrost.views.viewResolver = Bifrost.Type.extend(function() {});
		Bifrost.views.viewResolvers.firstResolver = Bifrost.views.viewResolver.extend(function() {
			this.canResolve = function() { return false; };
			this.resolve = firstResolverResolveStub;
		};
		Bifrost.views.viewResolvers.secondResolver = Bifrost.views.viewResolver.extend(function() {
			this.canResolve: function() { return true; },
			this.resolve = secondResolverResolveSpy;
		};

		var viewResolvers = Bifrost.views.viewResolvers.create();
		result = viewResolvers.resolve(element);
	});

	afterEach(function() {
		Bifrost.views.viewResolvers.firstResolver = undefined;
		Bifrost.views.viewResolvers.secondResolver = undefined;
		Bifrost.views.viewResolver = undefined;
	});

	it("should not use first resolver for resolving", function() {
		expect(firstResolverResolveStub.called).toBe(false);
	});

	it("should user the second resolver for resolving", function() {
		expect(secondResolverResolveSpy.calledWith(element)).toBe(true);
	});
});