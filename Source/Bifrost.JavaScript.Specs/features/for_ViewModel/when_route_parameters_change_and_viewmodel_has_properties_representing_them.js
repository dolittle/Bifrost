describe("when route parameters change and viewmodel has properties representing them", function() {
 	function MyViewModel() {
		this.routeParameters = {
			someString: "",
			someValue: 0
		}
	}
	
	Bifrost.features.ViewModel.baseFor(MyViewModel);
	
	var instance = new MyViewModel();
	
	it("should set the properties with the values", function() {
		
	});
});