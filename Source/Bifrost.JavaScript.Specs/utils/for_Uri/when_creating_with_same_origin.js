describe("when creating with same origin", function() {
	var instance = Bifrost.Uri.create(
		window.location.protocol+"://"+
		window.location.host+"/some/route");
	
	it("should be considered same origin Uri", function() {
		expect(instance.isSameAsOrigin).toBe(true);
	});
});