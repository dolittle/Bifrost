describe("when getting for an object literal", function() {
	var instanceToGetFor = { };
	var typeInfo = null;
	
	beforeEach(function() {
		typeInfo = instanceToGetFor.getTypeInfo();
	});
	
	it("should have name of object", function() {
		expect(typeInfo.name).toBe("Object");
	});
});