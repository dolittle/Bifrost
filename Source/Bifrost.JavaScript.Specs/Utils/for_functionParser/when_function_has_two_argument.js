describe("when function has two arguments", function() {
	var func = function(first, second) {};

	var result = Bifrost.functionParser.parse(func);
		
	it("should return array with one argument", function() {
		expect(result.length).toBe(2ß);
	});
	
	it("should have the first argument as name of the element", function() {
		expect(result[0].name).toBe("first");
	});
	
	it("should have the second argument as name of the element", function() {
		expect(result[1].name).toBe("second");
	});
});