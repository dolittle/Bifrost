describe("when function has one argument with surrounding whitespace", function() {
	var func = function( first ) {};

	var result = Bifrost.functionParser.parse(func);
		
	it("should return array with one argument", function() {
		expect(result.length).toBe(1);
	});
	
	it("should have the argument as name of the element", function() {
		expect(result[0].name).toBe("first");
	});
});