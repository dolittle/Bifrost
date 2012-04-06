describe("when setting as super", function() {
	function MyViewModel() {
		
	}
	
	print(Bifrost.features);
	Bifrost.features.ViewModel.superFor(MyViewModel);
	
	it("should set prototype to be a ViewModel", function() {
		expect(MyViewModel.prototype.toString()).toBe("ViewModel");
		
	});
});