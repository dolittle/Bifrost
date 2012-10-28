describe("when getting for an array", function() {
	var instanceToGetFrom = [];
	var typeInfo = null;
	
	beforeEach(function() {
		typeInfo = Bifrost.TypeInfo.getFor(instanceToGetFrom);
	});
	
	it("should have type name set to array", function() {
		expect(typeInfo.name).toBe("Array");
	});
});