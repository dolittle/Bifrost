describe("when creating", function() {
	var firstInstance = Bifrost.Uri.create();
	var secondInstance = Bifrost.Uri.create();
	
	it("should return an instance", function() {
		expect(firstInstance).not.toBeUndefined();
	});
	
	it("should return unique instances", function() {
		expect(firstInstance).not.toBe(secondInstance);
	});
});