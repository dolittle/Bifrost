describe("when checking an array", function() {
	var result = Bifrost.isArray([]);
	it("should return true", function() {
		expect(result).toBe(true);
	});
});