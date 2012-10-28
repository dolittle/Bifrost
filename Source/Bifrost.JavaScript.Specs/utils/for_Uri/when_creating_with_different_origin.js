describe("when creating with different", function() {
	var instance = Bifrost.Uri.create("http://www.vg.no/some/route");
	
	it("should be considered as different origin Uri", function() {
		expect(instance.isSameAsOrigin).toBe(false);
	});
});