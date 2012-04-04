describe("when resetting", function() {
	
	Bifrost.routing.router.register({});
	
	//Bifrost.routing.router.reset();
	
	it("should clear routes", function() {
		expect(Bifrost.routing.router.routes.length).toBe(0);
	});
});