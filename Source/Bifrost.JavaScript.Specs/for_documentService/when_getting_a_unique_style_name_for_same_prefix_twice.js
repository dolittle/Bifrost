describe("when getting a unique style name for same prefix twice", function() {
	
	var service = Bifrost.documentService.createWithoutScope({DOMRoot: {}});

	var first = service.getUniqueStyleName("Something");
	var second = service.getUniqueStyleName("Something");

	it("should not return same style name", function() {
		expect(first).not.toBe(second);
	});
});