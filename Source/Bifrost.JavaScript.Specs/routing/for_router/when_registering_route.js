describe("when registering route", function() {
	var route = { name : "something" };
	Bifrost.routing.router.register(route);
	
	it("should have one route", function() {
		expect(Bifrost.routing.router.routes.length).toBe(1);
	});
	
	it("should the actual route", function() {
		expect(Bifrost.routing.router.routes[0]).toBe(route);
	});
});