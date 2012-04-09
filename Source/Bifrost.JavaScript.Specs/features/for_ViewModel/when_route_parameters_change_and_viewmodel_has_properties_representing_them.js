describe("when route parameters change and viewmodel has properties representing them", sinon.test(function() {
	var instance = null;
	
 	function MyViewModel() {
		this.routeParameters = {
			someString: "",
			someValue: 0
		}
	}
	
	beforeEach(function() {
		sinon.stub(Bifrost.Uri,"create");
		Bifrost.messaging = Bifrost.messaging || {};
		Bifrost.messaging.messenger = {};
		
		Bifrost.features.ViewModel.baseFor(MyViewModel);

		instance = new MyViewModel();
	});

	afterEach(function() {
		Bifrost.Uri.create.restore();	
	});
	
	it("should set the properties with the values", function() {
		
	});
}));