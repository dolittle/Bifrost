describe("when creating for element", function() {
	var element = $("<div><table/></div>")[0];
	Bifrost.navigation.NavigationContainer.createForElement(element);	
	it("should add navigation property", function() {
		expect(element.navigation).toBeDefined();
	});
	it("should remove any children inside the element", function() {
		expect($(element).children().length).toBe(0);
	});
});