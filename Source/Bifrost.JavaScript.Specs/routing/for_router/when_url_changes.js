describe("when url changes", function() {
	var isMatchCalled = false;
	
	var route = {
		isMatch : function() {
			isMatchCalled = true;
		}
	};
	
	Bifrost.routing.router.register(route)


	History.pushState({},"","");
	
	it("should ask route if it is able to handle it", function() {
		expect(isMatchCalled).toBe(true);
	});
});