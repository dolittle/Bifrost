describe("when activating", function() {
	var instance = null;
	var handleUriState = false;
	
	function MyViewModel() {
	}

	beforeEach(function() {
		sinon.stub(Bifrost.Uri,"create", function() {
			return {
				setLocation: function() {}
			}
		});
		
		Bifrost.messaging = Bifrost.messaging || {}
		Bifrost.messaging.messenger = {};
		
		Bifrost.features.ViewModel.baseFor(MyViewModel);
		instance = new MyViewModel();
		instance.handleUriState = function() {
			handleUriState = true;
		}
		
		instance.onActivated();
	});

	afterEach(function() {
		Bifrost.Uri.create.restore();	
	});
	
	
	it("should handle uri state", function() {
		expect(handleUriState).toBe(true);
	});
});