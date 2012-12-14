describe("when checking a function", function() {
	var result = Bifrost.isArray(function() {});
	it("should return true", function() {
		expect(result).toBe(false);
	});
});