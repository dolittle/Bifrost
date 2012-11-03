describe("when checking an object", function() {
	var result = Bifrost.isArray({});
	it("should return true", function() {
		expect(result).toBe(false);
	});
});