describe("when setting as base", function() {
	function MyViewModel() {
	}
	
	Bifrost.features.ViewModel.baseFor(MyViewModel);
	
	var typeInfo = MyViewModel.prototype.getTypeInfo();
	
	it("should set prototype to be a ViewModel", function() {
		expect(typeInfo.name).toBe("ViewModel");
	});
});