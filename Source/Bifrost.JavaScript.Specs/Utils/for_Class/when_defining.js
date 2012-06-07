describe("when defining", function() {
	Bifrost.ClassInfo = {};
	var typeDefinition = function(something) {};
	var result = Bifrost.Class(typeDefinition);
	
	it("should return an object", function() {
		expect(typeof result == "object").toBeTruthy();
	});
	
	it("should return an object of type ClassInfo", function() {
		expect(result instanceof Bifrost.ClassInfo).toBeTruthy();
	});
	
	it("should have the typeDefinition set to the input type definition", function() {
		expect(result.typeDefinition).toBe(typeDefinition);
	})
});