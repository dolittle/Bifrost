describe("when setting as base", function() {
	var prototypeName = null;
	
	function MyViewModel() {
	}

	beforeEach(function() {
		sinon.stub(Bifrost.Uri,"create");

		Bifrost.messaging = Bifrost.messaging || {}
		Bifrost.messaging.messenger = {};

		Bifrost.features.ViewModel.baseFor(MyViewModel);

		try {
			var target = new MyViewModel();
			var funcNameRegex = /function (.{1,})\(/;
			var results = (funcNameRegex).exec((target).constructor.toString());
			prototypeName = (results && results.length > 1) ? results[1] : "";
		} catch( e ) {
			prototypeName = "unknown";
		}
	});
	
	afterEach(function() {
		Bifrost.Uri.create.restore();	
	});
	
	
	it("should set prototype to be a ViewModel", function() {
		expect(prototypeName).toBe("ViewModel");
	});
});