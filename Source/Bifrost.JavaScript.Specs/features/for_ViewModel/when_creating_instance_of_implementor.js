describe("when creating instance of implementor", sinon.test(function() {
	var instance = null;
	
	function MyViewModel() {}

	beforeEach(function() {
		sinon.stub(Bifrost.Uri,"create");
		
		Bifrost.messaging = Bifrost.messaging || {}
		Bifrost.messaging.messenger = {};
		
		Bifrost.features.ViewModel.baseFor(MyViewModel);
		instance = new MyViewModel();
	});

	afterEach(function() {
		Bifrost.Uri.create.restore();	
	});
	
	it("should have messenger", function() {
		expect(instance.messenger).not.toBeUndefined();
	});
}));