describe("when defining", function() {
	Bifrost.ClassInfo = {};
	var typeDefinition = function(something) {};
	var result = Bifrost.Type(typeDefinition);
	
	it("should return an object", function() {
		expect(typeof result == "object").toBeTruthy();
	});
	
	it("should return an object of type TypeInfo", function() {
		expect(result instanceof Bifrost.TypeInfo).toBeTruthy();
	});
	
	it("should have the typeDefinition set to the input type definition", function() {
		expect(result.typeDefinition).toBe(typeDefinition);
	})
});