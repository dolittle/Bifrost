describe("when route parameters change and viewmodel has properties representing them", sinon.test(function() {
	var instance = null;
	var expectedString = "Horse";
	var expectedValue = "42";
	var location = "http://www.vg.no:8081/some/route#some/anchor?someString="+expectedString+"&someValue="+expectedValue+"&observedString="+expectedString+"&observedValue="+expectedValue;
	
 	function MyViewModel() {
		this.queryParameters.define({
			someString: "",
			someValue: 0,
			observedString: ko.observable(),
			observedValue: ko.observable()
		});
	}
	
	beforeEach(function() {
		sinon.stub(Bifrost.Uri,"create", function() { 
			return { 
				parameters: {},
				setLocation: function(location) {
					this.parameters.someString = expectedString;
					this.parameters.someValue = expectedValue;
					this.parameters.observedString = expectedString;
					this.parameters.observedValue = expectedValue;
				}
			}
		});
		Bifrost.messaging = Bifrost.messaging || {};
		Bifrost.messaging.messenger = {};

		var stateChangeCallback;
		sinon.stub(History.Adapter,"bind", function(window, event, callback) {
			stateChangeCallback = callback;
		});
		sinon.stub(History,"getState", function() {
			return {
				url: location
			}
		});
		
		Bifrost.features.ViewModel.baseFor(MyViewModel);

		instance = new MyViewModel();
		stateChangeCallback();
	});

	afterEach(function() {
		Bifrost.Uri.create.restore();	
		History.Adapter.bind.restore();
	});
	
	it("should set the properties with the values", function() {
		expect(instance.queryParameters.someString).toBe(expectedString);
		expect(instance.queryParameters.someValue).toBe(expectedValue);
		expect(instance.queryParameters.observedString()).toBe(expectedString);
		expect(instance.queryParameters.observedValue()).toBe(expectedValue);
	});
}));