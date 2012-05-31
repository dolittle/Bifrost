describe("when setting as base", function() {
	var typeInfo = null;
	
	function MyViewModel() {
	}

	beforeEach(function() {
		sinon.stub(Bifrost.Uri,"create");

		Bifrost.messaging = Bifrost.messaging || {}
		Bifrost.messaging.messenger = {};


		Bifrost.features.ViewModel.baseFor(MyViewModel);

		typeInfo = MyViewModel.prototype.getTypeInfo();
	});
	
	afterEach(function() {
		Bifrost.Uri.create.restore();	
	});
	
	
	it("should set prototype to be a ViewModel", function() {
		expect(typeInfo.name).toBe("ViewModel");
	});
});