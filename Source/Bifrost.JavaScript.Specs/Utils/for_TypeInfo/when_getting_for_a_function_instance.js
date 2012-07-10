describe("when getting for a function instance", function() {
	function SomeType() {
		
	}
	var instance = new SomeType();
	
	var typeInfo = Bifrost.TypeInfo.getFor(instance);
	
	it("should return an instance", function() {
		expect(typeInfo).not.toBeUndefined();
	});
	
	it("should have name of type", function() {
		expect(typeInfo.name).toBe("SomeType");
	});
});