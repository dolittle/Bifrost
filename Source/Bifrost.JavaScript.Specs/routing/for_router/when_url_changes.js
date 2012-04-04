describe("when url changes", function() {
	var isMatchCalled = false;
	
	var route = {
		isMatch : function() {
			isMatchCalled = true;
		}
	};

	beforeEach(function() {
		Bifrost.routing.router.register(route)
		History.pushState({},"","?Something=5");
	});
	
	afterEach(function() {
		Bifrost.routing.router.reset();
	});
	
	it("should ask route if it is able to handle it", function() {
		expect(isMatchCalled).toBe(true);
	});
});