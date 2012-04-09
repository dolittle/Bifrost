describe("when creating instance of implementor", function() {
	function MyViewModel() {
		
	}
	
	Bifrost.features.ViewModel.baseFor(MyViewModel);
	
	var instance = new MyViewModel();
	
	it("should have messenger", function() {
		expect(instance.messenger).not.toBeUndefined();
	});
});