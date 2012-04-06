describe("when getting for an object", function() {
	function SomeType() {
		
	}
	var instance = new SomeType();
	
	var typeInfo = instance.getTypeInfo();
	
	it("should return an instance", function() {
		expect(typeInfo).not.toBeUndefined();
	});
	
	it("should have name of type", function() {
		expect(typeInfo.name).toBe("SomeType");
	});
});